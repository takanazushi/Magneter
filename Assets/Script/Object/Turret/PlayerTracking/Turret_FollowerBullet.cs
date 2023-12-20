using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_FollowerBullet : MonoBehaviour
{
    //プレイヤーの座標格納
    private Transform target;

    //移動速度
    [SerializeField, Header("弾の発射スピード")]
    private float moveSpeed;

    //画面内
    private bool InField = false;

    private Rigidbody2D rb;

    private Vector3 bulletDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        target = GameObject.FindGameObjectWithTag("Player").transform;

        // 弾の方向を自機から敵へ向ける
        bulletDirection = target.position - transform.position;

        //10秒後にオブジェクトを破棄
        Destroy(gameObject, 10.0f);
    }

    //画面内で動かす
    private void OnBecameVisible()
    {
        //画面内にいるときtrue
        InField = true;
    }

    //画面外で消す処理
    private void OnBecameInvisible()
    {
        //消す
        Destroy(rb.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //画面内で実行
        if (InField)
        {
            MoveBullet();
        }
    }

    /// <summary>
    /// 弾の移動処理
    /// </summary>
    private void MoveBullet()
    {
        //向き固定
        bulletDirection.Normalize();

        //その向きにスピードを掛ける
        rb.velocity = bulletDirection * moveSpeed;
    }
}
