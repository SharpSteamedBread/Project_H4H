// Made with Amplify Shader Editor v1.9.3.3
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "portal"
{

    Properties
    {
		[HideInInspector] _AlphaCutoff("Alpha Cutoff ", Range(0, 1)) = 0.5
		[HideInInspector] _EmissionColor("Emission Color", Color) = (1,1,1,1)
		_Tex0_baseT("Tex0_baseT", 2D) = "white" {}
		_Vector0("Tex0_Panning", Vector) = (1,1,0,0)
		_Float0("Tex0_Tiling", Float) = 1
		_Tex1_panning("Tex1_panning", Vector) = (0,0,0,0)
		_Tex2_NoiseT("Tex2_NoiseT", 2D) = "white" {}
		_Tex2_panning1("Tex2_panning", Vector) = (0,0,0,0)
		_Float1("offset_Tex2_Tilling", Float) = 1
		_Tex1_MaskT("Tex1_MaskT", 2D) = "white" {}
		[HDR]_Color0("Color 0", Color) = (1,1,1,0)
		[Toggle(_KEYWORD0_ON)] _Keyword0("Keyword 0", Float) = 1

        [HideInInspector][NoScaleOffset] unity_Lightmaps("unity_Lightmaps", 2DArray) = "" {}
        [HideInInspector][NoScaleOffset] unity_LightmapsInd("unity_LightmapsInd", 2DArray) = "" {}
        [HideInInspector][NoScaleOffset] unity_ShadowMasks("unity_ShadowMasks", 2DArray) = "" {}
    }

    SubShader
    {
		LOD 0

		
        Tags { "RenderPipeline"="UniversalPipeline" "RenderType"="Transparent" "UniversalMaterialType"="Lit" "Queue"="Transparent" "ShaderGraphShader"="true" }

		Cull Off
		Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
		ZTest LEqual
		ZWrite Off
		Offset 0 , 0
		ColorMask RGBA
		

		HLSLINCLUDE
		#pragma target 2.0
		#pragma prefer_hlslcc gles
		// ensure rendering platforms toggle list is visible
		ENDHLSL

		
        Pass
        {
			
            Name "Sprite Lit"
            Tags { "LightMode"="Universal2D" }

            HLSLPROGRAM

			#define ASE_SRP_VERSION 120111


			#pragma vertex vert
			#pragma fragment frag

            #define _SURFACE_TYPE_TRANSPARENT 1
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define ATTRIBUTES_NEED_TEXCOORD0
            #define ATTRIBUTES_NEED_COLOR
            #define VARYINGS_NEED_POSITION_WS
            #define VARYINGS_NEED_TEXCOORD0
            #define VARYINGS_NEED_COLOR
            #define VARYINGS_NEED_SCREENPOSITION

            #define SHADERPASS SHADERPASS_SPRITELIT

            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"

			#pragma shader_feature_local _KEYWORD0_ON


			struct VertexInput
			{
				float3 positionOS : POSITION;
				float3 normal : NORMAL;
				float4 tangent : TANGENT;
				float4 uv0 : TEXCOORD0;
				float4 color : COLOR;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 positionCS : SV_POSITION;
				float4 texCoord0 : TEXCOORD0;
				float3 positionWS : TEXCOORD1;
				float4 color : TEXCOORD2;
				float4 screenPosition : TEXCOORD3;
				float4 ase_texcoord4 : TEXCOORD4;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

            struct SurfaceDescription
			{
				float3 BaseColor;
				float Alpha;
			};

			#include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/SurfaceData2D.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Debug/Debugging2D.hlsl"

			half4 _RendererColor;

			sampler2D _Tex0_baseT;
			sampler2D _Tex1_MaskT;
			sampler2D _Tex2_NoiseT;
			CBUFFER_START( UnityPerMaterial )
			float4 _Tex2_NoiseT_ST;
			float4 _Color0;
			float2 _Vector0;
			float2 _Tex1_panning;
			float2 _Tex2_panning1;
			float _Float0;
			float _Float1;
			CBUFFER_END


			
			VertexOutput vert( VertexInput v  )
			{
				VertexOutput o;
				ZERO_INITIALIZE(VertexOutput, o);

				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				float4 ase_clipPos = TransformObjectToHClip((v.positionOS).xyz);
				float4 screenPos = ComputeScreenPos(ase_clipPos);
				o.ase_texcoord4 = screenPos;
				
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.positionOS.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif
				float3 vertexValue = defaultVertexValue;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.positionOS.xyz = vertexValue;
				#else
					v.positionOS.xyz += vertexValue;
				#endif
				v.normal = v.normal;
				v.tangent.xyz = v.tangent.xyz;

				VertexPositionInputs vertexInput = GetVertexPositionInputs(v.positionOS.xyz);

				o.positionCS = vertexInput.positionCS;
				o.positionWS.xyz =  vertexInput.positionWS;
				o.texCoord0.xyzw =  v.uv0;
				o.color.xyzw =  v.color;
				o.screenPosition.xyzw =  vertexInput.positionNDC;

				return o;
			}

			half4 frag( VertexOutput IN   ) : SV_TARGET
			{
				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN);

				float4 screenPos = IN.ase_texcoord4;
				float2 panner24 = ( 1.0 * _Time.y * _Vector0 + ( (screenPos).xy * _Float0 ));
				float3 objToWorld112 = mul( GetObjectToWorldMatrix(), float4( float3( 0,0,0 ), 1 ) ).xyz;
				float2 texCoord117 = IN.texCoord0.xy * float2( 1,1 ) + float2( 0,0 );
				float2 panner118 = ( 1.0 * _Time.y * _Tex1_panning + texCoord117);
				float2 uv_Tex2_NoiseT = IN.texCoord0.xy * _Tex2_NoiseT_ST.xy + _Tex2_NoiseT_ST.zw;
				float2 panner127 = ( 1.0 * _Time.y * _Tex2_panning1 + uv_Tex2_NoiseT);
				float4 tex2DNode21 = tex2D( _Tex1_MaskT, ( float4( panner118, 0.0 , 0.0 ) + ( tex2D( _Tex2_NoiseT, panner127 ) * _Float1 ) ).rg );
				float temp_output_23_0 = ( tex2DNode21.r * tex2DNode21.a );
				float4 tex2DNode129 = tex2D( _Tex1_MaskT, panner118 );
				float temp_output_130_0 = ( tex2DNode129.r * tex2DNode129.a );
				float temp_output_133_0 = ( saturate( ( temp_output_23_0 - temp_output_130_0 ) ) + saturate( ( temp_output_130_0 - temp_output_23_0 ) ) );
				#ifdef _KEYWORD0_ON
				float staticSwitch147 = temp_output_133_0;
				#else
				float staticSwitch147 = 0.0;
				#endif
				
				SurfaceDescription surfaceDescription = (SurfaceDescription)0;
				surfaceDescription.BaseColor = ( tex2D( _Tex0_baseT, ( panner24 * distance( _WorldSpaceCameraPos , objToWorld112 ) ) ) + ( saturate( staticSwitch147 ) * _Color0 ) ).rgb;
				surfaceDescription.Alpha = saturate( ( temp_output_130_0 + temp_output_133_0 + temp_output_23_0 ) );

				half4 color = half4(surfaceDescription.BaseColor, surfaceDescription.Alpha);

				#if defined(DEBUG_DISPLAY)
				SurfaceData2D surfaceData;
				InitializeSurfaceData(color.rgb, color.a, surfaceData);
				InputData2D inputData;
				InitializeInputData(IN.positionWS.xy, half2(IN.texCoord0.xy), inputData);
				half4 debugColor = 0;

				SETUP_DEBUG_DATA_2D(inputData, IN.positionWS);

				if (CanDebugOverrideOutputColor(surfaceData, inputData, debugColor))
				{
					return debugColor;
				}
				#endif

				color *= IN.color * _RendererColor;
				return color;
			}

            ENDHLSL
        }

		
        Pass
        {
			
            Name "Sprite Normal"
            Tags { "LightMode"="NormalsRendering" }

            HLSLPROGRAM

			#define ASE_SRP_VERSION 120111


			#pragma vertex vert
			#pragma fragment frag

            #define _SURFACE_TYPE_TRANSPARENT 1
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define VARYINGS_NEED_NORMAL_WS
            #define VARYINGS_NEED_TANGENT_WS

            #define SHADERPASS SHADERPASS_SPRITENORMAL

            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/NormalsRenderingShared.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"

			

			sampler2D _Tex1_MaskT;
			sampler2D _Tex2_NoiseT;
			CBUFFER_START( UnityPerMaterial )
			float4 _Tex2_NoiseT_ST;
			float4 _Color0;
			float2 _Vector0;
			float2 _Tex1_panning;
			float2 _Tex2_panning1;
			float _Float0;
			float _Float1;
			CBUFFER_END


			struct VertexInput
			{
				float3 positionOS : POSITION;
				float3 normal : NORMAL;
				float4 tangent : TANGENT;
				float4 ase_texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 positionCS : SV_POSITION;
				float3 normalWS : TEXCOORD0;
				float4 tangentWS : TEXCOORD1;
				float4 ase_texcoord2 : TEXCOORD2;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

            struct SurfaceDescription
			{
				float3 NormalTS;
				float Alpha;
			};

			
			VertexOutput vert( VertexInput v  )
			{
				VertexOutput o;
				ZERO_INITIALIZE(VertexOutput, o);

				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				o.ase_texcoord2.xy = v.ase_texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord2.zw = 0;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.positionOS.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif
				float3 vertexValue = defaultVertexValue;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.positionOS.xyz = vertexValue;
				#else
					v.positionOS.xyz += vertexValue;
				#endif
				v.normal = v.normal;
				v.tangent.xyz = v.tangent.xyz;


				float3 positionWS = TransformObjectToWorld(v.positionOS);
				float4 tangentWS = float4(TransformObjectToWorldDir(v.tangent.xyz), v.tangent.w);

				o.positionCS = TransformWorldToHClip(positionWS);
				o.normalWS.xyz =  -GetViewForwardDir();
				o.tangentWS.xyzw =  tangentWS;
				return o;
			}

			half4 frag( VertexOutput IN  ) : SV_TARGET
			{
				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN);

				float2 texCoord117 = IN.ase_texcoord2.xy * float2( 1,1 ) + float2( 0,0 );
				float2 panner118 = ( 1.0 * _Time.y * _Tex1_panning + texCoord117);
				float4 tex2DNode129 = tex2D( _Tex1_MaskT, panner118 );
				float temp_output_130_0 = ( tex2DNode129.r * tex2DNode129.a );
				float2 uv_Tex2_NoiseT = IN.ase_texcoord2.xy * _Tex2_NoiseT_ST.xy + _Tex2_NoiseT_ST.zw;
				float2 panner127 = ( 1.0 * _Time.y * _Tex2_panning1 + uv_Tex2_NoiseT);
				float4 tex2DNode21 = tex2D( _Tex1_MaskT, ( float4( panner118, 0.0 , 0.0 ) + ( tex2D( _Tex2_NoiseT, panner127 ) * _Float1 ) ).rg );
				float temp_output_23_0 = ( tex2DNode21.r * tex2DNode21.a );
				float temp_output_133_0 = ( saturate( ( temp_output_23_0 - temp_output_130_0 ) ) + saturate( ( temp_output_130_0 - temp_output_23_0 ) ) );
				
				SurfaceDescription surfaceDescription = (SurfaceDescription)0;
				surfaceDescription.NormalTS = float3(0.0f, 0.0f, 1.0f);
				surfaceDescription.Alpha = saturate( ( temp_output_130_0 + temp_output_133_0 + temp_output_23_0 ) );

				half crossSign = (IN.tangentWS.w > 0.0 ? 1.0 : -1.0) * GetOddNegativeScale();
				half3 bitangent = crossSign * cross(IN.normalWS.xyz, IN.tangentWS.xyz);
				half4 color = half4(1.0,1.0,1.0, surfaceDescription.Alpha);

				return NormalsRenderingShared(color, surfaceDescription.NormalTS, IN.tangentWS.xyz, bitangent, IN.normalWS);
			}

            ENDHLSL
        }

		
        Pass
        {
			
            Name "SceneSelectionPass"
            Tags { "LightMode"="SceneSelectionPass" }

            Cull Off
			Blend Off
			ZTest LEqual
			ZWrite On

            HLSLPROGRAM

			#define ASE_SRP_VERSION 120111


			#pragma vertex vert
			#pragma fragment frag

            #define _SURFACE_TYPE_TRANSPARENT 1
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT

            #define SHADERPASS SHADERPASS_DEPTHONLY
	        #define SCENESELECTIONPASS 1


            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"

			

			sampler2D _Tex1_MaskT;
			sampler2D _Tex2_NoiseT;
			CBUFFER_START( UnityPerMaterial )
			float4 _Tex2_NoiseT_ST;
			float4 _Color0;
			float2 _Vector0;
			float2 _Tex1_panning;
			float2 _Tex2_panning1;
			float _Float0;
			float _Float1;
			CBUFFER_END


            struct VertexInput
			{
				float3 positionOS : POSITION;
				float3 normal : NORMAL;
				float4 tangent : TANGENT;
				float4 ase_texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};


			struct VertexOutput
			{
				float4 positionCS : SV_POSITION;
				float4 ase_texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

            int _ObjectId;
            int _PassValue;

            struct SurfaceDescription
			{
				float Alpha;
			};

			
			VertexOutput vert( VertexInput v )
			{
				VertexOutput o;
				ZERO_INITIALIZE(VertexOutput, o);
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				o.ase_texcoord.xy = v.ase_texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord.zw = 0;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.positionOS.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif
				float3 vertexValue = defaultVertexValue;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.positionOS.xyz = vertexValue;
				#else
					v.positionOS.xyz += vertexValue;
				#endif


				float3 positionWS = TransformObjectToWorld(v.positionOS);
				o.positionCS = TransformWorldToHClip(positionWS);
				return o;
			}

			half4 frag( VertexOutput IN ) : SV_TARGET
			{
				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN);

				float2 texCoord117 = IN.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 panner118 = ( 1.0 * _Time.y * _Tex1_panning + texCoord117);
				float4 tex2DNode129 = tex2D( _Tex1_MaskT, panner118 );
				float temp_output_130_0 = ( tex2DNode129.r * tex2DNode129.a );
				float2 uv_Tex2_NoiseT = IN.ase_texcoord.xy * _Tex2_NoiseT_ST.xy + _Tex2_NoiseT_ST.zw;
				float2 panner127 = ( 1.0 * _Time.y * _Tex2_panning1 + uv_Tex2_NoiseT);
				float4 tex2DNode21 = tex2D( _Tex1_MaskT, ( float4( panner118, 0.0 , 0.0 ) + ( tex2D( _Tex2_NoiseT, panner127 ) * _Float1 ) ).rg );
				float temp_output_23_0 = ( tex2DNode21.r * tex2DNode21.a );
				float temp_output_133_0 = ( saturate( ( temp_output_23_0 - temp_output_130_0 ) ) + saturate( ( temp_output_130_0 - temp_output_23_0 ) ) );
				
				SurfaceDescription surfaceDescription = (SurfaceDescription)0;
				surfaceDescription.Alpha = saturate( ( temp_output_130_0 + temp_output_133_0 + temp_output_23_0 ) );

				#if _ALPHATEST_ON
					float alphaClipThreshold = 0.01f;
					#if ALPHA_CLIP_THRESHOLD
						alphaClipThreshold = surfaceDescription.AlphaClipThreshold;
					#endif
					clip(surfaceDescription.Alpha - alphaClipThreshold);
				#endif

				half4 outColor = half4(_ObjectId, _PassValue, 1.0, 1.0);
				return outColor;
			}

            ENDHLSL
        }

		
        Pass
        {
			
            Name "ScenePickingPass"
            Tags { "LightMode"="Picking" }

			Cull Off
			Blend Off
			ZTest LEqual
			ZWrite On


            HLSLPROGRAM

			#define ASE_SRP_VERSION 120111


			#pragma vertex vert
			#pragma fragment frag

            #define _SURFACE_TYPE_TRANSPARENT 1
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT

            #define SHADERPASS SHADERPASS_DEPTHONLY
			#define SCENEPICKINGPASS 1

            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"

        	

			sampler2D _Tex1_MaskT;
			sampler2D _Tex2_NoiseT;
			CBUFFER_START( UnityPerMaterial )
			float4 _Tex2_NoiseT_ST;
			float4 _Color0;
			float2 _Vector0;
			float2 _Tex1_panning;
			float2 _Tex2_panning1;
			float _Float0;
			float _Float1;
			CBUFFER_END


            struct VertexInput
			{
				float3 positionOS : POSITION;
				float3 normal : NORMAL;
				float4 tangent : TANGENT;
				float4 ase_texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 positionCS : SV_POSITION;
				float4 ase_texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

            float4 _SelectionID;

            struct SurfaceDescription
			{
				float Alpha;
			};

   			
			VertexOutput vert( VertexInput v  )
			{
				VertexOutput o;
				ZERO_INITIALIZE(VertexOutput, o);

				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				o.ase_texcoord.xy = v.ase_texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord.zw = 0;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.positionOS.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif
				float3 vertexValue = defaultVertexValue;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.positionOS.xyz = vertexValue;
				#else
					v.positionOS.xyz += vertexValue;
				#endif

				float3 positionWS = TransformObjectToWorld(v.positionOS);
				o.positionCS = TransformWorldToHClip(positionWS);

				return o;
			}

			half4 frag(VertexOutput IN ) : SV_TARGET
			{
				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN);

				float2 texCoord117 = IN.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 panner118 = ( 1.0 * _Time.y * _Tex1_panning + texCoord117);
				float4 tex2DNode129 = tex2D( _Tex1_MaskT, panner118 );
				float temp_output_130_0 = ( tex2DNode129.r * tex2DNode129.a );
				float2 uv_Tex2_NoiseT = IN.ase_texcoord.xy * _Tex2_NoiseT_ST.xy + _Tex2_NoiseT_ST.zw;
				float2 panner127 = ( 1.0 * _Time.y * _Tex2_panning1 + uv_Tex2_NoiseT);
				float4 tex2DNode21 = tex2D( _Tex1_MaskT, ( float4( panner118, 0.0 , 0.0 ) + ( tex2D( _Tex2_NoiseT, panner127 ) * _Float1 ) ).rg );
				float temp_output_23_0 = ( tex2DNode21.r * tex2DNode21.a );
				float temp_output_133_0 = ( saturate( ( temp_output_23_0 - temp_output_130_0 ) ) + saturate( ( temp_output_130_0 - temp_output_23_0 ) ) );
				
				SurfaceDescription surfaceDescription = (SurfaceDescription)0;
				surfaceDescription.Alpha = saturate( ( temp_output_130_0 + temp_output_133_0 + temp_output_23_0 ) );

				#if _ALPHATEST_ON
					float alphaClipThreshold = 0.01f;
					#if ALPHA_CLIP_THRESHOLD
						alphaClipThreshold = surfaceDescription.AlphaClipThreshold;
					#endif
					clip(surfaceDescription.Alpha - alphaClipThreshold);
				#endif

				half4 outColor = _SelectionID;
				return outColor;
			}


            ENDHLSL
        }

		
        Pass
        {
			
            Name "Sprite Forward"
            Tags { "LightMode"="UniversalForward" }

            HLSLPROGRAM

			#define ASE_SRP_VERSION 120111


			#pragma vertex vert
			#pragma fragment frag

            #define _SURFACE_TYPE_TRANSPARENT 1
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define ATTRIBUTES_NEED_TEXCOORD0
            #define ATTRIBUTES_NEED_COLOR
            #define VARYINGS_NEED_POSITION_WS
            #define VARYINGS_NEED_TEXCOORD0
            #define VARYINGS_NEED_COLOR

            #define SHADERPASS SHADERPASS_SPRITEFORWARD

            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"

			#pragma shader_feature_local _KEYWORD0_ON


			sampler2D _Tex0_baseT;
			sampler2D _Tex1_MaskT;
			sampler2D _Tex2_NoiseT;
			CBUFFER_START( UnityPerMaterial )
			float4 _Tex2_NoiseT_ST;
			float4 _Color0;
			float2 _Vector0;
			float2 _Tex1_panning;
			float2 _Tex2_panning1;
			float _Float0;
			float _Float1;
			CBUFFER_END


            struct VertexInput
			{
				float3 positionOS : POSITION;
				float3 normal : NORMAL;
				float4 tangent : TANGENT;
				float4 uv0 : TEXCOORD0;
				float4 color : COLOR;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};


			struct VertexOutput
			{
				float4 positionCS : SV_POSITION;
				float4 texCoord0 : TEXCOORD0;
				float3 positionWS : TEXCOORD1;
				float4 color : TEXCOORD2;
				float4 ase_texcoord3 : TEXCOORD3;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

            struct SurfaceDescription
			{
				float3 BaseColor;
				float Alpha;
				float3 NormalTS;
			};

			#include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/SurfaceData2D.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Debug/Debugging2D.hlsl"

			
			VertexOutput vert( VertexInput v  )
			{
				VertexOutput o;
				ZERO_INITIALIZE(VertexOutput, o);


				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				float4 ase_clipPos = TransformObjectToHClip((v.positionOS).xyz);
				float4 screenPos = ComputeScreenPos(ase_clipPos);
				o.ase_texcoord3 = screenPos;
				
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.positionOS.xyz;
				#else
					float3 defaultVertexValue = float3( 0, 0, 0 );
				#endif
				float3 vertexValue = defaultVertexValue;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.positionOS.xyz = vertexValue;
				#else
					v.positionOS.xyz += vertexValue;
				#endif
				v.normal = v.normal;
				v.tangent.xyz = v.tangent.xyz;


				float3 positionWS = TransformObjectToWorld(v.positionOS);

				o.positionCS = TransformWorldToHClip(positionWS);
				o.positionWS.xyz =  positionWS;
				o.texCoord0.xyzw =  v.uv0;
				o.color.xyzw =  v.color;

				return o;
			}

			half4 frag( VertexOutput IN  ) : SV_TARGET
			{
				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN);

				float4 screenPos = IN.ase_texcoord3;
				float2 panner24 = ( 1.0 * _Time.y * _Vector0 + ( (screenPos).xy * _Float0 ));
				float3 objToWorld112 = mul( GetObjectToWorldMatrix(), float4( float3( 0,0,0 ), 1 ) ).xyz;
				float2 texCoord117 = IN.texCoord0.xy * float2( 1,1 ) + float2( 0,0 );
				float2 panner118 = ( 1.0 * _Time.y * _Tex1_panning + texCoord117);
				float2 uv_Tex2_NoiseT = IN.texCoord0.xy * _Tex2_NoiseT_ST.xy + _Tex2_NoiseT_ST.zw;
				float2 panner127 = ( 1.0 * _Time.y * _Tex2_panning1 + uv_Tex2_NoiseT);
				float4 tex2DNode21 = tex2D( _Tex1_MaskT, ( float4( panner118, 0.0 , 0.0 ) + ( tex2D( _Tex2_NoiseT, panner127 ) * _Float1 ) ).rg );
				float temp_output_23_0 = ( tex2DNode21.r * tex2DNode21.a );
				float4 tex2DNode129 = tex2D( _Tex1_MaskT, panner118 );
				float temp_output_130_0 = ( tex2DNode129.r * tex2DNode129.a );
				float temp_output_133_0 = ( saturate( ( temp_output_23_0 - temp_output_130_0 ) ) + saturate( ( temp_output_130_0 - temp_output_23_0 ) ) );
				#ifdef _KEYWORD0_ON
				float staticSwitch147 = temp_output_133_0;
				#else
				float staticSwitch147 = 0.0;
				#endif
				
				SurfaceDescription surfaceDescription = (SurfaceDescription)0;
				surfaceDescription.BaseColor = ( tex2D( _Tex0_baseT, ( panner24 * distance( _WorldSpaceCameraPos , objToWorld112 ) ) ) + ( saturate( staticSwitch147 ) * _Color0 ) ).rgb;
				surfaceDescription.NormalTS = float3(0.0f, 0.0f, 1.0f);
				surfaceDescription.Alpha = saturate( ( temp_output_130_0 + temp_output_133_0 + temp_output_23_0 ) );


				half4 color = half4(surfaceDescription.BaseColor, surfaceDescription.Alpha);

				#if defined(DEBUG_DISPLAY)
				SurfaceData2D surfaceData;
				InitializeSurfaceData(color.rgb, color.a, surfaceData);
				InputData2D inputData;
				InitializeInputData(IN.positionWS.xy, half2(IN.texCoord0.xy), inputData);
				half4 debugColor = 0;

				SETUP_DEBUG_DATA_2D(inputData, IN.positionWS);

				if (CanDebugOverrideOutputColor(surfaceData, inputData, debugColor))
				{
					return debugColor;
				}
				#endif

				color *= IN.color;
				return color;
			}


            ENDHLSL
        }
    }
    CustomEditor "UnityEditor.ShaderGraph.GenericShaderGraphMaterialGUI"
    FallBack "Hidden/Shader Graph/FallbackError"
	
	Fallback Off
}
/*ASEBEGIN
Version=19303
Node;AmplifyShaderEditor.TextureCoordinatesNode;125;-2144,1536;Inherit;False;0;122;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;126;-2080,1696;Inherit;False;Property;_Tex2_panning1;Tex2_panning;5;0;Create;False;0;0;0;False;0;False;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.PannerNode;127;-1760,1552;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;117;-1520,1088;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;119;-1456,1248;Inherit;False;Property;_Tex1_panning;Tex1_panning;3;0;Create;True;0;0;0;False;0;False;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;124;-1296,1712;Inherit;False;Property;_Float1;offset_Tex2_Tilling;6;0;Create;False;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;122;-1456,1472;Inherit;True;Property;_Tex2_NoiseT;Tex2_NoiseT;4;0;Create;True;0;0;0;False;0;False;-1;a201c45e8496d0b429f47c9587cb7316;a201c45e8496d0b429f47c9587cb7316;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;123;-1024,1472;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.PannerNode;118;-1216,1104;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;121;-800,1040;Inherit;False;2;2;0;FLOAT2;0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TexturePropertyNode;128;-864,688;Inherit;True;Property;_Tex1_MaskT;Tex1_MaskT;7;0;Create;True;0;0;0;False;0;False;323b7a0418603ab4f9dd5e2622796aa4;323b7a0418603ab4f9dd5e2622796aa4;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.SamplerNode;21;-544,688;Inherit;True;Property;_TextureSample1;Texture Sample 1;3;0;Create;True;0;0;0;False;0;False;-1;2aa0b43652b98954e8d115f1eafbba3b;2aa0b43652b98954e8d115f1eafbba3b;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;129;-512,976;Inherit;True;Property;_TextureSample3;Texture Sample 1;3;0;Create;True;0;0;0;False;0;False;-1;2aa0b43652b98954e8d115f1eafbba3b;2aa0b43652b98954e8d115f1eafbba3b;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;23;-128,624;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;130;-125.0714,979.6449;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;131;208,624;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;132;224,960;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScreenPosInputsNode;1;-2080,-96;Float;False;1;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;29;-1808,288;Inherit;False;Property;_Float0;Tex0_Tiling;2;0;Create;False;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SwizzleNode;27;-1744,96;Inherit;False;FLOAT2;0;1;2;3;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SaturateNode;135;432,768;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;136;448,912;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;26;-1568,336;Inherit;False;Property;_Vector0;Tex0_Panning;1;0;Create;False;0;0;0;False;0;False;1,1;1,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;28;-1552,160;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.WorldSpaceCameraPos;111;-1424,544;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.TransformPositionNode;112;-1440,800;Inherit;False;Object;World;False;Fast;True;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleAddOpNode;133;608,768;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;146;928,912;Inherit;False;Constant;_Float2;Float 2;9;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;24;-1216,144;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DistanceOpNode;114;-1104,672;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;147;960,1024;Inherit;False;Property;_Keyword0;Keyword 0;9;0;Create;True;0;0;0;False;0;False;0;1;1;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;115;-912,496;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SaturateNode;148;1200,1008;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;140;960,1248;Inherit;False;Property;_Color0;Color 0;8;1;[HDR];Create;False;0;0;0;False;0;False;1,1,1,0;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;144;1120,544;Inherit;True;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;149;1424,960;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;2;-576,160;Inherit;True;Property;_Tex0_baseT;Tex0_baseT;0;0;Create;True;0;0;0;False;0;False;-1;d27e3e0a50b973047ae548c49c2c649b;d27e3e0a50b973047ae548c49c2c649b;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TexCoordVertexDataNode;36;-2192,112;Inherit;False;0;2;0;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;137;672,336;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;145;1408,544;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;106;0,0;Float;False;False;-1;2;UnityEditor.ShaderGraph.GenericShaderGraphMaterialGUI;0;1;New Amplify Shader;ece0159bad6633944bf6b818f4dd296c;True;Sprite Lit;0;0;Sprite Lit;0;False;True;2;5;False;;10;False;;3;1;False;;10;False;;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;2;False;;True;3;False;;True;True;0;False;;0;False;;True;5;RenderPipeline=UniversalPipeline;RenderType=Transparent=RenderType;UniversalMaterialType=Lit;Queue=Transparent=Queue=0;ShaderGraphShader=true;True;0;True;12;all;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=Universal2D;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;107;0,0;Float;False;False;-1;2;UnityEditor.ShaderGraph.GenericShaderGraphMaterialGUI;0;1;New Amplify Shader;ece0159bad6633944bf6b818f4dd296c;True;Sprite Normal;0;1;Sprite Normal;0;False;True;2;5;False;;10;False;;3;1;False;;10;False;;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;2;False;;True;3;False;;True;True;0;False;;0;False;;True;5;RenderPipeline=UniversalPipeline;RenderType=Transparent=RenderType;UniversalMaterialType=Lit;Queue=Transparent=Queue=0;ShaderGraphShader=true;True;0;True;12;all;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=NormalsRendering;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;108;0,0;Float;False;False;-1;2;UnityEditor.ShaderGraph.GenericShaderGraphMaterialGUI;0;1;New Amplify Shader;ece0159bad6633944bf6b818f4dd296c;True;SceneSelectionPass;0;2;SceneSelectionPass;0;False;True;2;5;False;;10;False;;3;1;False;;10;False;;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;2;False;;True;3;False;;True;True;0;False;;0;False;;True;5;RenderPipeline=UniversalPipeline;RenderType=Transparent=RenderType;UniversalMaterialType=Lit;Queue=Transparent=Queue=0;ShaderGraphShader=true;True;0;True;12;all;0;False;True;0;1;False;;0;False;;0;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;;False;False;False;False;False;False;False;False;False;False;False;True;1;False;;True;3;False;;False;True;1;LightMode=SceneSelectionPass;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;109;0,0;Float;False;False;-1;2;UnityEditor.ShaderGraph.GenericShaderGraphMaterialGUI;0;1;New Amplify Shader;ece0159bad6633944bf6b818f4dd296c;True;ScenePickingPass;0;3;ScenePickingPass;0;False;True;2;5;False;;10;False;;3;1;False;;10;False;;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;2;False;;True;3;False;;True;True;0;False;;0;False;;True;5;RenderPipeline=UniversalPipeline;RenderType=Transparent=RenderType;UniversalMaterialType=Lit;Queue=Transparent=Queue=0;ShaderGraphShader=true;True;0;True;12;all;0;False;True;0;1;False;;0;False;;0;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;;False;False;False;False;False;False;False;False;False;False;False;True;1;False;;True;3;False;;False;True;1;LightMode=Picking;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;110;1616,352;Float;False;True;-1;2;UnityEditor.ShaderGraph.GenericShaderGraphMaterialGUI;0;17;portal;ece0159bad6633944bf6b818f4dd296c;True;Sprite Forward;0;4;Sprite Forward;6;False;True;2;5;False;;10;False;;3;1;False;;10;False;;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;2;False;;True;3;False;;True;True;0;False;;0;False;;True;5;RenderPipeline=UniversalPipeline;RenderType=Transparent=RenderType;UniversalMaterialType=Lit;Queue=Transparent=Queue=0;ShaderGraphShader=true;True;0;True;12;all;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=UniversalForward;False;False;0;;0;0;Standard;2;Vertex Position;1;0;Debug Display;0;0;0;5;True;True;True;True;True;False;;False;0
WireConnection;127;0;125;0
WireConnection;127;2;126;0
WireConnection;122;1;127;0
WireConnection;123;0;122;0
WireConnection;123;1;124;0
WireConnection;118;0;117;0
WireConnection;118;2;119;0
WireConnection;121;0;118;0
WireConnection;121;1;123;0
WireConnection;21;0;128;0
WireConnection;21;1;121;0
WireConnection;129;0;128;0
WireConnection;129;1;118;0
WireConnection;23;0;21;1
WireConnection;23;1;21;4
WireConnection;130;0;129;1
WireConnection;130;1;129;4
WireConnection;131;0;23;0
WireConnection;131;1;130;0
WireConnection;132;0;130;0
WireConnection;132;1;23;0
WireConnection;27;0;1;0
WireConnection;135;0;131;0
WireConnection;136;0;132;0
WireConnection;28;0;27;0
WireConnection;28;1;29;0
WireConnection;133;0;135;0
WireConnection;133;1;136;0
WireConnection;24;0;28;0
WireConnection;24;2;26;0
WireConnection;114;0;111;0
WireConnection;114;1;112;0
WireConnection;147;1;146;0
WireConnection;147;0;133;0
WireConnection;115;0;24;0
WireConnection;115;1;114;0
WireConnection;148;0;147;0
WireConnection;144;0;130;0
WireConnection;144;1;133;0
WireConnection;144;2;23;0
WireConnection;149;0;148;0
WireConnection;149;1;140;0
WireConnection;2;1;115;0
WireConnection;137;0;2;0
WireConnection;137;1;149;0
WireConnection;145;0;144;0
WireConnection;110;0;137;0
WireConnection;110;2;145;0
ASEEND*/
//CHKSM=691B31DD0C6DE6C0BA316B1CFCBB116AF1E7DAE1