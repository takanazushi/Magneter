using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player_Jump : MonoBehaviour
{
    private Rigidbody2D rbody2D;

    [SerializeField]
    private float jumpForce; //ジャンプ力

    private int jumpCount = 0;

    void Start()
    {
        rbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space)&& jumpCount < 1)
        {
            //transform.upで上方向に対して、jumpForceの力を加えます。
            this.rbody2D.AddForce(transform.up * jumpForce);
            jumpCount++;
        }
    }

    //プレイヤーの足元にジャンプ用の空のGameObjectを用意し、Triggerで検知。
    private void OnTriggerStay2D(Collider2D collision)
    {
        //そのオブジェクトの名前がFloorの場合
        if (collision.gameObject.name == "Floor")
        {
            jumpCount = 0;
        }
        //そのオブジェクトの名前がLineMoveFloorの場合
        if (collision.gameObject.name == "LineMoveFloor")
        {
           jumpCount = 0;
        }
        //そのオブジェクトの名前がPointMoveFloorの場合
        if (collision.gameObject.name == "PointMoveFloor")
        {
            jumpCount = 0;
        }
    }
}