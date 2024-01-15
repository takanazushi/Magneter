using Cinemachine;
using System;
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

    /// <summary>
    /// プレイヤーの操作可能判定
    /// true:操作無効
    /// </summary>
    bool Player_PlayFlg;
    public bool Is_Player_StopFlg
    {
        get { return Player_PlayFlg; }
        set { Player_PlayFlg = value; }
    }

    [SerializeField, Tooltip("フェードアウト用画像")]
    private GameObject Image;
    private string Image_Name = "FadeOutImage";
    private FadeOut fadeOut;

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
        //プレイヤーのHPをリセットする
        GameManager.instance.HP = GameManager.instance.RestHP;

        if (StartCamera == null)
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

            //Debug.Log("ゲームマネージャー存在します。");
            //Debug.Log("プレイヤーのチェックポイント" + checkpointNo);
        }
        else
        {
            //インスタンスが存在する場合は破棄
            Destroy(gameObject);
        }

        if (!Ster_Camera_end)
        {
            //カメラの遷移開始
            SetStaetCamera();
        }

        SceneManager.activeSceneChanged += SetFadeOutOj;

        fadeOut = Image.GetComponent<FadeOut>();
        Image_Name = Image.name;
    }

    
    void SetFadeOutOj(Scene thisScene, Scene nextScene)
    {
        Image = GameObject.Find(Image_Name);

        if(Image != null)
        {
            Debug.Log("シーン着替え時：" + Image.name);
            fadeOut = Image.GetComponent<FadeOut>();
            Image_Name = Image.name;
        }

    }

    /// <summary>
    /// 現在のシーンを再度読み込む
    /// </summary>
    public void ActiveSceneReset()
    {
        Player_PlayFlg = true;

        StartCoroutine(fadeOut.Execute(SceneManager.GetActiveScene().name));

        //todo 前回の経過時間を保存
        PlayerPrefs.SetFloat("PreviousElapsedTime", ClearTime.instance.second);

        
        //現在のシーンを再度読み込む
        //Scene activeScene = SceneManager.GetActiveScene();
        //SceneManager.LoadScene(activeScene.name);
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
