using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    /// <summary>
    /// プレイヤーの死亡カウント
    /// </summary>
    private int Player_Dea_Count;
    public int Is_Player_Dea_Count
    {
        get { return Player_Dea_Count; }
        set { Player_Dea_Count=value; }
    }
    /// <summary>
    /// プレイヤーの死亡判定
    /// true:死んでいる
    /// </summary>
    private bool Player_Death;
    public bool Is_Player_Death
    {
        get { return Player_Death; }

        set 
        { 
            Player_Death=value;
            if (Player_Death)
            {
                //死亡カウント増加
                Player_Dea_Count++;
                Debug.Log("死亡カウント:" + Player_Dea_Count);

            }
        }
    }

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
        if (SceneManager.GetActiveScene().name == "Title" || 
            SceneManager.GetActiveScene().name == "StageSelect"||
            SceneManager.GetActiveScene().name == "Option"|| 
            SceneManager.GetActiveScene().name == "Result")
        {
            instance.Is_Ster_camera_end = false;
            return;
        }


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

        if (Image == null)
        {
            Debug.Log("フェードアウトのImageない");

            // GameObject(1)を見つける
            GameObject parentObject = GameObject.Find("Canvas");

            // Camera_Childを見つける
            Transform childObject = parentObject.transform.Find("FadeImage");

            // Start_Camera_Listを見つける
            GameObject fadeoutImage = childObject.Find("FadeOutImage").gameObject;

            // StartCameraに代入する
            Image = fadeoutImage;
        }

        //プレイヤーのHPをリセットする
        instance.HP = instance.RestHP;
        Player_Death = false;

        fadeOut = Image.GetComponent<FadeOut>();
        Image_Name = Image.name;

    }

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

        if (SceneManager.GetActiveScene().name == "Title" || SceneManager.GetActiveScene().name == "StageSelect" || SceneManager.GetActiveScene().name == "Option" || SceneManager.GetActiveScene().name == "Result")
        {
            return;
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
            fadeOut = Image.GetComponent<FadeOut>();
            Image_Name = Image.name;
        }

    }

    /// <summary>
    /// 現在のシーンを再度読み込む
    /// </summary>
    public void ActiveSceneReset(string scenename)
    {
        Player_PlayFlg = true;

        Debug.Log(fadeOut.name);
        StartCoroutine(fadeOut.Execute(scenename));

        //todo 前回の経過時間を保存
        PlayerPrefs.SetFloat("PreviousElapsedTime", ClearTime.instance.second);

    }

    public void SetStaetCamera()
    {
        if (SceneManager.GetActiveScene().name == "Title" || SceneManager.GetActiveScene().name == "StageSelect"|| SceneManager.GetActiveScene().name == "Option" || SceneManager.GetActiveScene().name == "Result")
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
