using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Player_Walk : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //右入力で左向きに動く
        if (Input.GetKey(KeyCode.D))
        {
            speed = 3;
        }
        //左入力で左向きに動く
        else if (Input.GetKey(KeyCode.A))
        {
            speed = -3;
        }
        //ボタンを話すと止まる
        else
        {
            speed = 0;
        }

        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    //オブジェクト同士が接触している時
    private void OnCollisionStay2D(Collision2D other)
    {
        //そのオブジェクトの名前がMoveFloorの場合
        if (other.gameObject.name == "MoveFloor")
        {
            //動く床を親にすることでプレイヤーを追従させる
            transform.parent = other.gameObject.transform;
        }
    }

    //オブジェクト同市が離れた時
    private void OnCollisionExit2D(Collision2D other)
    {
        //親子関係を解除
        transform.parent = null;
    }
}
