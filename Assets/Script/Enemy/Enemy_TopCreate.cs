using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_TopCreate : MonoBehaviour
{
    //todo : �S��

    private Rigidbody2D rb;

    //�J����
    private Camera mainCamera;

    //�J�������W�i�[
    Vector3 viewPos;

    //�G�̐�
    [SerializeField]
    private int enemy_coumt;

    //�G�̏o�����Ă鐔
    public int enemy_outcount;

    //�X�|�[���ʒu
    private Vector3 spawnpositison;

    //�o��������G�����Ă���
    [SerializeField]
    private GameObject enemy;

    public static Enemy_TopCreate instance;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        //�ŏ��̏o���ʒu�i�[
        spawnpositison = transform.position;
        rb = GetComponent<Rigidbody2D>();
        
        //�G�̏o����
        enemy_outcount = 1;
        
        //�J��Ԃ� 5�b��1��
        InvokeRepeating("EnemyCreate", 0f, 5f);
    }

    void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        //�J�������W�擾
        viewPos = mainCamera.WorldToViewportPoint(transform.position);
    }

    //�G��������
    private void EnemyCreate()
    {
        //�J�������̎�����
        if (viewPos.x > 0 && viewPos.x < 1 && enemy_coumt > 0 && enemy_outcount > 0)
        {
            //��������
            Instantiate(enemy, spawnpositison, Quaternion.identity);
            //�G���̏o����
            enemy_coumt--;
            //�o������0�ŏo�������Ȃ����遨�d�C���ŏ��Ō�J�E���g��1�ɂ���
            enemy_outcount = 0;
        }
    }
}
