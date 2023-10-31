using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firing_WithAim : MonoBehaviour
{
    private Rigidbody2D rb;

    //��ʓ�
    private bool InField = false;

    //Prefabs�ŕ������镨������
    [SerializeField, Header("�ePrefab�����Ă�������")]
    public GameObject BulletObj;

    //�v���C���[�̍��W�i�[
    [SerializeField, Header("�v���C���[�����Ă�������")]
    public Transform target;

    //�ړ����x
    [SerializeField, Header("�e�̔��˃X�s�[�h")]
    public float moveSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //2.5�b��Ɏ��s���鏈��
        InvokeRepeating("Shot", 2.5f,2.5f);
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
        //��ʓ��ɂ���Ƃ�true
        InField = true;
    }
    void Shot()
    {
        //��ʓ��Ŏ��s
        if (InField)
        {
            // �e�̕��������@����G�֌�����
            Vector3 bulletDirection = target.position - transform.position;
            //�����Œ�
            bulletDirection.Normalize();

            //��������
            GameObject obj = Instantiate(BulletObj);

            //���̌����ɃX�s�[�h���|����
            rb.velocity = bulletDirection * moveSpeed;
            //10�b��ɖC�e��j�󂷂�
            Destroy(obj, 10.0f);
        }
    }
}
