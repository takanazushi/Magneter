using UnityEngine;
using System.Collections;
using System.Collections.Generic;

 public class NoLineMoveFloor : MonoBehaviour
{
    //GameObject PointMoveFloor�Ŏg�p
    //GameObject PointMoveFloorRigitBody�̃{�f�B�^�C�v���L�l�}�e�B�b�N�ɕύX���Ă܂�
    //�L�l�}�e�B�b�N�ɕύX���邱�Ƃŏd�͂̉e���𖳂���
    //GameObject PointMoveFloor��PlatformEffector2D�����Ă܂�
    //PlatformEffector2D�œ����蔻����ゾ���Ɍ��肵�Ă܂�

    //GameObject MovePoint1,MovePoint2������ϐ�
    [SerializeField]
    [Header("�ړ��o�H")] private GameObject[] movePoint;
    //����
    [SerializeField]
    [Header("����")] private float speed;

    private Rigidbody2D rb;
    //Point�̐����Ǘ�
    private int nowPoint = 0;
    //�܂�Ԃ����Ǘ�
    private bool returnPoint = false;

    //�O�̒l������ϐ�
    private Vector2 oldpos = Vector2.zero;
    //�i�񂾋���������ϐ�
    private Vector2 newpos = Vector2.zero;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //�ŏ���point������
        if (movePoint != null && movePoint.Length > 0 && rb != null)
        {
            rb.position = movePoint[0].transform.position;
        }
        //�����ʒu
        oldpos = rb.position;
    }

    public Vector2 GetVelocity()
    {
        return newpos;
    }

    private void FixedUpdate()
    {
        if (movePoint != null && movePoint.Length > 1 && rb != null)
        {
            //�ʏ�i�s
            if (!returnPoint)
            {
                int nextPoint = nowPoint + 1;

                //�ڕW�|�C���g�Ƃ̌덷���킸���ɂȂ�܂ňړ�
                if (Vector2.Distance(transform.position, movePoint[nextPoint].transform.position) > 0.1f)
                {
                    //���ݒn���玟�̃|�C���g�ւ̃x�N�g�����쐬
                    Vector2 toVector = Vector2.MoveTowards(transform.position, movePoint[nextPoint].transform.position, speed * Time.deltaTime);

                    //���̃|�C���g�ֈړ�
                    rb.MovePosition(toVector);
                }
                //���̃|�C���g���P�i�߂�
                else
                {
                    //���̃|�C���g�̍��W��ǂ�
                    rb.MovePosition(movePoint[nextPoint].transform.position);
                    ++nowPoint;

                    //���ݒn���z��̍Ōゾ�����ꍇ
                    if (nowPoint + 1 >= movePoint.Length)
                    {
                        returnPoint = true;
                    }
                }
            }
            //�ܕԂ��i�s
            else
            {
                int nextPoint = nowPoint - 1;

                //�ڕW�|�C���g�Ƃ̌덷���킸���ɂȂ�܂ňړ�
                if (Vector2.Distance(transform.position, movePoint[nextPoint].transform.position) > 0.1f)
                {
                    //���ݒn���玟�̃|�C���g�ւ̃x�N�g�����쐬
                    Vector2 toVector = Vector2.MoveTowards(transform.position, movePoint[nextPoint].transform.position, speed * Time.deltaTime);

                    //���̃|�C���g�ֈړ�
                    rb.MovePosition(toVector);
                }
                //���̃|�C���g���P�߂�
                else
                {
                    //�|�C���g��1�߂�
                    rb.MovePosition(movePoint[nextPoint].transform.position);
                    --nowPoint;

                    //���ݒn���z��̍ŏ��������ꍇ
                    if (nowPoint <= 0)
                    {
                        returnPoint = false;
                    }
                }
            }
        }
        //�i�񂾋���
        newpos = (rb.position - oldpos) / Time.deltaTime;
        //�O�̈ʒu
        oldpos = rb.position;
    }
}