using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Magnet;

public class Enemy_Magnet : MonoBehaviour
{
    public Magnet magnet;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // MagnetBox_Square ����n�܂���̂�����
        if (collision.gameObject.name.StartsWith("MagnetBox_Square"))
        {
            //�Փ˂����I�u�W�F�N�g��MagnetComponent���擾
            Magnet collisionMagnet = collision.gameObject.GetComponent<Magnet>();
            // �ɂ̎�ނ��擾
            Type_Magnet currentType = collisionMagnet.PuroTypeManet;

            //Null�łȂ���Ύ��΂̃^�C�v��ݒ�
            if (magnet != null && collisionMagnet != null && collisionMagnet.PuroTypeManet == Type_Magnet.None) 
            {
                // �ɂ̎�ނ�ݒ�
                collisionMagnet.SetType_Magnat(magnet.PuroTypeManet);
            }

            //�f�o�b�O�\��
            Debug.Log("�G�����Ɏ��͕t�^�I");
        }
    }

}
