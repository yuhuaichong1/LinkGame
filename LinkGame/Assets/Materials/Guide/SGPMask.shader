Shader "Custom/SGPMask"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _CutoutTex ("Cutout Texture", 2D) = "white" {}
        _CutoutPos ("Cutout Position", Vector) = (0,0,0,0)
        _CutoutSize ("Cutout Size", Vector) = (0.1,0.1,0,0)
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                half2 texcoord  : TEXCOORD0;
                float4 worldPosition : TEXCOORD1;
            };

            sampler2D _MainTex;
            sampler2D _CutoutTex;
            fixed4 _Color;
            float4 _CutoutPos;
            float4 _CutoutSize;

            v2f vert(appdata_t IN)
            {
                v2f OUT;
                OUT.worldPosition = IN.vertex;
                OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);
                OUT.texcoord = IN.texcoord;
                OUT.color = IN.color * _Color;
                return OUT;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                half4 color = tex2D(_MainTex, IN.texcoord) * IN.color;
                
                // Calculate if current pixel is inside cutout area
                float2 cutoutUV = (IN.worldPosition.xy - _CutoutPos.xy) / _CutoutSize.xy;
                if(all(cutoutUV >= 0) && all(cutoutUV <= 1))
                {
                    half4 cutout = tex2D(_CutoutTex, cutoutUV);
                    color.a *= (1 - cutout.a); // Cut out based on hole's alpha
                }
                
                return color;
            }
            ENDCG
        }
    }
}
