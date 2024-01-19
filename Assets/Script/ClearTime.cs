using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.SceneManagement;

public class ClearTime : MonoBehaviour
{
    //todo GameObjct Timerで使用

    public static ClearTime instance;

    private TextMeshProUGUI timerText;
    //タイマー
    public float second;
    //分
    public int minute;
    //時
    private int hour;

    //タイマーリセット
    private bool resetflg;

    // Start is called before the first frame update
    void Start()
    {
        resetflg = false;
        //経過時間読み込む
        ResumeTimer();
        timerText = GetComponent<TextMeshProUGUI>();
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

        if (SceneManager.GetActiveScene().name == "Title" || SceneManager.GetActiveScene().name == "StageSelect" || SceneManager.GetActiveScene().name == "Option")
        {
            ClearTime.instance.second = 0;
            ClearTime.instance.minute = 0;
            return;
        }
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
    }

    // Update is called once per frame
    void Update()
    {
        //falseの時に保存してるタイマーをリセット
        if(!resetflg)
        {
            PlayerPrefs.DeleteAll();
            resetflg = true;
        }
        //ゴールまでタイマー加算
        if (Goal_mng.instance != null&& !Goal_mng.instance.Is_Goal) 
        {
            second += Time.deltaTime;
        }

        //分が60を超えたら時に1を足す
        if (minute > 60)
        {
            hour++;
            minute = 0;
        }
        //秒が60を超えたら分に1を足す
        if (second > 60f)
        {
            minute += 1;
            second = 0;
        }
       
        //描画
        //timerText.text = hour.ToString() + ":" + minute.ToString("00") + ":" + second.ToString("f2");

    }

    public void ResumeTimer()
    {
        // 保存された経過時間を読み込み、secondに設定する
        second = PlayerPrefs.GetFloat("PreviousElapsedTime", 0f);
    }

    //private static void BuildPlayerHandler(BuildPlayerOptions options)
    //{
    //    PlayerPrefs.DeleteAll();
    //}
}
