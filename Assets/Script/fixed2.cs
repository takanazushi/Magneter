using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Purchasing;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class fixed2 : MonoBehaviour
{
    private Rigidbody2D rb;

    //��ʓ�
    private bool InField = false;

    //X�X�s�[�h
    private float SpeedX;

    //Y�X�s�[�h
    private float SpeedY;

    //����
    public bool Direction = false;

    //���΂�
    public bool LeftDiazional = false;

    //�E�΂�
    public bool RightDiazional = false;

    //Prefabs�ŕ������镨������(����̏ꍇCircle)
    public GameObject BulletObj;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("Shot", 1.0f, 5.0f);
    }

    //��ʊO�ŏ�������
    private void OnBecameInvisible()
    {
        //����
        Destroy(rb.gameObject);
    }

    //��ʓ��œ�����
    private void OnBecameVisible()
    {
        InField = true;
    }

    ////�����蔻��
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    
    //}

    void Shot()
    {
        if(InField)
        {
            //��������
            GameObject obj = Instantiate(BulletObj, transform.position, Quaternion.identity);
            //���O��Circle�ɂ���
            obj.name = BulletObj.name;
            
            //������
            if (Direction)
            {
                SpeedX = -5;
            }
            //�E����
            else
            {
                SpeedX = 5;
            }

            // �e���͎��R�ɐݒ�
            rb.velocity = new Vector2(rb.velocity.x + SpeedX, rb.velocity.y + SpeedY);
            //8�b��ɖC�e��j�󂷂�
            Destroy(obj, 8.0f);
        }   
    }
}
