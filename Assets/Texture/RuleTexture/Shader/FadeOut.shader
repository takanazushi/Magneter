Shader "Unlit/FadeOut"
{
	Properties
	{
		//2D�̃e�N�X�`����`
		//"Sprite Texture"�̓��x���B2d��2D�e�N�X�`���ł��邱�Ƃ�����
		//=white�̓f�t�H���g�l�i���F�̃e�N�X�`���j��ݒ�ł���
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}

		//�J���[��`
		//"Tint"�̓��x��
		//RGBA�̐F�����i�[���Ă���B
		_Color("Tint", Color) = (1,1,1,1)

		//�����x���`
		//"Time"�̓��x��
		//Range�͒l�͈͎̔w��B0�`1�܂ł͈̔͂̒l�����󂯓���Ȃ�
		//0�����S�ɓ����A1�͕s����
		//=0�͊��S�ɓ����ȏ�Ԃ�����
		_Alpha("Time", Range(0, 1)) = 0
	}

	SubShader
	{
		//
		Tags
		{
			//�����_�����O�̗D�揇�ʂ�����
			//"Transparent"�͓����x�Ɋ�Â��đ��̌��̃I�u�W�F�N�g�ɕ`�悳���
			//�A���t�@�u�����f�B���O�������́i�[�x�o�b�t�@�ɏ������݂��Ȃ����́j�́A"Transparent"�̕�������
			//https://docs.unity3d.com/ja/2019.4/Manual/SL-SubShaderTags.html
			"Queue" = "Transparent"

			//�e�̉e�����󂯂邩�ǂ���������
			//True���ƁA�e�̉e�����󂯂邱�Ƃ͂Ȃ�
			"IgnoreProjector" = "True"

			//�ǂ̎�ނ̃����_�����O�I�u�W�F�N�g�Ȃ̂��ǂ���������
			//"Transparent"�͓����ȃI�u�W�F�N�g�Ƃ��ď��������
			"RenderType" = "Transparent"

			//Preview�łǂ̂悤�ɕ\������邩�ǂ���������
			//Plane���ƕ��ʂƂ��ď��������
			"PreviewType" = "Plane"

			//�X�v���C�g�A�g���X�i�����̃X�v���C�g��1�̃e�N�X�`���Ƃ��Ĉ�����j
			//�X�v���C�g�A�g���X�Ƃ��Ďg�p���邩�ǂ���������
			"CanUseSpriteAtlas" = "True"
	}

		//�J�����O�i�K�v�Ȃ����̂�`�悵�Ȃ��j�̐ݒ������
		//Off�̓J�����O�𖳌��ɂ��āA�S�Ă̖ʂ�`�悷��
		Cull Off

		//�A�e�v�Z�̐ݒ������
		//Off�͉A�e�v�Z�𖳌��ɂ���B���̉e�����󂯂Ȃ��悤�ɂȂ�
		Lighting Off

		//�[�x�o�b�t�@�ւ̏������ݐ���ݒ������
		//Off�͐[�x�o�b�t�@�ւ̏������݂𖳌��ɂ���
		ZWrite Off

		//�[�x�e�X�g�̕��@�𐧌䂷��
		//UI�p��material������Ă���ꍇ�͂��̐ݒ�ɂ���
		ZTest[unity_GUIZTestMode]

		//�t�H�O�̕`��ݒ������
		//Off�ɂ���ƃt�H�O�̉e�����󂯂Ȃ��Ȃ�
		Fog{ Mode Off }

		//�u�����f�B���O�i�����x�̌����j�̐ݒ������
		//SrcAlpha�̓I�u�W�F�N�g�̐F�Ɠ����x��\��
		//OneMinusSrcAlpha�͔w�i�̐F�Ɠ����x�̕⊮��\��
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			//�V�F�[�_�[�v���O�����̊J�n
			CGPROGRAM

			//vertex�F���_�V�F�[�_�[
			#pragma vertex vert

			//frag�F�t���O�����g�V�F�[�_�[
			#pragma fragment frag

			//UnityCG.cginc�̃C���N���[�h
			#include "UnityCG.cginc"

			//���_�f�[�^���i�[����\����
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

			// ���_�V�F�[�_�[�̊�{
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

			// �ʏ�̃t���O�����g�V�F�[�_�[
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
