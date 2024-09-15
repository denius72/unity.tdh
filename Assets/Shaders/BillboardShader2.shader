Shader "Custom/BillboardShader2"
{
    Properties {
        _MainTex ("Background Texture", 2D) = "white" {}
        _BillboardTex ("Billboard Texture", 2D) = "white" {}
        _ScrollSpeed ("Scroll Speed", Range(0, 10)) = 1
        _BillboardAngle ("Billboard Angle (Degrees)", Range(0, 360)) = 0
    }
 
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 100
 
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
 
            struct appdata_t {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
 
            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
 
            sampler2D _MainTex;
            sampler2D _BillboardTex;
            float _ScrollSpeed;
            float _BillboardAngle;
 
            v2f vert (appdata_t v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
 
            half4 frag (v2f i) : SV_Target {
                float angleRad = _BillboardAngle * 3.14159265359 / 180.0;
                float2 scrollDirection = float2(cos(angleRad), sin(angleRad));
                float2 backgroundUV = i.uv;
                float2 billboardUV = i.uv + _Time.y * _ScrollSpeed * scrollDirection;
 
                half4 backgroundCol = tex2D(_MainTex, backgroundUV);
                half4 billboardCol = tex2D(_BillboardTex, billboardUV);
 
                // Combine the two textures with alpha blending
                return lerp(backgroundCol, billboardCol, billboardCol.a);
            }
            ENDCG
        }
    }
}