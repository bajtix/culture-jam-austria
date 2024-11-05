// This shader adds tessellation in URP
Shader "Custom/Snow"
{

	// The properties block of the Unity shader. In this example this block is empty
	// because the output color is predefined in the fragment shader code.
	Properties
	{
		_Tess("Tessellation", Range(1, 32)) = 20
		_MaxTessDistance("Max Tess Distance", Range(1, 32)) = 20
		_HeightMap("Height map", 2D) = "gray" {}

		_Weight("Displacement Amount", Range(0, 1)) = 0
		_NormalStrength("Normal Strength", Range(0, 1)) = 0

		_MainTex ("Base Color", 2D) = "white" {}
		_Specular ("Specular", Vector) = (0,0,0,0)
		_Smoothness ("Smoothness", Float) = 0
	}

	// The SubShader block containing the Shader code. 
	SubShader
	{
		// SubShader Tags define when and under which conditions a SubShader block or
		// a pass is executed.
		Tags{ "RenderType" = "Opaque" "RenderPipeline" = "UniversalRenderPipeline" }

		Pass
		{
			Tags{ "LightMode" = "UniversalForward" }
			Cull Off
			ZWrite On

			// The HLSL code block. Unity SRP uses the HLSL language.
			HLSLPROGRAM
			// The Core.hlsl file contains definitions of frequently used HLSL
			// macros and functions, and also contains #include references to other
			// HLSL files (for example, Common.hlsl, SpaceTransforms.hlsl, etc.).
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"    
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"    
			#include "CustomTessellation.hlsl"

			#pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE  _SHADOWS_SOFT _SCREEN_SPACE_OCCLUSION  _MAIN_LIGHT_SHADOWS

			#pragma require tessellation
			// This line defines the name of the vertex shader. 
			#pragma vertex TessellationVertexProgram
			// This line defines the name of the fragment shader. 
			#pragma fragment frag
			// This line defines the name of the hull shader. 
			#pragma hull hull
			// This line defines the name of the domain shader. 
			#pragma domain domain

			sampler2D _HeightMap;
			sampler2D _MainTex;
			half4 _Specular;
			half _Smoothness;
			float _Weight;
			float _NormalStrength;

			// pre tesselation vertex program
			ControlPoint TessellationVertexProgram(Attributes v)
			{
				ControlPoint p;

				p.vertex = v.vertex;
				p.uv = v.uv;
				p.normal = v.normal;
				p.color = v.color;

				return p;
			}

		

			// after tesselation
			Varyings vert(Attributes input)
			{
				Varyings output;
 				

				float height = tex2Dlod(_HeightMap, float4(input.uv.x, input.uv.y, 0,0)).r;
				input.vertex.y += height * _Weight;

				VertexNormalInputs i = GetVertexNormalInputs(input.normal);
				Light l = GetMainLight();

				VertexPositionInputs positions = GetVertexPositionInputs(ApplyShadowBias(input.vertex.xyz, i.normalWS, l.direction));

				output.vertex = TransformObjectToHClip(input.vertex.xyz);
				output.color = input.color;
				output.normal = input.normal;
				output.uv = input.uv;
				output.shadowCoords = GetShadowCoord(positions);
				return output;
			}

			[UNITY_domain("tri")]
			Varyings domain(TessellationFactors factors, OutputPatch<ControlPoint, 3> patch, float3 barycentricCoordinates : SV_DomainLocation)
			{
				Attributes v;

				#define DomainPos(fieldName) v.fieldName = \
				patch[0].fieldName * barycentricCoordinates.x + \
				patch[1].fieldName * barycentricCoordinates.y + \
				patch[2].fieldName * barycentricCoordinates.z;

				DomainPos(vertex)
				DomainPos(uv)
				DomainPos(color)
				DomainPos(normal)

				return vert(v);
			}

			// The fragment shader definition.            
			half4 frag(Varyings IN) : SV_TARGET
			{

				Light l = GetMainLight();
				float offset = 0.01;
				
				float height = tex2D(_HeightMap, IN.uv).r;

				float heightRight = tex2D(_HeightMap, IN.uv + float2(offset,0)).r;
				float heightUp = tex2D(_HeightMap, IN.uv + float2(0, offset)).r;
				// Create the normal vector
				float3 normal = float3((heightRight - height)/offset * _Weight * _NormalStrength, 1.0, (heightUp - height)/offset * _Weight * _NormalStrength); // Invert X and Y for correct orientation
				normal = normalize(normal); 

				VertexNormalInputs i = GetVertexNormalInputs(normal);
				float3 nor  =  i.normalWS;
				float3 view = GetViewForwardDir();
				half4 color = tex2D(_MainTex, IN.uv);
				half shadowAmount = MainLightRealtimeShadow(IN.shadowCoords);
				float3 tex = LightingLambert(l.color,l.direction,nor) * color * clamp(shadowAmount, 0.2,1);
				
				return half4(tex,1);
			}
			
			ENDHLSL
		}

		
        Pass
        {
            Name "DepthNormalsOnly"
            Tags
            {
                "LightMode" = "DepthNormalsOnly"
            }

            // -------------------------------------
            // Render State Commands
            ZWrite On

            HLSLPROGRAM
			// The Core.hlsl file contains definitions of frequently used HLSL
			// macros and functions, and also contains #include references to other
			// HLSL files (for example, Common.hlsl, SpaceTransforms.hlsl, etc.).
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"    
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"    
			#include "CustomTessellation.hlsl"

			#pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE  _SHADOWS_SOFT _SCREEN_SPACE_OCCLUSION  _MAIN_LIGHT_SHADOWS

			#pragma require tessellation
			// This line defines the name of the vertex shader. 
			#pragma vertex TessellationVertexProgram
			// This line defines the name of the fragment shader. 
			#pragma fragment frag
			// This line defines the name of the hull shader. 
			#pragma hull hull
			// This line defines the name of the domain shader. 
			#pragma domain domain

			sampler2D _HeightMap;
			sampler2D _MainTex;
			half4 _Specular;
			half _Smoothness;
			float _Weight;
			float _NormalStrength;

			// pre tesselation vertex program
			ControlPoint TessellationVertexProgram(Attributes v)
			{
				ControlPoint p;

				p.vertex = v.vertex;
				p.uv = v.uv;
				p.normal = v.normal;
				p.color = v.color;

				return p;
			}

		

			// after tesselation
			Varyings vert(Attributes input)
			{
				Varyings output;
 				

				float height = tex2Dlod(_HeightMap, float4(input.uv.x, input.uv.y, 0,0)).r;
				input.vertex.y += height * _Weight;

				VertexNormalInputs i = GetVertexNormalInputs(input.normal);
				Light l = GetMainLight();

				VertexPositionInputs positions = GetVertexPositionInputs(ApplyShadowBias(input.vertex.xyz, i.normalWS, l.direction));

				output.vertex = TransformObjectToHClip(input.vertex.xyz);
				output.color = input.color;
				output.normal = input.normal;
				output.uv = input.uv;
				output.shadowCoords = GetShadowCoord(positions);
				return output;
			}

			[UNITY_domain("tri")]
			Varyings domain(TessellationFactors factors, OutputPatch<ControlPoint, 3> patch, float3 barycentricCoordinates : SV_DomainLocation)
			{
				Attributes v;

				#define DomainPos(fieldName) v.fieldName = \
				patch[0].fieldName * barycentricCoordinates.x + \
				patch[1].fieldName * barycentricCoordinates.y + \
				patch[2].fieldName * barycentricCoordinates.z;

				DomainPos(vertex)
				DomainPos(uv)
				DomainPos(color)
				DomainPos(normal)

				return vert(v);
			}

			// The fragment shader definition.            
			half4 frag(Varyings IN) : SV_TARGET
			{

				Light l = GetMainLight();
				float offset = 0.01;
				
				float height = tex2D(_HeightMap, IN.uv).r;

				float heightRight = tex2D(_HeightMap, IN.uv + float2(offset,0)).r;
				float heightUp = tex2D(_HeightMap, IN.uv + float2(0, offset)).r;
				// Create the normal vector
				float3 normal = float3((heightRight - height)/offset * _Weight * _NormalStrength, 1.0, (heightUp - height)/offset * _Weight * _NormalStrength); // Invert X and Y for correct orientation
				normal = normalize(normal); 

				VertexNormalInputs i = GetVertexNormalInputs(normal);
				float3 nor  =  i.normalWS;
				float3 view = GetViewForwardDir();
				half4 color = tex2D(_MainTex, IN.uv);
				half shadowAmount = MainLightRealtimeShadow(IN.shadowCoords);
				float3 tex = LightingLambert(l.color,l.direction,nor) * color * clamp(shadowAmount, 0.2,1);
				
				return half4(tex,1);
			}
			
			ENDHLSL
        }

	}
}
