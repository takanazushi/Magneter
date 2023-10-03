using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Player_Walk : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // 物理演算をしたい場合はFixedUpdateを使うのが一般的
    void FixedUpdate()
    {
        //右入力で左向きに動く
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        //左入力で左向きに動く
        else if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        //ボタンを話すと止まる
        else
        {
            rb.velocity = new Vector2(0,0);
        }
    }
}
