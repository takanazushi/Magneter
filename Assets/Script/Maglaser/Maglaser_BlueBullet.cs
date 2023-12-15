using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Magnet;

public class Maglaser_BlueBullet : Maglaser_Bullet
{
    // Update is called once per frame
    private void Awake()
    {
        base.BulletStart();
    }


    void Update()
    {
        base.BulletUpdate();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //衝突したオブジェクトのMagnetComponentを取得
        Magnet magnet = collision.gameObject.GetComponent<Magnet>();
        
        //Nullでなければ磁石のタイプを設定
        if (magnet != null)
        {
            magnet.SetType_Magnat(Type_Magnet.N);
        }

        //弾の削除
        Destroy(gameObject);
        
        //デバッグ表示
        Debug.Log("青弾が当たりました！");
    }

}
