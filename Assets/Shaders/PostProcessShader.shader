Shader "Custom/PostProcessShader" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
    }
    SubShader {
        Pass {
            ZTest Always Cull Off ZWrite Off
            SetTexture [_MainTex] {
                combine texture
            }
        }
    }
}
