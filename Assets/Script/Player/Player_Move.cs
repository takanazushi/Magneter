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

    [SerializeField, Header("�Ǔ�������������")]
    public Player_kabehan player_Kabehan;

    private float speed;

    private int jumpCount = 0;

    bool jumpflag = false;
    bool kabeflag = false;

    private Rigidbody2D rb;

    private RaycastHit2D[] raycastHit2D = new RaycastHit2D[2];

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        if (GameManager.instance.checkpointNo > -1)
        {
            // ���[�v��̃`�F�b�N�|�C���g�I�u�W�F�N�g��������("checkpoint (1)" �̂悤�Ȗ��O�ɂȂ��Ă�����́j
            GameObject checkpointObject = GameObject.Find("checkpoint (" + GameManager.instance.checkpointNo + ")");

            // �`�F�b�N�|�C���g�I�u�W�F�N�g�����������ꍇ�́A�v���C���[�����[�v������
            if (checkpointObject != null)
            {
                transform.position = checkpointObject.transform.position;
            }
            else
            {
                Debug.Log(GameManager.instance.checkpointNo + "�`�F�b�N�|�C���g��ʉ߂��Ă��Ȃ�");
            }
        }

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

        for(int i = 0; i < 2; i++)
        {
            if (kabeflag && raycastHit2D[i].collider || kabeflag == false && raycastHit2D[i].collider|| player_Kabehan.isOn == false)
            {
                PlayerWalk();
            }
            else if (kabeflag && raycastHit2D[i].collider == null)
            {
                Debug.Log("���肨��");
            }
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
        float xSpeed = 0;

        if (jumpflag)
        {
            speed = jumpMoveX;
        }
        else
        {
            speed = walkMoveX;
        }

        if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            xSpeed = speed;
        }
        else if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            xSpeed = -speed;
        }
        else
        {
            xSpeed = 0;
        }

        //speed = horizontalInput * speed;


        rb.velocity = new Vector3(xSpeed, rb.velocity.y, 0);
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

    RaycastHit2D[] CheckGroundStatus()
    {
        //Vector2 startPos = transform.position;
        Vector2 pos = transform.position;
        Vector2 startPosLeft = pos - new Vector2(transform.localScale.x / 4, 0); // �v���C���[�e�N�X�`���̍��[
        Vector2 startPosRight = pos + new Vector2(transform.localScale.x / 4, 0); // �v���C���[�e�N�X�`���̉E�[
        Vector2 direction = Vector2.down; // ��������Ray�𔭎�

        // Ray�𔭎˂��ăq�b�g�����擾
        RaycastHit2D hitLeft = Physics2D.Raycast(startPosLeft, direction, rayLength, groundLayers);
        RaycastHit2D hitRight = Physics2D.Raycast(startPosRight, direction, rayLength, groundLayers);

        //Debug.DrawRay(startPos, direction * rayLength, Color.red); // Ray���V�[���r���[�ɕ\��

        return new RaycastHit2D[] { hitLeft, hitRight };
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

        //Vector2 startPos = transform.position;
        Vector2 pos = transform.position;
        Vector2 startPosLeft = pos - new Vector2(transform.localScale.x/4, 0); // �v���C���[�e�N�X�`���̍��[
        Vector2 startPosRight = pos + new Vector2(transform.localScale.x/4, 0); // �v���C���[�e�N�X�`���̉E�[
        //Vector2 direction = Vector2.down; // ��������Ray�𔭎�

        //Vector2 startPos = transform.position;
        Vector2 direction = Vector2.down * rayLength; // ��������Ray��\�����邽�߂�rayLength���|���܂�

        Gizmos.DrawRay(startPosLeft, direction);
        Gizmos.DrawRay (startPosRight, direction);
    }
}
