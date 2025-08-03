Shader "Custom/Fade" {
        Properties {
        // 主纹理属性，用于接收 Image 的纹理
        _MainTex ("Base (RGB)", 2D) = "white" {}
    }
    SubShader {
        // 渲染类型设为透明，适用于 UI Image
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Pass {
            // 开启混合模式，确保透明效果正确
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                half2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // 从主纹理采样颜色
                fixed4 col = tex2D(_MainTex, i.uv);
                // 计算灰度值，使用加权平均法
                float gray = dot(col.rgb, float3(0.299, 0.587, 0.114));
                // 将灰度值赋给颜色的 RGB 通道
                col.rgb = float3(gray, gray, gray);
                return col;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    FallBack "Diffuse"
}