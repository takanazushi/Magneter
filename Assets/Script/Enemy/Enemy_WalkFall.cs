using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_WalkFall : MonoBehaviour
{
    [SerializeField, Header("敵の速度")]
    private float speed = 1;

    [SerializeField]
    private bool Left=false;//左向き

    [Header("接触判定")] 
    public Enemy_hanten checkhanten;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // 物理演算をしたい場合のFixedUpdate
    void FixedUpdate()
    {
        if (checkhanten.isOn)
        {
            Left = !Left;
        }
        //右移動
        if (!Left)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        //左移動
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector3(1, 1, 1);
        }

        //落下後ｙが-10時点で削除
        if (transform.position.y < -10)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Left = !Left;
        //if(collision.gameObject.tag == "kabe")
        //{
        //    Left = !Left;
        //}
        //if (collision.gameObject.tag == "Enemy")
        //{
        //    // Enemyタグを持つオブジェクトとの当たり判定を無視
        //    Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        //}
        //else if(collision.gameObject.name == "Player")
        //{
        //    Left = !Left;
        //}
    }
}
