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

    public bool[] stageClearFlag = new bool[4] { true, false, false, false };

    //Sceneが有効になった時
    private void OnEnable()
    {
        //自動的にMethod呼び出し
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //Sceneが無効になった時
    private void OnDisable()
    {
        //自動的にMethod削除
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //Sceneが読み込まれる度に呼び出し
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name == "Title" || SceneManager.GetActiveScene().name == "StageSelect"|| SceneManager.GetActiveScene().name == "Option")
        {
            return;
        }
        else if (StartCamera == null && checkpointNo == -1)
        {

            Debug.Log("StartCameraない");

            // GameObject(1)を見つける
            GameObject parentObject = GameObject.Find("Camera");

            // Camera_Childを見つける
            Transform childObject = parentObject.transform.Find("Camera_Control");

            // Start_Camera_Listを見つける
            CinemachineVirtualCameraBase startCameraList = childObject.Find("Start_Camera_List").GetComponent<CinemachineVirtualCameraBase>();

            // StartCameraに代入する
            StartCamera = startCameraList;
        }
    }

    void Awake()
    {
       
        if (instance == null)
        {
            instance = this;
            //シーン間でオブジェクトが破棄されないようにする
            DontDestroyOnLoad(gameObject);

            Debug.Log("ゲームマネージャー存在します。");
            Debug.Log("プレイヤーのチェックポイント" + checkpointNo);
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
        if (SceneManager.GetActiveScene().name == "Title" || SceneManager.GetActiveScene().name == "StageSelect"|| SceneManager.GetActiveScene().name == "Option")
        {
            return;
        }

        StartCamera.Priority = 1;


    }

    //HP取得
    public int GetHP()
    {
        return HP;
    }
}
