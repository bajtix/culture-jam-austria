Shader "Unlit/DrawingShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BrushPosition ("Brush", Vector) = (0,0,0,0)
        _BrushRadius ("Radius", Float) = 0.1
        
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
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
            float4 _MainTex_ST;
            float2 _BrushPosition;
            float _BrushRadius;

            v2f vert (appdata v)
            {
                v2f o;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            float frag (v2f i) : SV_Target
            {
                float2 range = (i.uv - _BrushPosition);
                float dst = range.x*range.x + range.y*range.y;
                float l = 1-smoothstep(0, _BrushRadius, dst);
                return lerp(tex2D(_MainTex, i.uv), 0, l);
            }

            ENDCG
        }
    }
}
