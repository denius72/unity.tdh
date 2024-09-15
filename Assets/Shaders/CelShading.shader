Shader "Custom/CelShading" {
    Properties {
        _TextureEnabled ("Texture Enabled", Float) = 0
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
    }
    
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Lambert

        sampler2D _MainTex;
        fixed4 _Color;
        float _TextureEnabled;

        struct Input {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o) {
            fixed4 c;

            // Se a textura estiver habilitada, use-a como cor
            if (_TextureEnabled > 0.5) {
                c = tex2D(_MainTex, IN.uv_MainTex);
            } else {
                // Caso contrário, use a cor fixa
                c = _Color;
            }

            o.Albedo = c.rgb;

            // Determine o ângulo entre a normal da superfície e a direção da luz
            float NdotL = dot(o.Normal, _WorldSpaceLightPos0.xyz);
            
            // Defina o número de sombras (graduações de cor) que você deseja no cel shading
            // Quanto menor o valor, mais sombras haverá.
            float numShades = 3.0;
            
            // Ajuste o tom da cor com base no ângulo entre a normal e a luz
            o.Albedo = floor(NdotL * numShades) / numShades;
        }
        ENDCG
    }
    FallBack "Diffuse"
}