using Cinemachine;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 遷移開始した時イベントを実行しすべて終了した時Is_Ster_camera_endをtrueにします
/// </summary>

public class Cinemachine_End : CinemachineExtension
{
    ICinemachineCamera camera_d;

    [SerializeField,Header("終了後のカメラ")]
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

            //イベント実行
            evnt?.Invoke(StartCamera_End);

            //カットで遷移するので次のカメラの優先度をセット
            End_virtualCamera.Priority = 1;
        }

        return base.OnTransitionFromCamera(fromCam, worldUp, deltaTime);
    }

    /// <summary>
    /// Is_Ster_camera_endを更新
    /// コールバックとして呼び出す
    /// </summary>
    private void StartCamera_End()
    {
        //自身が呼び出されたカウント
        count++;

        //イベントの数分呼び出された時
        if (count >= evnt.GetPersistentEventCount())
        {
            GameManager.instance.Is_Ster_camera_end = true;
            //Debug.Log("jtdj");
            count = 0;
        }

    }
}

