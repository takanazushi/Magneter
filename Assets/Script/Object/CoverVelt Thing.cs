using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverVeltThing : MonoBehaviour
{
    private Rigidbody2D rb;

    //�C���X�^���X�̒�`
    public static CoverVeltThing Instance;

    //�X�s�[�h
    private float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //�C���X�^���X�������ɍŏ��ɌĂяo�����
    private void Awake()
    {
        if (Instance == null)
        {
            //���g���C���X�^���X�ɂ���
            Instance = this;
        }
    }

    void Update()
    { 
       //�ړ�
       rb.velocity = new Vector2(speed, rb.velocity.y);
    }
    
    //����Ă����
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "PlusVeltConver")
        {
            speed = 3;
        }
        else if (collision.gameObject.name == "MinusVeltConver")
        {
            speed = -3;
        }
    }

    //���ꂽ��
    private void OnCollisionExit2D(Collision2D collision)
    {
        speed = 0;
    }

    //����Ă��镨�̃X�s�[�h���擾����֐�
    public float returnSpeed()
    {
        return speed;
    }
}
