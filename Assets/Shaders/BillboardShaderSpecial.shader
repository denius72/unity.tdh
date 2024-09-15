Shader "Custom/BillboardShader6"
{
    Properties {
        _MainTex ("Background Texture", 2D) = "white" {}
        _BillboardTex ("Billboard Texture", 2D) = "white" {}
        _BackfaceTex ("Backface Texture", 2D) = "white" {}
        _ScrollSpeed ("Scroll Speed", Range(0, 10)) = 1
        _BillboardAngle ("Billboard Angle (Degrees)", Range(0, 360)) = 0
    }
 
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 100
 
        Pass {
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off // Desativando Backface Culling

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
                float4 worldPos : TEXCOORD1;
            };
 
            sampler2D _MainTex;
            sampler2D _BillboardTex;
            sampler2D _BackfaceTex;
            float _ScrollSpeed;
            float _BillboardAngle;
 
            v2f vert (appdata_t v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }
 
            half4 frag (v2f i) : SV_Target {
                float angleRad = _BillboardAngle * 3.14159265359 / 180.0;
                float2 scrollDirection = float2(cos(angleRad), sin(angleRad));
                float2 backgroundUV = i.uv;
                float2 billboardUV = backgroundUV + _Time.y * _ScrollSpeed * scrollDirection;

                // Sample textures
                half4 backgroundCol = tex2D(_MainTex, backgroundUV);
                half4 billboardCol = tex2D(_BillboardTex, billboardUV);
                half4 backfaceCol = tex2D(_BackfaceTex, billboardUV);

                // Combine textures with alpha blending
                half4 finalColor = lerp(backgroundCol, backfaceCol * billboardCol.a, billboardCol.a);

                return finalColor;
            }
            ENDCG
        }
    }
}