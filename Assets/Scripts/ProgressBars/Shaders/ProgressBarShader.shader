Shader "UI/Custom/ProgressBar"{
	Properties{
		[PerRendererData] _MainTex("Textura Base", 2D ) = "white"{}
		_TransVal("Transparencia", Range(0,1)) = 0.5
		_TransMask("Máscara de transparencia", 2D) = "white"{}
		_ProgressionMask("Máscara de avance", 2D) = "white"{}
		_ProgressionVal("Avance", Range(0,1)) = 0.5
		_Color2("Color del avance", Color) = (0,0,0,1)

		 // required for UI.Mask
		 _Color ("Tint", Color) = (1,1,1,1)
         _StencilComp ("Stencil Comparison", Float) = 8
         _Stencil ("Stencil ID", Float) = 0
         _StencilOp ("Stencil Operation", Float) = 0
         _StencilWriteMask ("Stencil Write Mask", Float) = 255
         _StencilReadMask ("Stencil Read Mask", Float) = 255
         _ColorMask ("Color Mask", Float) = 15
	}

	SubShader{
		// required for UI.Mask
			 Tags
		 {
			 "Queue" = "Transparent"
			 "IgnoreProjector" = "True"
			 "RenderType" = "Transparent"
			 "PreviewType" = "Plane"
			 "CanUseSpriteAtlas" = "True"
		 }
         Stencil
         {
             Ref [_Stencil]
             Comp [_StencilComp]
             Pass [_StencilOp] 
             ReadMask [_StencilReadMask]
             WriteMask [_StencilWriteMask]
         }
		Cull Off
		Lighting Off
		ZWrite Off
		ZTest [unity_GUIZTestMode]
		Blend SrcAlpha OneMinusSrcAlpha
        ColorMask [_ColorMask]

        Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			#include "UnityUI.cginc"

			#pragma multi_compile __ UNITY_UI_ALPHACLIP
			
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
			
			fixed4 _Color;
			fixed4 _TextureSampleAdd;
			float4 _ClipRect;

			sampler2D _TransMask;
			sampler2D _ProgressionMask;
			float _ProgressionVal;
			float _TransVal;
			fixed4 _Color2;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.worldPosition = IN.vertex;
				OUT.vertex = mul(UNITY_MATRIX_MVP, OUT.worldPosition);

				OUT.texcoord = IN.texcoord;
				
				#ifdef UNITY_HALF_TEXEL_OFFSET
				OUT.vertex.xy += (_ScreenParams.zw-1.0)*float2(-1,1);
				#endif
				
				OUT.color = IN.color * _Color;
				
				return OUT;
			}

			sampler2D _MainTex;

			fixed4 frag(v2f IN) : SV_Target
			{
				half4 color = (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd) * IN.color;
				
				color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);

				//Inner Circle 
				half4 transUV = tex2D(_TransMask, IN.texcoord);
				if (transUV.r >= _TransVal) {
					color.a = 0.0;
				}
				#ifdef UNITY_UI_ALPHACLIP
				clip (color.a - 0.001);
				#endif

				//Bar Progression
				half4 progressUV = tex2D(_ProgressionMask, IN.texcoord);
				if (_ProgressionVal == 0) 
					return color;
				if (progressUV.r <= _ProgressionVal) {
					color.rgb = _Color2;
					
				}
				return color;
			}
		ENDCG
		}
		
	}
}