using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_hanten : MonoBehaviour
{
    //磁力が反発しあっているときの反転処理

    /// <summary>
    /// 判定内に敵か壁がある
    /// </summary>
    [HideInInspector] public bool isOn = false;

    private GameObject parent;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //衝突したオブジェクトのMagnetComponentを取得
        Magnet magnet = collision.gameObject.GetComponent<Magnet>();
        //親オブジェクトを取得
        parent = transform.root.gameObject;
        //親のマグネット属性と衝突したマグネットの属性が一緒の場合
        if (parent.gameObject.GetComponent<Magnet>().Gat_Magnet() == magnet.Gat_Magnet())
        {
            isOn = true;
        }
        else //if(parent.gameObject.GetComponent<Magnet>().Gat_Magnet() != magnet.Gat_Magnet())
        {
            isOn = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isOn)
        {
            isOn = false;
        }
    }
}