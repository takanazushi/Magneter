using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_WalkFall : MonoBehaviour
{
    [SerializeField]
    private float speed=1;

    [SerializeField]
    private bool Left=false;//左向き
    
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
        if (transform.position.y<-10) { 
        Destroy(this.gameObject);
        }
    }
}
