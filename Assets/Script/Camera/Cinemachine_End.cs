using Cinemachine;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// �J�ڊJ�n�������C�x���g�����s�����ׂďI��������Is_Ster_camera_end��true�ɂ��܂�
/// </summary>

public class Cinemachine_End : CinemachineExtension
{
    ICinemachineCamera camera_d;

    [SerializeField,Header("�I����̃J����")]
    CinemachineVirtualCamera End_virtualCamera;

    [SerializeField]
    public UnityEvent<UnityAction> evnt;

    int count = 0;
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
    }

    public override bool OnTransitionFromCamera(ICinemachineCamera fromCam, Vector3 worldUp, float deltaTime)
    {
        if (fromCam != null)
        {
            camera_d = fromCam;

            //�C�x���g���s
            evnt?.Invoke(StartCamera_End);

            //�J�b�g�őJ�ڂ���̂Ŏ��̃J�����̗D��x���Z�b�g
            End_virtualCamera.Priority = 1;
        }

        return base.OnTransitionFromCamera(fromCam, worldUp, deltaTime);
    }

    /// <summary>
    /// Is_Ster_camera_end���X�V
    /// �R�[���o�b�N�Ƃ��ČĂяo��
    /// </summary>
    private void StartCamera_End()
    {
        //���g���Ăяo���ꂽ�J�E���g
        count++;

        //�C�x���g�̐����Ăяo���ꂽ��
        if (count >= evnt.GetPersistentEventCount())
        {
            GameManager.instance.Is_Ster_camera_end = true;
            //Debug.Log("jtdj");
            count = 0;
        }

    }
}

