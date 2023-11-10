using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    public static Wind instance;


    [SerializeField, Header("���ɐi�ރX�s�[�h")]
    public float windMoveLeftSpeed = 0.5f;

    [SerializeField, Header("�E�ɐi�ރX�s�[�h")]
    public float windMoveRightSpeed = 1.5f;

    [SerializeField, Header("���̏o������")]
    public float onTime = 10f;

    [SerializeField, Header("���̃N�[���^�C��")]
    public float outTime = 10f;

    [SerializeField, Header("���̏��")]
    public bool windTimeflg = true;

    //���ɓ������Ă����Ԃ̈ړ����x
    public float movespeed = 0;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        movespeed = 1;

        //�f�o�b�N ���I�u�W�F�N�g�̐F�ύX
        gameObject.GetComponent<Renderer>().material.color = Color.red;

        //���I����Ԃ̃^�C�}�[�X�^�[�g
        StartCoroutine(Loop(onTime));
    }

    private IEnumerator Loop(float second)
    {
        if (windTimeflg)
        {
            yield return new WaitForSeconds(second);
            windTimeflg = false;
            //���I�t��Ԃ̃^�C�}�[�X�^�[�g
            StartCoroutine(Loop(outTime));

            //�f�o�b�N�p
            gameObject.GetComponent<Renderer>().material.color = Color.blue;
            Debug.Log(windTimeflg);
        }
        else
        {
            yield return new WaitForSeconds(second);
            windTimeflg = true;
            StartCoroutine(Loop(onTime));

            //�f�o�b�O
            gameObject.GetComponent<Renderer>().material.color = Color.red;
            Debug.Log(windTimeflg);
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (windTimeflg)
            {
                //�E�ɐi�ނ��A���ɐi��ł��邩�̃L�[���͂̎擾
                float horizontalInput = Input.GetAxis("Horizontal");
                //�E
                if (horizontalInput > 0f)
                {
                    movespeed = windMoveRightSpeed;
                }
                //��
                else if (horizontalInput < 0f)
                {
                    movespeed = windMoveLeftSpeed;
                }
            }
            else
            {
                movespeed = 1;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            movespeed = 1;

            //�f�o�b�O
            Debug.Log("��" + movespeed);
        }
    }
}

