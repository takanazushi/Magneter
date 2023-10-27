using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    private Rigidbody2D rb;

    //画面内
    private bool InField = false;

    //Prefabsで複製する物を入れる
    public GameObject BulletObj;

    //プレイヤーの座標格納
    public Transform target;

    //移動速度
    public float moveSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //2.5秒後に実行する処理
        InvokeRepeating("Shot", 2.5f,2.5f);
    }

    //画面外で消す処理
    private void OnBecameInvisible()
    {
        //消す
        Destroy(rb.gameObject);
    }

    //画面内で動かす
    private void OnBecameVisible()
    {
        //画面内にいるときtrue
        InField = true;
    }
    void Shot()
    {
        //画面内で実行
        if (InField)
        {
            // 弾の方向を自機から敵へ向ける
            Vector3 bulletDirection = target.position - transform.position;
            //向き固定
            bulletDirection.Normalize();

            //複製処理
            GameObject obj = Instantiate(BulletObj) as GameObject;
            //名前をCircleにする
            obj.name = BulletObj.name;
            //その向きにスピードを掛ける
            rb.velocity = bulletDirection * moveSpeed;
            //10秒後に砲弾を破壊する
            Destroy(obj, 10.0f);
        }
    }
}
