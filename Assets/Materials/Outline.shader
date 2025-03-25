Shader "Custom/Outline"
{
    Properties
    {
        _OutlineColor ("Outline Color", Color) = (0, 0, 1, 1)
        _OutlineWidth ("Outline Width", Float) = 1.0
        _MainTex ("Base (RGB)", 2D) = "white" { }
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        
        Pass {
            Name "OUTLINE"
            Tags { "LightMode" = "Always" }

            ZWrite On
            ZTest LEqual
            Cull Front
            ColorMask RGB

            // Outline color
            Color [_OutlineColor]
            
            // Inflate the geometry
            Offset 20, 20

            // Set the shader to ignore texture, we don't need it for the outline
            SetTexture[_MainTex] { combine primary }
        }
    }
    SubShader {
        Tags { "RenderType"="Opaque" }

        Pass {
            Name "OUTLINE"
            Tags { "LightMode" = "Always" }

            ZWrite On
            ZTest LEqual
            Cull Front
            ColorMask RGB

            // Outline color
            Color [_OutlineColor]
            
            // Inflate the geometry
            Offset 20, 20

            // Set the shader to ignore texture, we don't need it for the outline
            SetTexture[_MainTex] { combine primary }
        }
    }
    FallBack "Diffuse"
}