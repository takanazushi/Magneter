using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ResultUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textHP;

    [SerializeField]
    TextMeshProUGUI textTime;

    [SerializeField]
    TextMeshProUGUI textDieCount;

    [SerializeField]
    TextMeshProUGUI textScore;

    private int score;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance == null)
        {
            Debug.LogError("ゲームマネージャーがありませんYo");
            return;
        }

        if (ClearTime.instance == null)
        {
            Debug.LogError("クリアタイムないよ！！");
            return;
        }

        ResultCheck();

        ResultText();
    }

    private void ResultCheck()
    {
        int hp = GameManager.instance.HP;

        int CountDie = GameManager.instance.Is_Player_Dea_Count;

        int second = (int)ClearTime.instance.second + (ClearTime.instance.minute * 60);

        float s = hp * 1000 + (3600 - second) - CountDie * 1000;
        s = (float)Math.Floor(s);

        if (s < 0)
        {
            s = 0;
        }

        score = (int)s;
    }

    private void ResultText()
    {
        textHP.text = GameManager.instance.HP.ToString();
        textDieCount.text=GameManager.instance.Is_Player_Dea_Count.ToString();
        int minute = (int)ClearTime.instance.minute;
        int second = (int)ClearTime.instance.second;

        if (second < 10)
        {
            textTime.text = minute + ":0" + second;
        }
        else
        {
            textTime.text = minute + ":" + second;
        }

        textScore.text = score.ToString();
    }
}
