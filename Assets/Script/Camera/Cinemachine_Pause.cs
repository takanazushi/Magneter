using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Timeline;

/// <summary>
/// カメラにアタッチすると遷移した時遷移効果が終了するまでゲーム時間が停止します
/// </summary>

public class Cinemachine_Pause : CinemachineExtension
{

    ICinemachineCamera camera_d;

    GameTimeControl timeControl;

    /// <summary>
    /// 終了後の処理実行したかtrue：済
    /// </summary>
    bool End_Action;


    private void Start()
    {
        //仮
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

            //リセット
            End_Action = false;

            //遷移開始時ゲーム時間を停止
            timeControl.GameTime_Stop();
            Debug.Log("停止");
        }

        return base.OnTransitionFromCamera(fromCam, worldUp, deltaTime);
    }

    private void LateUpdate()
    {
        if (camera_d != null)
        {
            // 自身を管理しているBrainを取得
            CinemachineBrain brain = CinemachineCore.Instance.FindPotentialTargetBrain(VirtualCamera);
            // 現在動いているActiveBlendを取得
            var blend = brain.ActiveBlend;

            //終了後の処理実行済みの場合は無視
            if (blend != null&& !End_Action)
            {
                //残りの遷移値
                float da = blend.BlendWeight - 1.0f;

                //遷移が終了した時ゲーム時間を再開
                if (Mathf.Abs(da) < 0.05f)
                {
                    timeControl.GameTime_Start();
                    End_Action = true;
                }


            }
        }

    }



}
