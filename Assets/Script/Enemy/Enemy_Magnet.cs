using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Magnet;

/// <summary>
/// 接触したマグネット磁気を自身と同じにする
/// 名前変更した方がいいかも
/// </summary>
[RequireComponent(typeof(Magnet))]
public class Enemy_Magnet : MonoBehaviour
{
    private Magnet magnet;

    private void Start()
    {
        magnet=GetComponent<Magnet>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // MagnetBox_Square から始まるものを処理
        if (collision.gameObject.name.StartsWith("MagnetBox_Square"))
        {
            //衝突したオブジェクトのMagnetComponentを取得
            Magnet collisionMagnet = collision.gameObject.GetComponent<Magnet>();

            //Nullでなければ磁石のタイプを設定
            if (magnet != null && 
                collisionMagnet != null && 
                collisionMagnet.PuroTypeManet == Type_Magnet.None) 
            {
                // 極の種類を設定
                collisionMagnet.SetType_Magnat(magnet.PuroTypeManet);
            }
        }

        if (collision.gameObject.name.StartsWith("MagnetBox_Rectangle"))
        {
            //衝突したオブジェクトのMagnetComponentを取得
            Magnet collisionMagnet = collision.gameObject.GetComponent<Magnet>();

            //Nullでなければ磁石のタイプを設定
            if (magnet != null &&
                collisionMagnet != null &&
                collisionMagnet.PuroTypeManet == Type_Magnet.None)
            {
                // 極の種類を設定
                collisionMagnet.SetType_Magnat(magnet.PuroTypeManet);
            }
        }

        if (collision.gameObject.name.StartsWith("MagnetBox_Slender"))
        {
            //衝突したオブジェクトのMagnetComponentを取得
            Magnet collisionMagnet = collision.gameObject.GetComponent<Magnet>();

            //Nullでなければ磁石のタイプを設定
            if (magnet != null &&
                collisionMagnet != null &&
                collisionMagnet.PuroTypeManet == Type_Magnet.None)
            {
                // 極の種類を設定
                collisionMagnet.SetType_Magnat(magnet.PuroTypeManet);
            }
        }
    }

}
