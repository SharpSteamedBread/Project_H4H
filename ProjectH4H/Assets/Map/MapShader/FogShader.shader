Shader "Custom/FogShader"
{
    Properties
    {
        _MainTex("Sprite Texture", 2D) = "white" {}
        _FogColor("Fog Color", Color) = (0.5, 0.5, 0.5, 1)
        _FogDensity("Fog Density", Float) = 0.05
    }
        SubShader
        {
            Tags {"Queue" = "Transparent" "RenderType" = "Transparent"}
            Blend SrcAlpha OneMinusSrcAlpha

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                #include "UnityCG.cginc"

                struct appdata_t
                {
                    float4 vertex : POSITION;
                    float2 texcoord : TEXCOORD0;
                };

                struct v2f
                {
                    float4 vertex : SV_POSITION;
                    float2 texcoord : TEXCOORD0;
                    float3 worldPos : TEXCOORD1;
                };

                sampler2D _MainTex;
                float4 _MainTex_ST;
                fixed4 _FogColor;
                float _FogDensity;

                v2f vert(appdata_t v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                    o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    fixed4 col = tex2D(_MainTex, i.texcoord);
                    float depth = i.worldPos.z;
                    float fogFactor = exp2(-_FogDensity * _FogDensity * depth * depth * 1.442695);
                    fogFactor = clamp(fogFactor, 0.0, 1.0);
                    col.rgb = lerp(_FogColor.rgb, col.rgb, fogFactor);
                    return col;
                }
                ENDCG
            }
        }
}
