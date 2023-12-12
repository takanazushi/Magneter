using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_WalkFall : MonoBehaviour
{
    //�R�����g���Ă��͌��X�����Ă���ł��B
    //todo�U���Ă�Ƃ���͕ς����Ƃ���ł��B

    [SerializeField, Header("�G�̑��x")]
    private float speed;

    [SerializeField]
    private bool Left;//������

    ////[Header("�ڐG����")] 
    //public Enemy_hanten checkhanten;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // �������Z���������ꍇ��FixedUpdate
    void FixedUpdate()
    {
        //if (checkhanten.isOn)
        //{
        //    Left = !Left;
        //}

        //�E�ړ�
        if (!Left)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        //���ړ�
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }

        ////�����゙��-10���_�ō폜
        //if (transform.position.y < -10)
        //{
        //    Destroy(this.gameObject);
        //}
    }

    //todo �G�ꂽ��
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //���Ƃ̏����͖���
        if (collision.gameObject.name == "Floor")
        {
            return;
        }
        //Left��false��true�ɕύX
        Left = (Left == true) ? false : true;
        //�����̕ύX
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

        //�ǂƂԂ������Ƃ��ɋt�Ɉړ�
        //if (collision.gameObject.CompareTag("Floor"))
        //{
        //    Left = (Left == true) ? false : true;
        //}

        //if (collision.gameObject.tag == "Enemy")
        //{
        //    // Enemy�^�O�����I�u�W�F�N�g�Ƃ̓����蔻��𖳎�
        //    Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        //}

        //if(collision.gameObject.tag == "kabe")
        //{
        //    Left = (Left == true) ? false : true;
        //}
    }

}
