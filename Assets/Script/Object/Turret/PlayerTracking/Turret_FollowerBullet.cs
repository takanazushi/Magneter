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

    [SerializeField]
    private AudioClip ShotSE;

    //��ʓ�
    private bool InField = false;

    private Rigidbody2D rb;

    private Vector3 bulletDirection;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("Battery_FollowerBullet��AudioSource���ĂȂ�");
        }

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
        if (!GameManager.instance.Is_Ster_camera_end) { return; }
        //��ʓ��ɂ���Ƃ�true
        audioSource.PlayOneShot(ShotSE);
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
        if (InField&&GameManager.instance.Is_Ster_camera_end)
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
