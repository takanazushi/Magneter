using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField, Header("ゲームの中間ポイントを保持 初期値-1")]
    public int checkpointNo = -1;
    [SerializeField, Header("デス後のプレイヤー初期HPを保持")]
    public int RestHP = 3;
    [SerializeField, Header("プレイヤーHPを保持")]
    public int HP = 3;

    /// <summary>
    /// カメラ開始番号
    /// </summary>
    [SerializeField, Header("開始カメラ")]
    private CinemachineVirtualCameraBase StartCamera;

    /// <summary>
    /// スタート時のカメラ遷移中フラグ
    /// </summary>
    [SerializeField]
    bool Ster_Camera_end;
    public bool Is_Ster_camera_end
    {
        get { return Ster_Camera_end; }
        set { Ster_Camera_end = value;}
    }

    public bool[] stageClearFlag = new bool[3] { true, false, false };


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //シーン間でオブジェクトが破棄されないようにする
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //インスタンスが存在する場合は破棄
            Destroy(gameObject);
        }

        //カメラの遷移開始
        SetStaetCamera();

    }

    public void SetStaetCamera()
    {
        StartCamera.Priority = 1;
    }

    //HP取得
    public int GetHP()
    {
        return HP;
    }
}
