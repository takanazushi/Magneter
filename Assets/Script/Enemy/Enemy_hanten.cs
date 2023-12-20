using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_hanten : MonoBehaviour
{
    //このスクリプト使ってません。
    ///// <summary>
    ///// 判定内に敵か壁がある
    ///// </summary>
    //[HideInInspector] public bool isOn = false;

    private GameObject parent;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //衝突したオブジェクトのMagnetComponentを取得
        Magnet magnet = collision.gameObject.GetComponent<Magnet>();
        //親オブジェクトを取得
        parent = transform.root.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isOn)
        {
            isOn = false;
        }
    }
}