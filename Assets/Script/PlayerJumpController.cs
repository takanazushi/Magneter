using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpController : MonoBehaviour
{
    private Rigidbody2D rb;
    //ジャンプ力
    [SerializeField]
    private float jumpForce = 450f;
    //ジャンプ回数
    private int jumpCount = 0;
    
    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
        if (Input.GetKey(KeyCode.Space)&& jumpCount < 1)
        {//transform.upで上方向に対して、jumpForceの力を加えます。
            rb.AddForce(transform.up * jumpForce);
            jumpCount++;
        }
    }
    //Colliderオブジェクト同士が当たった時、
    private void OnCollisionEnter2D(Collision2D other)
    { //そのオブジェクトの名前がFloorの場合
        if (other.gameObject.name=="Floor")
        {
            jumpCount = 0;
        }
    }
   
}