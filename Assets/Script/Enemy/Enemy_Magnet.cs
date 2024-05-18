using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Magnet;

/// <summary>
/// �ڐG�����}�O�l�b�g���C�����g�Ɠ����ɂ���
/// ���O�ύX����������������
/// </summary>
[RequireComponent(typeof(Magnet))]
public class Enemy_Magnet : MonoBehaviour
{
    private Magnet magnet;

    private void Start()
    {
        magnet=GetComponent<Magnet>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // MagnetBox_Square ����n�܂���̂�����
        if (collision.gameObject.name.StartsWith("MagnetBox_Square"))
        {
            //�Փ˂����I�u�W�F�N�g��MagnetComponent���擾
            Magnet collisionMagnet = collision.gameObject.GetComponent<Magnet>();

            //Null�łȂ���Ύ��΂̃^�C�v��ݒ�
            if (magnet != null && 
                collisionMagnet != null && 
                collisionMagnet.PuroTypeManet == Type_Magnet.None) 
            {
                // �ɂ̎�ނ�ݒ�
                collisionMagnet.SetType_Magnat(magnet.PuroTypeManet);
            }
        }

        if (collision.gameObject.name.StartsWith("MagnetBox_Rectangle"))
        {
            //�Փ˂����I�u�W�F�N�g��MagnetComponent���擾
            Magnet collisionMagnet = collision.gameObject.GetComponent<Magnet>();

            //Null�łȂ���Ύ��΂̃^�C�v��ݒ�
            if (magnet != null &&
                collisionMagnet != null &&
                collisionMagnet.PuroTypeManet == Type_Magnet.None)
            {
                // �ɂ̎�ނ�ݒ�
                collisionMagnet.SetType_Magnat(magnet.PuroTypeManet);
            }
        }

        if (collision.gameObject.name.StartsWith("MagnetBox_Slender"))
        {
            //�Փ˂����I�u�W�F�N�g��MagnetComponent���擾
            Magnet collisionMagnet = collision.gameObject.GetComponent<Magnet>();

            //Null�łȂ���Ύ��΂̃^�C�v��ݒ�
            if (magnet != null &&
                collisionMagnet != null &&
                collisionMagnet.PuroTypeManet == Type_Magnet.None)
            {
                // �ɂ̎�ނ�ݒ�
                collisionMagnet.SetType_Magnat(magnet.PuroTypeManet);
            }
        }
    }

}
