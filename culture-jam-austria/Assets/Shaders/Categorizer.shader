Shader "Unlit/Categorizer"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _TexelSize ("Texel Size", Float) = 0.0078
        _Factor ("Downsample Factor", Int) = 4
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            Name "Max"
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
            float4 _MainTex_ST;
            float _TexelSize;
            int _Factor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float frag (v2f input) : SV_Target
            {
                float2 uv = floor(input.uv / (_TexelSize * _Factor)) * _TexelSize * _Factor;

                float maxf = 0;
                for(int i = 0; i < _Factor; i++) {
                    for(int j = 0; j < _Factor; j++) {
                        float myValue = tex2D(_MainTex, uv + float2((i+0.5) * _TexelSize, (j+0.5) * _TexelSize));
                        maxf = max(maxf, myValue);
                    }
                    
                }
                return maxf;
            }
            ENDHLSL
        }

        Pass
        {
            Name "Derivative"
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
            float4 _MainTex_ST;
            float _TexelSize;
            int _Factor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float frag (v2f input) : SV_Target
            {
                float2 uv = floor(input.uv / (_TexelSize * _Factor)) * _TexelSize * _Factor;

                float myValue = tex2D(_MainTex, uv + float2((.5) * _TexelSize, (.5) * _TexelSize));
                float otherValue = tex2D(_MainTex, uv + float2((1.5) * _TexelSize, (1.5) * _TexelSize));

                return otherValue - myValue;
            }
            ENDHLSL
        }
    }
}
