using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    //�ς����Ƃ���@todo�U���Ă܂�

    [SerializeField, Header("���ړ��̑���")]
    private float walkMoveX;

    [SerializeField, Header("�W�����v���̉��ړ��̑���")]
    private float jumpMoveX;

    [SerializeField, Header("�W�����v�̍���")]
    private float jumpForce = 350f;

    [SerializeField, Header("�ǂ̃��C���[�̃I�u�W�F�N�g�Ɠ����蔻������邩")]
    LayerMask groundLayers = 0;

    [SerializeField, Header("���C�̒���"),
        Tooltip("")]
    float rayLength = 1.0f;

    /// <summary>
    /// ����ɐG��Ă���ꍇ�̂ݗL��
    /// </summary>
    private LineMoveFloor moveFloor=null;

    private float speed;
    //todo �ǉ�1 �x���g�R���x�A�ɏ�������̕ϐ�
    private float converspeed;
    //todo �ǉ�2 �x���g�R���x�A�ɗ���Ă�u���b�N�ɏ�������̕ϐ�
    private float blockspeed;

    private float horizontalInput;

    /// <summary>
    /// �W�����v�J�E���g
    /// </summary>
    private int jumpCount = 0;

    /// <summary>
    /// �W�����v�t���O
    /// true:�W�����v��
    /// </summary>
    bool jumpflag = false;

    private Rigidbody2D rb;

    //���C�̏Փˏ��
    private RaycastHit2D[] raycastHit2D = new RaycastHit2D[2];

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        //if (GameManager.instance.checkpointNo > -1)
        //{
        //    // ���[�v��̃`�F�b�N�|�C���g�I�u�W�F�N�g��������("checkpoint (1)" �̂悤�Ȗ��O�ɂȂ��Ă�����́j
        //    GameObject checkpointObject = GameObject.Find("checkpoint (" + GameManager.instance.checkpointNo + ")");

        //    // �`�F�b�N�|�C���g�I�u�W�F�N�g�����������ꍇ�́A�v���C���[�����[�v������
        //    if (checkpointObject != null)
        //    {
        //        transform.position = checkpointObject.transform.position;
        //    }
        //    else
        //    {
        //        Debug.Log(GameManager.instance.checkpointNo + "�`�F�b�N�|�C���g��ʉ߂��Ă��Ȃ�");
        //    }
        //}

        rb = GetComponent<Rigidbody2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("sprite�����_���[�Ȃ�");
        }

        jumpflag = false;
    }

    // �������Z���������ꍇ��FixedUpdate���g���̂���ʓI
    void FixedUpdate()
    {
        //�d�͂�ǉ��Ŋ|����
        //Rigidbody2D->GravityScale���炢���邩�������E�E�E
        //rb.velocity = new(rb.velocity.x, rb.velocity.y - 0.5f);

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

        //���C�̏������ʂ��󂯎��
        raycastHit2D = CheckGroundStatus();

        for (int i = 0; i < 2; i++)
        {
            //���C�ڐG���̂݃W�����v�\
            if (raycastHit2D[i].collider || moveFloor != null)
            {
                //�W�����v������
                jumpCount = 0;
                jumpflag = false;

            }
        }

        for (int i = 0; i < 2; i++)
        {
            //���C�ڐG���̂݃W�����v�\
            if (raycastHit2D[i].collider|| moveFloor != null)
            {
                //�W�����v����
                PlayerJump();
            }
        }

        //�ړ�����
        PlayerWalk();

        //todo �x���g�R���x�A�̑��x�𑫂��Ēʏ푬�x��葬���Ȃ�����
        if(rb.velocity.x > 5.0 || rb.velocity.x < -5.0)
        {
            jumpMoveX += 0.1f;
            //�ő�l
            if(jumpMoveX >= 7.0)
            {
                jumpMoveX = 7.0f;
            }
        }

        PlayerJump();

        if (kabeflag && raycastHit2D.collider || !kabeflag && raycastHit2D.collider)
        {
            PlayerWalk();
        }
        else if (kabeflag && raycastHit2D.collider == null)
        {
            Debug.Log("���肨��");
        }
        Debug.Log(rb.velocity.x);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {


        //if (other.gameObject.tag == "MoveFloor")

        if (other.gameObject.tag == "kabe")
        {
            moveFloor = other.gameObject.GetComponent<LineMoveFloor>();
            //Debug.Log("�������Ɠ������Ă�");
        }

        //Debug.Log("�W�����v�t���O�́F" + jumpflag);

        //todo Jump�l���Z�b�g
        jumpMoveX = 3.0f;
        jumpCount = 0;
        jumpflag = false;

        //Debug.Log("�W�����v�t���O�́F" + jumpflag);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //todo �x���g�R���x�A�ɗ���Ă�u���b�N�ɏ������
        if (collision.gameObject.name == "ConverBlock")
        {
            //�u���b�N�̃X�s�[�h�擾
            blockspeed = CoverVeltThing.Instance.returnSpeed();
        }
        //todo �E�ɗ����x���g�R���x�A�̎�
        if (collision.gameObject.name == "PlusVeltConver")
        {
            converspeed = 3;
        }
        //todo ���ɗ����x���g�R���x�A�̎�
        if (collision.gameObject.name == "MinusVeltConver")
        {
            converspeed = -3;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        //if (collision.gameObject.tag == "MoveFloor")
        
        converspeed = 0;
        
        if (collision.gameObject.tag == "kabe")
        {
            moveFloor = null;
            // Debug.Log("�������Ɠ������ĂȂ�");
        }

        //todo �u���b�N���痣�ꂽ��
        if (collision.gameObject.name == "ConverBlock")
        {
            blockspeed = 0;
        }
    }

    private void PlayerWalk()
    {
        //���ړ����擾
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        horizontalInput = Input.GetAxis("Horizontal");

        //���ړ��X�s�[�h
        float speed;

        //�W�����v���͉��ړ����x��؂�ւ���
        if (jumpflag)
        {
            speed = jumpMoveX;
        }
        else
        {
            speed = walkMoveX;
        }

        //���E���]
        if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        
        //����ɏ���Ă���ꍇ
        Vector2 floorVelocity = Vector2.zero;
        if (moveFloor != null)
        {
            floorVelocity = moveFloor.GetVelocity();
        }

        //���x����
        Vector2 velocity = new (horizontalInput, rb.velocity.y);

        //�X�s�[�h��Z
        velocity.x = velocity.x * speed;

        //����̈ړ����x��ǉ�
        velocity.x += floorVelocity.x;
        velocity.y = rb.velocity.y;
        ////velocity.y += floorVelocity.y;

        //if (floorVelocity.y >= 0)
        //{
        //    //velocity.y -= floorVelocity.y;
        //}
        //else
        //{
        //    //velocity.y += floorVelocity.y;
        //}

        rb.velocity = velocity;

        Debug.Log(floorVelocity);

        //todo �ǉ�7 �R���x�A�ƃu���b�N�̃X�s�[�h���Z
        rb.velocity = new Vector3(speed + converspeed + blockspeed, rb.velocity.y, 0);
    }

    private void PlayerJump()
    {
        if (Input.GetKey(KeyCode.Space) && this.jumpCount < 1)
        {
            float pwa = jumpForce;

            jumpflag = true;
            rb.velocity = new Vector2(rb.velocity.x, 0);

            //�u�ԓI�ȗ͂�������
            rb.AddForce(transform.up * pwa, ForceMode2D.Impulse);
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

        Debug.DrawRay(startPos, direction * rayLength, Color.red); // Ray���V�[���r���[�ɕ\��

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
            // �v���C���[���g��Transform�ȊO�𑀍�
            if (childTransform != transform) 
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
