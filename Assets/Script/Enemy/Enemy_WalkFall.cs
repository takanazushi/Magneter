using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Magnet;

public class Enemy_WalkFall : MonoBehaviour
{
    [SerializeField, Header("�G�̑��x")]
    private float speed = 1;

    [SerializeField]
    private bool Left = false;//������

    [Header("�ڐG����")]
    public Magnet magnet;

    //���͂̉e���͈͓���
    public bool inversionWalk = true;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // �������Z���������ꍇ��FixedUpdate
    void FixedUpdate()
    {
        if (inversionWalk && magnet.inversion)
        {
            inversionWalk = false;
            Left = !Left;
        }
        else if (!magnet.inversion)
        {
            inversionWalk = true;
        }

        if (!magnet.inversion || !magnet.inversion && !magnet.notType)     
        {
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
        if (magnet.inversion)
        {
            inversionWalk = true;
        }
    }
}
