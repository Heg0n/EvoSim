Shader "Custom/Outline"
{
    Properties
    {
        _OutlineColor ("Outline Color", Color) = (0, 0, 1, 1) // Blue outline
        _OutlineWidth ("Outline Width", Float) = 0.03 // Thickness of outline
        _MainTex ("Main Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" }

        // Outline Pass
        Pass
        {
            Name "OUTLINE"
            Cull Front // Render outline behind the object
            ZWrite On
            ZTest LEqual

            Stencil {
                Ref 1
                Comp always
                Pass replace
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            float _OutlineWidth;
            void vert(appdata v, out v2f o)
            {
                o.pos = UnityObjectToClipPos(v.vertex + v.normal * _OutlineWidth);
            }

            half4 _OutlineColor;
            half4 frag(v2f i) : SV_Target
            {
                return _OutlineColor;
            }
            ENDCG
        }

        // Main Object Pass (Keeps original material)
        Pass
        {
            Name "MAIN"
            Cull Back
            ZWrite On
            ZTest LEqual

            Stencil {
                Ref 1
                Comp notequal
            }

            SetTexture[_MainTex] { combine texture }
        }
    }
    FallBack "Diffuse"
}