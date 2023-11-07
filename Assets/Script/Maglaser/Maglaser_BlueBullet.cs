using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Magnet;

public class Maglaser_BlueBullet : Maglaser_Bullet
{
    // Update is called once per frame
    private void Awake()
    {
        base.BulletStart();
    }


    void Update()
    {
        base.BulletUpdate();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //�Փ˂����I�u�W�F�N�g��MagnetComponent���擾
        Magnet magnet = collision.gameObject.GetComponent<Magnet>();
        
        //Null�łȂ���Ύ��΂̃^�C�v��ݒ�
        if (magnet != null)
        {
            magnet.SetType_Magnat(Type_Magnet.N);
        }

        //�e�̍폜
        Destroy(gameObject);
        
        //�f�o�b�O�\��
        Debug.Log("�e��������܂����I");
    }

}
