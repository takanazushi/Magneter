using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    [SerializeField, Header("横移動の速さ")]
    private float walkMoveX;

    [SerializeField, Header("ジャンプ中の横移動の速さ")]
    private float jumpMoveX;

    [SerializeField, Header("ジャンプの高さ")]
    private float jumpForce = 350f;

    private float speed;

    private int jumpCount = 0;

    bool jumpflag = false;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpflag = false;
    }

    // 物理演算をしたい場合はFixedUpdateを使うのが一般的
    void FixedUpdate()
    {

        PlayerJump();
        PlayerWalk();

        Debug.Log("現在のジャンプ：" + speed);
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            jumpCount = 0;
            jumpflag = false;

            Debug.Log("ジャンプフラグは：" + jumpflag);
        }
    }

    private void PlayerWalk()
    {
        speed = walkMoveX;
        var hori = Input.GetAxis("Horizontal");

        if (jumpflag)
        {
            speed = jumpMoveX;
        }

        speed = hori * speed;


        rb.velocity = new Vector3(speed, rb.velocity.y, 0);
    }

    private void PlayerJump()
    {
        if (Input.GetKey(KeyCode.Space) && this.jumpCount < 1)
        {
            jumpflag = true;
            rb.AddForce(transform.up * jumpForce);
            jumpCount++;
        }
    }
}
