using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpController : MonoBehaviour
{
    private Rigidbody2D rbody2D;

    [SerializeField]
    private float jumpForce = 450f; //ジャンプ力

    private int jumpCount = 0;
    

    void Start()
    {
        rbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
        if (Input.GetKey(KeyCode.Space)&& this.jumpCount < 1)
        {
            //transform.upで上方向に対して、jumpForceの力を加えます。
            this.rbody2D.AddForce(transform.up * jumpForce);
            jumpCount++;
        }
    }
    //Colliderオブジェクト同士が当たった時、
    private void OnCollisionEnter2D(Collision2D other)
    {
        //そのオブジェクトの名前がFloorの場合
        if (other.gameObject.name=="Floor")
        {
            jumpCount = 0;
        }
    }
}