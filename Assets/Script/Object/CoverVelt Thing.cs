using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverVeltThing : MonoBehaviour
{
    private Rigidbody2D rb;

    //インスタンスの定義
    public static CoverVeltThing Instance;

    //スピード
    private float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //インスタンス生成時に最初に呼び出される
    private void Awake()
    {
        if (Instance == null)
        {
            //自身をインスタンスにする
            Instance = this;
        }
    }

    void Update()
    { 
       //移動
       rb.velocity = new Vector2(speed, rb.velocity.y);
    }
    
    //乗っている間
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "PlusVeltConver")
        {
            speed = 3;
        }
        else if (collision.gameObject.name == "MinusVeltConver")
        {
            speed = -3;
        }
    }

    //離れたら
    private void OnCollisionExit2D(Collision2D collision)
    {
        speed = 0;
    }

    //流れてくる物のスピードを取得する関数
    public float returnSpeed()
    {
        return speed;
    }
}
