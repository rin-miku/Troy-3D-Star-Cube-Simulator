Shader "Custom/CircleMask"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Radius("Radius", Range(0, 1)) = 0.5
        _Center("Center", Vector) = (0.5, 0.5, 0, 0)
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            LOD 100

            Pass
            {
                CGPROGRAM
                #pragma vertex vert  
                #pragma fragment frag  
                #pragma multi_compile_fog  

                #include "UnityCG.cginc"  

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                    float4 screenPos : TEXCOORD1; // 用于计算屏幕空间坐标  
                };

                sampler2D _MainTex;
                float4 _MainTex_ST;
                float _Radius;
                float4 _Center; // (x, y, z, w) 其中z和w不使用  

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    o.screenPos = ComputeScreenPos(o.vertex);
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    // 计算屏幕空间下的中心点坐标  
                    float2 screenCenter = (_Center.xy * 2 - 1) * _ScreenParams.xy / _ScreenParams.zw;

                    // 计算当前像素到圆心的距离  
                    float2 diff = (i.uv - screenCenter.xy) * _ScreenParams.zw;
                    float dist = length(diff);

                    // 应用圆形裁剪  
                    if (dist > _Radius)
                    {
                        discard; // 裁剪掉圆外的像素  
                    }

                    // 采样纹理并返回颜色  
                    fixed4 col = tex2D(_MainTex, i.uv);
                    return col;
                }
                ENDCG
            }
        }
            FallBack "Diffuse"
}