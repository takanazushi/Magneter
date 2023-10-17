using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
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
    bool kabeflag = false;
    bool yukaflag = false;

    [SerializeField]
    LayerMask groundLayers = 0;
    [SerializeField]
    float rayLength = 1.0f;

    private Rigidbody2D rb;

    private RaycastHit2D raycastHit2D;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpflag = false;
    }

    // �������Z���������ꍇ��FixedUpdate���g���̂���ʓI
    void FixedUpdate()
    {
        raycastHit2D = CheckGroundStatus();

        PlayerJump();

        if (kabeflag && raycastHit2D.collider || kabeflag == false && raycastHit2D.collider)
        {
            PlayerWalk();
        }
        else if (kabeflag && raycastHit2D.collider == null) 
        {
            Debug.Log("���肨��");
        }

        Debug.Log(raycastHit2D.collider);
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.gameObject.name == "kabe")
        {
            kabeflag = true;
        }

        jumpCount = 0;
        jumpflag = false;

        Debug.Log("�W�����v�t���O�́F" + jumpflag);

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.name == "kabe")
        {
            kabeflag = false;
        }
    }

    private void PlayerWalk()
    {
        
        var hori = Input.GetAxis("Horizontal");

        if (jumpflag)
        {
            speed = jumpMoveX;
        }
        else
        {
            speed = walkMoveX;
        }

        speed = hori * speed;


        rb.velocity = new Vector3(speed, rb.velocity.y, 0);
    }

    private void PlayerJump()
    {
        if (Input.GetKey(KeyCode.Space) && this.jumpCount < 1)
        {
            jumpflag = true;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(transform.up * jumpForce);
            jumpCount++;
        }
    }

    RaycastHit2D CheckGroundStatus()
    {
        Vector2 startPos = transform.position;
        Vector2 direction = Vector2.down; // ��������Ray�𔭎�

        // Ray�𔭎˂��ăq�b�g�����擾
        RaycastHit2D hit = Physics2D.Raycast(startPos, direction, rayLength, groundLayers);

        Debug.DrawRay(startPos, direction * rayLength, Color.red); // Ray���V�[���r���[�ɕ\��

        return hit;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // �O���Y���̐F��ݒ�

        Vector2 startPos = transform.position;
        Vector2 direction = Vector2.down * rayLength; // ��������Ray��\�����邽�߂�rayLength���|���܂�

        Gizmos.DrawRay(startPos, direction);
    }
}
