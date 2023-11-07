using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_WalkFall : MonoBehaviour
{
    [SerializeField, Header("敵の速度")]
    private float speed = 1;

    [SerializeField, Header("動く方向（チェックで左に動く）")]
    private bool Left = true;
    
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // 物理演算をしたい場合のFixedUpdate
    void FixedUpdate()
    {
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

        //落下後ｙが-10時点で削除
        if (transform.position.y < -10)  
        { 
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //壁とぶつかったときに逆に移動
        if (collision.gameObject.CompareTag("Wall"))
        {
            Left = (Left == true) ? false : true;
        }
    }

}
