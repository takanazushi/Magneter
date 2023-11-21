using System.Collections;
using System.Collections.Generic;
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
    public int HP;

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
}
