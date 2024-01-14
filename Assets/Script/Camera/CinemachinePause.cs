using Cinemachine;
using System.Collections;
using UnityEngine;

/// <summary>
/// カメラにアタッチすると遷移した時遷移効果が終了するまでゲーム時間が停止します
/// </summary>

public class CinemachinePause : CinemachineExtension
{
    ICinemachineCamera camera_d;
    GameTimeControl timeControl;

    private void Start()
    {
        //仮
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

            //遷移開始時ゲーム時間を停止
            timeControl.GameTime_Stop();
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

            if (blend != null)
            {

                float da = blend.BlendWeight - 1.0f;

                //遷移が終了した時ゲーム時間を再開
                if (Mathf.Abs(da) < 0.001f)
                {
                    //※複数回実行されています
                    timeControl.GameTime_Start();
                }

            }
        }

    }
}
