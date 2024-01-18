using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Menu : MonoBehaviour
{
    /// <summary>
    /// 背景灰色画像
    /// </summary>
    [SerializeField]
    GameObject back_ash;

    [SerializeField]
    List<GameObject> BottaList;



    //メニューフラグ
    bool menuflg = false;

    // Start is called before the first frame update
    void Start()
    {
        menuflg = false;
        back_ash.SetActive(false);

        foreach (GameObject botta in BottaList)
        {
            botta.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //スタート時のカメラ遷移終了後
        //ポーズ中でないとき
        //ポーズ中かつメニューが開かれている時
        if (Input.GetKeyDown(KeyCode.Escape) &&
            GameManager.instance.Is_Ster_camera_end)
        {
            if (menuflg)
            {
                MenuEnd();
            }
            else if(!GameTimeControl.instance.IsPaused)
            {
                MenuStart();
            }
        }

        if (menuflg)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {

            }

        }
    }

    /// <summary>
    /// メニュー終了
    /// </summary>
    public void MenuEnd()
    {
        GameTimeControl.instance.GameTime_Start();
        menuflg = false;
        back_ash.SetActive(false);

        foreach (GameObject botta in BottaList)
        {
            botta.SetActive(false);
        }
    }
    /// <summary>
    /// メニュー開始
    /// </summary>
    public void MenuStart()
    {
        GameTimeControl.instance.GameTime_Stop();
        menuflg = true;
        back_ash.SetActive(true);

        foreach (GameObject botta in BottaList)
        {
            botta.SetActive(true);
        }
    }

    /// <summary>
    /// ゲームリスタート
    /// </summary>
    public void GameReStart() 
    {
        GameTimeControl.instance.GameTime_Start();
        menuflg = false;
        back_ash.SetActive(false);

        foreach (GameObject botta in BottaList)
        {
            botta.SetActive(false);
        }
        GameManager.instance.checkpointNo = -1;
        GameManager.instance.Is_Ster_camera_end = false;
        GameManager.instance.ActiveSceneReset(SceneManager.GetActiveScene().name);
    }

    public void StageSelect_SceneLoad()
    {
        GameTimeControl.instance.GameTime_Start();
        GameManager.instance.ActiveSceneReset("StageSelect");
    }
}
