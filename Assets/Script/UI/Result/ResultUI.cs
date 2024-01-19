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

        int second = (int)ClearTime.instance.second + (ClearTime.instance.minute * 60);

        float s = 10 * (hp + second);
        s = (float)Math.Floor(s);

        score = (int)s;
    }

    private void ResultText()
    {
        textHP.text = GameManager.instance.HP.ToString();
        int minute = (int)ClearTime.instance.minute;
        int second = (int)ClearTime.instance.second;
        textTime.text = minute + ":" + second;

        textScore.text = score.ToString();
    }
}
