Shader "Custom/DiamondTransitionWithColor"
{
    Properties
    {
        _Progress("Progress", Range(0, 1)) = 0
        _DiamondPixelSize("Diamond Pixel Size", Range(1, 100)) = 10
    }

        SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                fixed4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
            };

            sampler2D _MainTex;
            float _Progress;
            float _DiamondPixelSize;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.color = v.color;
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                half2 fragCoord = i.uv * _DiamondPixelSize;
                half xFraction = frac(fragCoord.x);
                half yFraction = frac(fragCoord.y);
                half xDistance = abs(xFraction - 0.5);
                half yDistance = abs(yFraction - 0.5);
                if (xDistance + yDistance + i.uv.x + i.uv.y > _Progress * 4) {
                    discard;
                }
                return tex2D(_MainTex, i.uv) * i.color;
            }
            ENDCG
        }
    }
}
