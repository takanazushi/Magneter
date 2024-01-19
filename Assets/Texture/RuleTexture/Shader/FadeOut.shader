Shader "Unlit/FadeOut"
{
	Properties
	{
		//2Dのテクスチャ定義
		//"Sprite Texture"はラベル。2dは2Dテクスチャであることを示す
		//=whiteはデフォルト値（白色のテクスチャ）を設定できる
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}

		//カラー定義
		//"Tint"はラベル
		//RGBAの色情報を格納している。
		_Color("Tint", Color) = (1,1,1,1)

		//透明度を定義
		//"Time"はラベル
		//Rangeは値の範囲指定。0～1までの範囲の値しか受け入れない
		//0が完全に透明、1は不透明
		//=0は完全に透明な状態を示す
		_Alpha("Time", Range(0, 1)) = 0
	}

	SubShader
	{
		//
		Tags
		{
			//レンダリングの優先順位を示す
			//"Transparent"は透明度に基づいて他の後ろのオブジェクトに描画される
			//アルファブレンディングされるもの（深度バッファに書き込みしないもの）は、"Transparent"の方がいい
			//https://docs.unity3d.com/ja/2019.4/Manual/SL-SubShaderTags.html
			"Queue" = "Transparent"

			//影の影響を受けるかどうかを示す
			//Trueだと、影の影響を受けることはない
			"IgnoreProjector" = "True"

			//どの種類のレンダリングオブジェクトなのかどうかを示す
			//"Transparent"は透明なオブジェクトとして処理される
			"RenderType" = "Transparent"

			//Previewでどのように表示されるかどうかを示す
			//Planeだと平面として処理される
			"PreviewType" = "Plane"

			//スプライトアトラス（複数のスプライトを1つのテクスチャとして扱うやつ）
			//スプライトアトラスとして使用するかどうかを示す
			"CanUseSpriteAtlas" = "True"
	}

		//カリング（必要ないものを描画しない）の設定を示す
		//Offはカリングを無効にして、全ての面を描画する
		Cull Off

		//陰影計算の設定を示す
		//Offは陰影計算を無効にする。光の影響を受けないようになる
		Lighting Off

		//深度バッファへの書き込み制御設定を示す
		//Offは深度バッファへの書き込みを無効にする
		ZWrite Off

		//深度テストの方法を制御する
		//UI用にmaterialを作っている場合はこの設定にする
		ZTest[unity_GUIZTestMode]

		//フォグの描画設定を示す
		//Offにするとフォグの影響を受けなくなる
		Fog{ Mode Off }

		//ブレンディング（透明度の結合）の設定を示す
		//SrcAlphaはオブジェクトの色と透明度を表す
		//OneMinusSrcAlphaは背景の色と透明度の補完を表す
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			//シェーダープログラムの開始
			CGPROGRAM

			//vertex：頂点シェーダー
			#pragma vertex vert

			//frag：フラグメントシェーダー
			#pragma fragment frag

			//UnityCG.cgincのインクルード
			#include "UnityCG.cginc"

			//頂点データを格納する構造体
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				half2 texcoord  : TEXCOORD0;
			};

			fixed4 _Color;
			fixed _Alpha;
			sampler2D _MainTex;

			// 頂点シェーダーの基本
			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
			#ifdef UNITY_HALF_TEXEL_OFFSET
				OUT.vertex.xy += (_ScreenParams.zw - 1.0) * float2(-1,1);
			#endif
				return OUT;
			}

			// 通常のフラグメントシェーダー
			fixed4 frag(v2f IN) : SV_Target
			{
				half alpha = tex2D(_MainTex, IN.texcoord).a;
				alpha = saturate(alpha + (_Alpha * 2 - 1));
				return fixed4(_Color.r, _Color.g, _Color.b, alpha);
			}
			ENDCG
		}
	}

		FallBack "UI/Default"
}
