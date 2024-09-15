Shader "Custom/LocalPosition" {
        Properties{
            _Color("Color", Color) = (1,1,1,1)
            _MainTex("Albedo (RGB)", 2D) = "white" {}
            _PBR("PBR", 2D) = "white" {}
            [Normal]_Normal("Normal", 2D) = "bump"{}
            _Emission("Emission", 2D) = "white" {}

            _Amount("Amount", Range(0, 1)) = 0
            _NoiseTex("NoiseTex", 2D) = "white" {}
        }
            SubShader{
                Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
                LOD 200

                Cull Off
                Blend SrcAlpha OneMinusSrcAlpha

                Pass {
                    CGPROGRAM
                    #pragma vertex vert
                    #pragma fragment frag
                    #include "UnityCG.cginc"

                    sampler2D _MainTex;
                    sampler2D _NoiseTex;
                    sampler2D _PBR;
                    sampler2D _Normal;
                    sampler2D _Emission;

                    fixed4 _Color;
                    fixed _Amount;

                    struct appdata_t {
                        float4 vertex : POSITION;
                        float2 uv : TEXCOORD0;
                    };

                    struct v2f {
                        float2 uv : TEXCOORD0;
                        float4 vertex : SV_POSITION;
                    };

                    v2f vert(appdata_t v) {
                        v2f o;
                        o.vertex = UnityObjectToClipPos(v.vertex);

                        // Noise displacement
                        float3 disp = tex2Dlod(_NoiseTex, float4(v.uv, 0, 0)).rgb;
                        o.uv = v.uv + disp.xy * _Amount;
                        return o;
                    }

                    fixed4 frag(v2f i) : SV_Target {
                        // Main texture sampling
                        fixed4 col = tex2D(_MainTex, i.uv) * _Color;

                    // Emission and PBR
                    fixed4 emission = tex2D(_Emission, i.uv);
                    col.rgb += emission.rgb;

                    // Alpha handling
                    col.a = col.a * _Color.a;
                    return col;
                }
                ENDCG
            }
        }
            FallBack "Sprites/Default"
    }