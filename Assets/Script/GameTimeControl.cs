using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeControl : MonoBehaviour
{
    public static GameTimeControl instance;

    /// <summary>
    /// ポーズフラグ
    /// true:ポーズ中
    /// </summary>
    [SerializeField]
    private bool isPaused = false;
    public bool IsPaused
    {
        get { return isPaused; }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            //インスタンスが存在する場合は破棄
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        //確認用キー
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    if (!isPaused)
        //    {
        //        GameTime_Stop();
        //    }
        //    else
        //    {
        //        GameTime_Start();
        //    }
        //}

    }

    /// <summary>
    /// ゲーム時間をストップ
    /// </summary>
    public void GameTime_Stop()
    {
        // ゲームを一時停止
        Time.timeScale = 0;
        isPaused = true;
        // 物理演算も停止
        Time.fixedDeltaTime = 0;
        Debug.Log("Stop");

    }

    /// <summary>
    /// ゲーム時間をスタート
    /// </summary>
    public void GameTime_Start()
    {
        // ゲームを再開
        Time.timeScale = 1;
        isPaused = false;
        // 物理演算も再開
        Time.fixedDeltaTime = 0.02f; // 通常の値に戻す
        Debug.Log("Start");
    }

}
