using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_HP_UI : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField, Header("HPアイコン")]
    private GameObject playerIcon;

    private int beforeHP;

    public bool Frameflg;

    private void Start()
    {
        //HP取得
        beforeHP = GameManager.instance.RestHP;
        CreateHPIcon();
    }

    private void Update()
    {
        ShowHPIcon();
    }

    //アイコン作成
    private void CreateHPIcon()
    {
        for (int i = 0; i < beforeHP; i++)
        {
            //playerIconに入ってる画像を生成
            GameObject playerHPObj = Instantiate(playerIcon);
            //オブジェクトHPを親にする
            playerHPObj.transform.parent = transform;
        }
    }

    //HP表示
    private void ShowHPIcon()
    {
        //元のHPと同じときはスルー
        if (beforeHP == GameManager.instance.GetHP() || Frameflg)
        {
            return;
        }

        //HPが変化したら
        //子(playerIcon)のコンポーネント取得
        Image[] icons = transform.GetComponentsInChildren<Image>();

        for (int i = 0; i < icons.Length; i++)
        {
            //HPの表示数を切り替える
            icons[i].gameObject.SetActive(i < GameManager.instance.HP);
        }

        //HP取得
        beforeHP = GameManager.instance.GetHP();
    }
}
