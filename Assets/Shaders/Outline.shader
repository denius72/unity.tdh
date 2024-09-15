Shader "Custom/Outline" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _OutlineWidth ("Outline Width", Range(0, 0.1)) = 0.005
    }
    
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Lambert

        sampler2D _MainTex;
        fixed4 _Color;
        fixed4 _OutlineColor;
        float _OutlineWidth;

        struct Input {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o) {
            // Verificar se a normal não está com comprimento zero
            float2 outlineOffset = float2(0, 0);
            float normalLength = length(o.Normal.xy);
            if (normalLength > 0.0001) {
                outlineOffset = _OutlineWidth * normalize(o.Normal).xy;
            }
            
            // Desenhe o contorno com a cor do contorno
            float4 outlineColor = tex2D(_MainTex, IN.uv_MainTex + outlineOffset) * _OutlineColor;
            o.Albedo = lerp(outlineColor.rgb, _Color.rgb, o.Albedo);
        }
        ENDCG
    }
    FallBack "Diffuse"
}