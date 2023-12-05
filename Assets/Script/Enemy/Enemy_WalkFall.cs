using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_WalkFall : MonoBehaviour
{
    [SerializeField, Header("�G�̑��x")]
    private float speed = 1;

    [SerializeField]
    private bool Left=false;//������

    [Header("�ڐG����")] 
    public Enemy_hanten checkhanten;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // �������Z���������ꍇ��FixedUpdate
    void FixedUpdate()
    {
        if (checkhanten.isOn)
        {
            Left = !Left;
        }
        //�E�ړ�
        if (!Left)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        //���ړ�
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector3(1, 1, 1);
        }

        //�����゙��-10���_�ō폜
        if (transform.position.y < -10)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Left = !Left;
        //if(collision.gameObject.tag == "kabe")
        //{
        //    Left = !Left;
        //}
        //if (collision.gameObject.tag == "Enemy")
        //{
        //    // Enemy�^�O�����I�u�W�F�N�g�Ƃ̓����蔻��𖳎�
        //    Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        //}
        //else if(collision.gameObject.name == "Player")
        //{
        //    Left = !Left;
        //}
    }
}
