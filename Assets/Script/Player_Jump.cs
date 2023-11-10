using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player_Jump : MonoBehaviour
{
    private Rigidbody2D rbody2D;

    [SerializeField]
    private float jumpForce; //�W�����v��

    private int jumpCount = 0;

    void Start()
    {
        rbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space)&& jumpCount < 1)
        {
            //transform.up�ŏ�����ɑ΂��āAjumpForce�̗͂������܂��B
            this.rbody2D.AddForce(transform.up * jumpForce);
            jumpCount++;
        }
    }

    //�v���C���[�̑����ɃW�����v�p�̋��GameObject��p�ӂ��ATrigger�Ō��m�B
    private void OnTriggerStay2D(Collider2D collision)
    {
        //���̃I�u�W�F�N�g�̖��O��Floor�̏ꍇ
        if (collision.gameObject.name == "Floor")
        {
            jumpCount = 0;
        }
        //���̃I�u�W�F�N�g�̖��O��LineMoveFloor�̏ꍇ
        if (collision.gameObject.name == "LineMoveFloor")
        {
           jumpCount = 0;
        }
        //���̃I�u�W�F�N�g�̖��O��PointMoveFloor�̏ꍇ
        if (collision.gameObject.name == "PointMoveFloor")
        {
            jumpCount = 0;
        }
    }
}