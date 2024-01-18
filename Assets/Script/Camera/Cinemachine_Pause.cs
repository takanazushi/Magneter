using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Timeline;

/// <summary>
/// �J�����ɃA�^�b�`����ƑJ�ڂ������J�ڌ��ʂ��I������܂ŃQ�[�����Ԃ���~���܂�
/// </summary>

public class Cinemachine_Pause : CinemachineExtension
{

    ICinemachineCamera camera_d;

    GameTimeControl timeControl;

    /// <summary>
    /// �I����̏������s������true�F��
    /// </summary>
    bool End_Action;


    private void Start()
    {
        //��
        timeControl = GameObject.Find("GameManager").GetComponent<GameTimeControl>();
        End_Action = false;
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
    }

    public override bool OnTransitionFromCamera(ICinemachineCamera fromCam, Vector3 worldUp, float deltaTime)
    {
        if (fromCam != null)
        {
            camera_d = fromCam;

            //���Z�b�g
            End_Action = false;

            //�J�ڊJ�n���Q�[�����Ԃ��~
            timeControl.GameTime_Stop();
            Debug.Log("��~");
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

            //�I����̏������s�ς݂̏ꍇ�͖���
            if (blend != null&& !End_Action)
            {
                //�c��̑J�ڒl
                float da = blend.BlendWeight - 1.0f;

                //�J�ڂ��I���������Q�[�����Ԃ��ĊJ
                if (Mathf.Abs(da) < 0.05f)
                {
                    timeControl.GameTime_Start();
                    End_Action = true;
                }


            }
        }

    }



}
