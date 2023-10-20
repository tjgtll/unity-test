Shader "Custom/RotateTextureShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _RotationAngle ("Rotation Angle", Range(0, 360)) = 0
    }

    CGINCLUDE
    #include "UnityCG.cginc"
    ENDCG

    SubShader
    {
        Tags{"Queue" = "Overlay" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma exclude_renderers gles xbox360 ps3
            ENDCG

            SetTexture[_MainTex]
            {
                combine primary
            }

            SetTexture[_MainTex]
            {
                combine secondary
                constantColor (0, 0, 0, 1)
                combine texture
            }
        }
    }

    SubShader
    {
        Tags{"Queue" = "Overlay" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma exclude_renderers gles xbox360 ps3
            ENDCG

            SetTexture[_MainTex]
            {
                combine primary
            }

            SetTexture[_MainTex]
            {
                combine secondary
                constantColor (0, 0, 0, 1)
                combine texture
            }

            SetTexture[_MainTex]
            {
                combine primary
                constantColor[_Color]
            }
        }
    }
}

