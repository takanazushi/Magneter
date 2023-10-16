using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Player_Walk : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //�E���͂ō������ɓ���
        if (Input.GetKey(KeyCode.D))
        {
            speed = 3;
        }
        //�����͂ō������ɓ���
        else if (Input.GetKey(KeyCode.A))
        {
            speed = -3;
        }
        //�{�^����b���Ǝ~�܂�
        else
        {
            speed = 0;
        }

        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    //�I�u�W�F�N�g���m���ڐG���Ă��鎞
    private void OnCollisionStay2D(Collision2D other)
    {
        //���̃I�u�W�F�N�g�̖��O��MoveFloor�̏ꍇ
        if (other.gameObject.name == "MoveFloor")
        {
            //��������e�ɂ��邱�ƂŃv���C���[��Ǐ]������
            transform.parent = other.gameObject.transform;
        }
    }

    //�I�u�W�F�N�g���s�����ꂽ��
    private void OnCollisionExit2D(Collision2D other)
    {
        //�e�q�֌W������
        transform.parent = null;
    }
}
