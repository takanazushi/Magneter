using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_FollowerBullet : MonoBehaviour
{
    //�v���C���[�̍��W�i�[
    private Transform target;

    [SerializeField, Header("�e�̍ő��]�p�x")]
    private float maxRotation;

    [SerializeField, Header("�e�̍Œ��]�p�x")]
    private float minRotation;

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

        Vector3 dir = (target.position - transform.position);

        //�p�x�ɕϊ�
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

        //����
        angle = Mathf.Clamp(angle, minRotation, maxRotation);

        // ��]��K�p
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

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
        audioSource.PlayOneShot(ShotSE);

        InField = true;
        Debug.Log("�J�����͈͓�");
    }

    //��ʊO�ŏ�������
    private void OnBecameInvisible()
    {
        //����
        Debug.Log("�J�����͈͊O");
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.Is_Ster_camera_end)
        {
            Destroy(rb.gameObject);
            return;
        }
        //��ʓ��Ŏ��s
        else if (InField)
        {
            //�����Œ�
            bulletDirection.Normalize();

            //���̌����ɃX�s�[�h���|����
            rb.velocity = bulletDirection * moveSpeed;

            //10�b��ɖC�e��j�󂷂�
            Destroy(gameObject, 5.0f);
        }   
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Destroy (gameObject);
    }
}
