using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_FollowerBullet : MonoBehaviour
{
    //�v���C���[�̍��W�i�[
    private Transform target;

    //�ړ����x
    [SerializeField, Header("�e�̔��˃X�s�[�h")]
    private float moveSpeed;

    //��ʓ�
    private bool InField = false;

    private Rigidbody2D rb;

    private Vector3 bulletDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        target = GameObject.FindGameObjectWithTag("Player").transform;

        // �e�̕��������@����G�֌�����
        bulletDirection = target.position - transform.position;

        //todo �e�̔��ˊp�x�̐���
        if(bulletDirection.y > -5)
        {
            bulletDirection.y = -5;
        }
    }

    //��ʓ��œ�����
    private void OnBecameVisible()
    {
        //��ʓ��ɂ���Ƃ�true
        InField = true;
    }

    //��ʊO�ŏ�������
    private void OnBecameInvisible()
    {
        //����
        Destroy(rb.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //��ʓ��Ŏ��s
        if (InField)
        {
            //�����Œ�
            bulletDirection.Normalize();

            //���̌����ɃX�s�[�h���|����
            rb.velocity = bulletDirection * moveSpeed;

            //10�b��ɖC�e��j�󂷂�
            Destroy(gameObject, 5.0f);
        }   
    }
}
