using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Magnet;

public class Enemy_Magnet : MonoBehaviour
{
    public Magnet magnet;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // MagnetBox_Square から始まるものを処理
        if (collision.gameObject.name.StartsWith("MagnetBox_Square"))
        {
            //衝突したオブジェクトのMagnetComponentを取得
            Magnet collisionMagnet = collision.gameObject.GetComponent<Magnet>();
            // 極の種類を取得
            Type_Magnet currentType = collisionMagnet.PuroTypeManet;

            //Nullでなければ磁石のタイプを設定
            if (magnet != null && collisionMagnet != null && collisionMagnet.PuroTypeManet == Type_Magnet.None) 
            {
                // 極の種類を設定
                collisionMagnet.SetType_Magnat(magnet.PuroTypeManet);
            }

            //デバッグ表示
            Debug.Log("敵が箱に磁力付与！");
        }
    }

}
