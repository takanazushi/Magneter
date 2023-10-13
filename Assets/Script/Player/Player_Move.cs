using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    [SerializeField, Header("���ړ��̑���")]
    private float walkMoveX;

    [SerializeField, Header("�W�����v���̉��ړ��̑���")]
    private float jumpMoveX;

    [SerializeField, Header("�W�����v�̍���")]
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

    // �������Z���������ꍇ��FixedUpdate���g���̂���ʓI
    void FixedUpdate()
    {

        PlayerJump();
        PlayerWalk();

        Debug.Log("���݂̃W�����v�F" + speed);
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            jumpCount = 0;
            jumpflag = false;

            Debug.Log("�W�����v�t���O�́F" + jumpflag);
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
