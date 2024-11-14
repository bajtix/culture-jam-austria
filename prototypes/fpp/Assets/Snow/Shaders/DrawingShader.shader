Shader "Unlit/DrawingShader"
{
    Properties
    {
        [NoScaleOffset] _MainTex ("Texture", 2D) = "white" {}
        [NoScaleOffset] _BrushTex ("Brush Texture", 2D) = "black" {}
        _Vectors ("Vectors", Vector) = (0,0,0,0)
        _RHS ("Radius Harshness Strength", Vector) = (0,0,0,0)

    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            Name "Circle"
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _BrushTex;
            float4 _MainTex_ST;
            float4 _Vectors;
            float4 _RHS;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float frag (v2f i) : SV_Target
            {
                float baseColor = tex2D(_MainTex, i.uv);

                float2 pos = _Vectors.xy;
                float radius = _RHS.x;
                float harsh = _RHS.y;
                float strength = _RHS.z;
                float alpha = clamp((radius - distance(i.uv, pos)) / (1-harsh), 0, 1) * strength;

                return clamp(baseColor - alpha, 0, 1);
            }

            ENDHLSL
        }

        Pass
        {
            Name "Line"
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _BrushTex;
            float4 _MainTex_ST;
            float4 _Vectors;
            float4 _RHS;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float DistanceToLineSegment(float2 pos1, float2 pos2, float2 target) {
                float2 lineVector = pos2 - pos1;
                float2 toTarget = target - pos1;
                float lineLength = length(lineVector);
                // Handle the case where the line segment is a point
                if (lineLength == 0.0f) {
                    return length(toTarget);
                }
                float t = dot(toTarget, lineVector) / (lineLength * lineLength);
                t = clamp(t, 0.0f, 1.0f);
                float2 closestPoint = pos1 + t * lineVector;
                return length(target - closestPoint);
            }

            float frag (v2f i) : SV_Target
            {
                float baseColor = tex2D(_MainTex, i.uv);

                float2 pos1 = _Vectors.xy;
                float2 pos2 = _Vectors.zw;
                float radius = _RHS.x;
                float harsh = _RHS.y;
                float strength = _RHS.z;

                float dst = DistanceToLineSegment(pos1, pos2, i.uv);

                float alpha = clamp((radius - dst) / (1-harsh), 0, 1) * strength;

                return clamp(baseColor - alpha, 0, 1);
            }

            ENDHLSL
        }

        Pass
        {
            Name "Texture"
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _BrushTex;
            float4 _MainTex_ST;
            float4 _Vectors;
            float4 _RHS;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float frag (v2f i) : SV_Target
            {
                float baseColor = tex2D(_MainTex, i.uv);
                float2 pos = _Vectors.xy;
                float2 size = _Vectors.zw;
                float rotation = _RHS.x;
                float strength = _RHS.z;
                

                // Calculate the UV coordinates for the brush texture
                float2 brushUV = (i.uv - pos) / size; // Normalize UV coordinates based on position and size

                // Apply rotation
                float cosTheta = cos(rotation);
                float sinTheta = sin(rotation);
                
                // Rotate the UV coordinates
                float2 rotatedUV;
                rotatedUV.x = brushUV.x * cosTheta - brushUV.y * sinTheta;
                rotatedUV.y = brushUV.x * sinTheta + brushUV.y * cosTheta;

                rotatedUV += 0.5;

                // Sample the brush texture
                float alpha = tex2D(_BrushTex, rotatedUV) * strength;

                return clamp(baseColor - alpha, 0, 1);
            }

            ENDHLSL
        }

        Pass
        {
            Name "Clear"
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _BrushTex;
            float4 _MainTex_ST;
            float4 _Vectors;
            float4 _RHS;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float frag (v2f i) : SV_Target
            {
                float baseColor = tex2D(_MainTex, i.uv);

                float strength = _RHS.z;

                return lerp(baseColor, 1, strength);
            }

            ENDHLSL
        }
    }
}
