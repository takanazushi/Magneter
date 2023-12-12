using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_WalkFall : MonoBehaviour
{
    //コメントしてるやつは元々書いてたやつです。
    //todo振ってるところは変えたところです。

    [SerializeField, Header("敵の速度")]
    private float speed;

    [SerializeField]
    private bool Left;//左向き

    ////[Header("接触判定")] 
    //public Enemy_hanten checkhanten;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // 物理演算をしたい場合のFixedUpdate
    void FixedUpdate()
    {
        //if (checkhanten.isOn)
        //{
        //    Left = !Left;
        //}

        //右移動
        if (!Left)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        //左移動
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }

        ////落下後ｙが-10時点で削除
        //if (transform.position.y < -10)
        //{
        //    Destroy(this.gameObject);
        //}
    }

    //todo 触れた時
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //床との処理は無視
        if (collision.gameObject.name == "Floor")
        {
            return;
        }
        //Leftをfalseかtrueに変更
        Left = (Left == true) ? false : true;
        //向きの変更
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

        //壁とぶつかったときに逆に移動
        //if (collision.gameObject.CompareTag("Floor"))
        //{
        //    Left = (Left == true) ? false : true;
        //}

        //if (collision.gameObject.tag == "Enemy")
        //{
        //    // Enemyタグを持つオブジェクトとの当たり判定を無視
        //    Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        //}

        //if(collision.gameObject.tag == "kabe")
        //{
        //    Left = (Left == true) ? false : true;
        //}
    }

}
