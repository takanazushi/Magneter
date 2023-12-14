using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class ResolutionSetter : MonoBehaviour
{
    [SerializeField, Header("テキストイメージの配列")]
    private Image[] texts = new Image[3];

    //[SerializeField]
    //private PlayerManager playerManager;

    private int count = 0;

    private void Start()
    {
        SetStartResolution();
    }

    /// <summary>
    /// 右向き△ボタンを押した時の処理
    /// </summary>
    public void IncrementCount()
    {
        count++;

        if (count > 2)
        {
            count = 0;
        }
        //count = (count + 1) % 3;

        Debug.Log(count);


        SetResolution();
    }

    /// <summary>
    /// 左向き△ボタンを押した時の処理
    /// </summary>
    public void DecrementCount()
    {
        count--;
        if (count < 0)
        {
            count = 2;
        }

        Debug.Log(count);

        //count = (count - 1 + 3) % 3;

        SetResolution();
    }


    //private void SetResolution()
    //{
    //    switch (count)
    //    {
    //        case 0:
    //            Screen.SetResolution(1920, 1080, false);
    //            texts[0].enabled = true;
    //            texts[1].enabled = false;
    //            texts[2].enabled = false;
    //            break;

    //        case 1:
    //            Screen.SetResolution(1280, 720, false);
    //            texts[0].enabled = false;
    //            texts[1].enabled = true;
    //            texts[2].enabled = false;
    //            break;

    //        case 2:
    //            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.width, true);
    //            texts[0].enabled = false;
    //            texts[1].enabled = false;
    //            texts[2].enabled = true;
    //            break;
    //    }
    //}

    private void SetStartResolution()
    {
        //横幅取得
        int screenWidth = Screen.currentResolution.width;

        //フルサイズかどうか
        bool isFullScreen = Screen.fullScreen;

        //初期化設定
        //横幅やフルサイズか否かで設定を行う
        if (isFullScreen)
        {
            count = 2;
            DisableTexts(0, 1);
        }
        else if (screenWidth == 1080)
        {
            count = 1;
            DisableTexts(0, 2);
        }
        else if (screenWidth == 1920)
        {
            count = 0;
            DisableTexts(1, 2);
        }
    }

    /// <summary>
    /// 解像度の設定と、表示UIの設定
    /// </summary>
    private void SetResolution()
    {
        switch (count)
        {
            case 0:
                SetAndEnableResolution(1920, 1080, false);
                DisableTexts(1, 2);
                break;

            case 1:
                SetAndEnableResolution(1280, 720, false);
                DisableTexts(0, 2);
                break;

            case 2:
                SetAndEnableResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
                DisableTexts(0, 1);
                break;
        }
    }

    /// <summary>
    /// 解像度の設定・対応するテキストを有効にする処理
    /// </summary>
    /// <param name="width">画面の横幅</param>
    /// <param name="height">画面の縦幅</param>
    /// <param name="flag">Ture：フルサイズ　False：ウィンドウ</param>
    private void SetAndEnableResolution(int width, int height, bool flag)
    {
        Screen.SetResolution(width, height, flag);
        texts[count].enabled = true;
    }

    /// <summary>
    /// 指定したテキストを非表示にする
    /// </summary>
    /// <param name="count">非表示にしたいテキストの添え字</param>
    private void DisableTexts(params int[] count)
    {
        foreach (int index in count)
        {
            texts[index].enabled = false;
        }
    }
}
