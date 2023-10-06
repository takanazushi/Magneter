using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_WalkFall : MonoBehaviour
{
    [SerializeField]
    private float speed=1;

    [SerializeField]
    private bool Left=false;//������
    
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // �������Z���������ꍇ��FixedUpdate
    void FixedUpdate()
    {
        
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
        
        //�����゙��-10���_�ō폜
        if (transform.position.y<-10) { 
        Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //�ǂƂԂ������Ƃ��ɋt�Ɉړ�
        if (collision.gameObject.CompareTag("Wall"))
        {
            Left = (Left == true) ? false : true;
        }
    }

}
