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

        //10�b��ɃI�u�W�F�N�g��j��
        Destroy(gameObject, 10.0f);
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
            MoveBullet();
        }
    }

    /// <summary>
    /// �e�̈ړ�����
    /// </summary>
    private void MoveBullet()
    {
        //�����Œ�
        bulletDirection.Normalize();

        //���̌����ɃX�s�[�h���|����
        rb.velocity = bulletDirection * moveSpeed;
    }
}
