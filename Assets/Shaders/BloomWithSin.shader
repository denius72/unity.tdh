Shader "Custom/BloomWithSin" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Bloom Color", Color) = (1, 1, 1, 1)
        _BloomStrength ("Bloom Strength", Range(0, 10)) = 1.0
        _Frequency ("Frequency", Range(0.1, 10)) = 1.0
    }
 
    SubShader {
        Tags { "RenderPipeline"="UniversalRenderPipeline" }
        Pass {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
 
            struct appdata_t {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
 
            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
 
            sampler2D _MainTex;
            float4 _Color;
            float _BloomStrength;
            float _Frequency;
 
            v2f vert(appdata_t v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
 
            half4 frag(v2f i) : SV_Target {
                half4 texColor = tex2D(_MainTex, i.uv);
                half bloomFactor = 0.5 + 0.5 * sin(_Frequency * _Time.y);
                half4 bloomColor = _Color * bloomFactor * _BloomStrength;
                return texColor + bloomColor;
            }
            ENDHLSL
        }
    }
}