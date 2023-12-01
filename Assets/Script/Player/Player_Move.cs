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

    [SerializeField, Header("�ǂ̃��C���[�̃I�u�W�F�N�g�Ɠ����蔻������邩")]
    LayerMask groundLayers = 0;

    [SerializeField, Header("���C�̒���")]
    float rayLength = 1.0f;

    [SerializeField, Header("���̌����l")]
    private float windMoveSpeed = 1.0f;

    private float speed;

    private int jumpCount = 0;

    bool jumpflag = false;
    bool kabeflag = false;

    private Rigidbody2D rb;

    private RaycastHit2D raycastHit2D;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        jumpflag = false;
    }

    // �������Z���������ꍇ��FixedUpdate���g���̂���ʓI
    void FixedUpdate()
    {
        //�}�E�X�̈ʒu���擾
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //�v���C���[�̈ʒu�����E�Ƀ}�E�X������ꍇ
        //�E�ړ�key���������ꍇ
        //�E����
        if (mousePosition.x > transform.position.x)
        {
            spriteRenderer.flipX = false;
            SetChildObjectRotation(false);
        }
        else if (mousePosition.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
            SetChildObjectRotation(true);
        }

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

        //Debug.Log(raycastHit2D.collider);

    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "kabe")
        {
            kabeflag = true;
        }

        jumpCount = 0;
        jumpflag = false;

        Debug.Log("�W�����v�t���O�́F" + jumpflag);

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "kabe")
        {
            kabeflag = false;
        }
    }

    private void PlayerWalk()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (jumpflag)
        {
            speed = jumpMoveX;
        }
        else
        {
            speed = walkMoveX;
        }

        speed = horizontalInput * speed;

        //���ɓ������Ă����Ԃ̑��x�擾
        windMoveSpeed = Wind.instance.getMoveSpeed;
        

        rb.velocity = new Vector3(speed / windMoveSpeed, rb.velocity.y, 0);

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

        //Debug.DrawRay(startPos, direction * rayLength, Color.red); // Ray���V�[���r���[�ɕ\��

        return hit;
    }

    void SetChildObjectRotation(bool isLeft)
    {
        // �v���C���[�̎q�I�u�W�F�N�g���擾
        SpriteRenderer[] childSpriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        // �e�q�I�u�W�F�N�g�̌�����ݒ�
        foreach (SpriteRenderer childRenderer in childSpriteRenderers)
        {
            childRenderer.flipX = isLeft;
        }

        // �v���C���[�̎q�I�u�W�F�N�g���擾
        Transform[] childTransforms = GetComponentsInChildren<Transform>();

        // �e�q�I�u�W�F�N�g�̈ʒu��ݒ�
        foreach (Transform childTransform in childTransforms)
        {
            if (childTransform != transform) // �v���C���[���g��Transform�ȊO�𑀍�
            {
                Vector3 newPosition = childTransform.localPosition;
                newPosition.x = isLeft ? -Mathf.Abs(newPosition.x) : Mathf.Abs(newPosition.x);
                childTransform.localPosition = newPosition;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // �O���Y���̐F��ݒ�

        Vector2 startPos = transform.position;
        Vector2 direction = Vector2.down * rayLength; // ��������Ray��\�����邽�߂�rayLength���|���܂�

        Gizmos.DrawRay(startPos, direction);
    }
}
