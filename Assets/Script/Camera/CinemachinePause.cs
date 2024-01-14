using Cinemachine;
using System.Collections;
using UnityEngine;

/// <summary>
/// �J�����ɃA�^�b�`����ƑJ�ڂ������J�ڌ��ʂ��I������܂ŃQ�[�����Ԃ���~���܂�
/// </summary>

public class CinemachinePause : CinemachineExtension
{
    ICinemachineCamera camera_d;
    GameTimeControl timeControl;

    private void Start()
    {
        //��
        timeControl = GameObject.Find("Game_Time").GetComponent<GameTimeControl>();
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
    }

    public override bool OnTransitionFromCamera(ICinemachineCamera fromCam, Vector3 worldUp, float deltaTime)
    {
        if (fromCam != null)
        {
            camera_d = fromCam;

            //�J�ڊJ�n���Q�[�����Ԃ��~
            timeControl.GameTime_Stop();
        }

        return base.OnTransitionFromCamera(fromCam, worldUp, deltaTime);
    }

    private void LateUpdate()
    {
        if (camera_d != null)
        {
            // ���g���Ǘ����Ă���Brain���擾
            CinemachineBrain brain = CinemachineCore.Instance.FindPotentialTargetBrain(VirtualCamera);
            // ���ݓ����Ă���ActiveBlend���擾
            var blend = brain.ActiveBlend;

            if (blend != null)
            {

                float da = blend.BlendWeight - 1.0f;

                //�J�ڂ��I���������Q�[�����Ԃ��ĊJ
                if (Mathf.Abs(da) < 0.001f)
                {
                    //����������s����Ă��܂�
                    timeControl.GameTime_Start();
                }

            }
        }

    }
}
