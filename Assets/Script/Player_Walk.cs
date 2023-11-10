using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class Player_Walk : MonoBehaviour
{
    //�X�s�[�h
    private float speed = 0;
    private Rigidbody2D rb;

    //Movefloor�N���X���瓮�����̈ړ����x������ϐ�
    private Vector2 linemovingfloor;
    //LineMoveFloor�N���X���擾
    private LineMoveFloor linemovefloor;

    //NoLineMoveFloor�N���X���瓮�����̈ړ����x������ϐ�
    private Vector2 nolinemovingfloor;
    //NoLineMoveFloor�N���X���擾
    private NoLineMoveFloor nolinemovefloor;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        linemovefloor = null;
        nolinemovefloor = null;
    }

    private void Update()
    {
        //�E���͂ŉE�����ɓ���
        if (Input.GetKey(KeyCode.D))
        {
            speed = 5.0f;
        }
        //�����͂ō������ɓ���
        else if (Input.GetKey(KeyCode.A))
        {
            speed = -5.0f;
        }
        //�{�^����b���Ǝ~�܂�
        else
        {
            speed = 0.0f;
        }

        //movefloor�ɓ������̏�񂪓�������
        if (linemovefloor != null)
        {
            LineMoveFloor();
        }
        //nolinemovefloor�ɓ������̏�񂪓�������
        else if (nolinemovefloor != null)
        {
            PointMoveFloor();
        }
        //�������ɏ���ĂȂ��Ƃ�
        else 
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
    }
    
    //�֐�LineMoveFloor��PointMoveFloor�͂ǂ������̗p���邩������Ȃ��̂Ŋ֐��ŕ����Ă܂��B
    //�����I�ɂ͈ꏏ�Ȃ̂ŕs�̗p�̕��͏����đ��v�ł��B
    //�ϐ�movefloor��nolinemovefloor���g���Ă��邩�̈Ⴂ�����B
    private void LineMoveFloor()
    {
        //�l���Z�b�g
        linemovingfloor = Vector2.zero;
        //MoveFloor�N���X����GetVelocity()���Ăяo���������̈ړ����x���擾
        linemovingfloor = linemovefloor.GetVelocity();

        //�������̐i�s�����Ƌt��Player���i�񂾎���Player�̈ړ����x���Ȃ���̑��x�����B
        //�X�s�[�h�̒l�͑̊��ŗǂ������Ȃ̂����Ă܂��B
        //�ς����������玩�R�ɕς��Ă��������B

        //�����������ɐi��ł�Ƃ�Player���E�ɐi�񂾎�
        if (Input.GetKey(KeyCode.D) && linemovingfloor.x < 0)
        { 
            speed = 6.5f;
        }
        //���������E�ɐi��ł�Ƃ�Player�����ɐi�񂾎�
        else if (Input.GetKey(KeyCode.A) && linemovingfloor.x > 0)
        {
            speed = -6.5f;
        }

        rb.velocity = new Vector2(speed + linemovingfloor.x, rb.velocity.y);
    }

    private void PointMoveFloor()
    {
        //�l���Z�b�g
        nolinemovingfloor = Vector2.zero;
       //nolinemovefloor�N���X����GetVelocity()���Ăяo���������̈ړ����x�擾
       nolinemovingfloor = nolinemovefloor.GetVelocity();

       //�����������ɐi��ł�Ƃ�Player���E�ɐi�񂾎�
       if (Input.GetKey(KeyCode.D) && nolinemovingfloor.x < 0)
       {
           speed = 6.5f;
       }
       //���������E�ɐi��ł�Ƃ�Player�����ɐi�񂾎�
       else if (Input.GetKey(KeyCode.A) && nolinemovingfloor.x > 0)
       {
           speed = -6.5f;
       }
        //�ړ�
        rb.velocity = new Vector2(speed + nolinemovingfloor.x, rb.velocity.y);
    }

    //�I�u�W�F�N�g���m���ڐG���Ă��鎞
    private void OnTriggerStay2D(Collider2D other)
    {
        //���̃I�u�W�F�N�g�̖��O��MoveFloor�̏ꍇ
        if (other.gameObject.name == "LineMoveFloor")
        {
            linemovefloor = other.gameObject.GetComponent<LineMoveFloor>();
        }
        else if (other.gameObject.name == "PointMoveFloor")
        {
            nolinemovefloor = other.gameObject.GetComponent<NoLineMoveFloor>();
        }
    }

    //�I�u�W�F�N�g���m�����ꂽ��
    private void OnTriggerExit2D(Collider2D other)
    {
        //�I�u�W�F�N�g�̖��O��MoveFloor�̏ꍇ
        if (other.gameObject.name == "LineMoveFloor")
        {
            //����
            linemovefloor = null;
        }
        else if (other.gameObject.name == "PointMoveFloor")
        {
            //����
            nolinemovefloor = null;
        }
    }
}
