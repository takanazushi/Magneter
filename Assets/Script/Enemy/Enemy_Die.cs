using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//�G�̎q�I�u�W�F�N�gTrigger�ɐG���ƓG���폜����

public class Enemy_Die : MonoBehaviour
{
    private Transform parent;

    private void Start()
    {
        //�e�I�u�W�F�N�g���擾
        parent = transform.parent;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy"))
        {

            return;

           //Debug.Log("Enemy�߂荞��" + collision.gameObject.name);

            //��莞�Ԍ�ɐe�I�u�W�F�N�g������
            //DestroyObject(parent, despawnTime);

        }

        //��莞�Ԍ�ɐe�I�u�W�F�N�g������
        //DestroyObject(parent.gameObject);
        //todo:�Ȃ�ׂ�������ŏ�������
        parent.gameObject.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //�d�C���̏���  GameObject Electric Floor�͉��ō��܂���
        //todo�F�����_�C�W���u���S�z
        if (collision.gameObject.name == "Electric Floor")
        {
            //�G�ꂽ�G������
            //Destroy(gameObject);
            parent.gameObject.SetActive(false);

            //�o������1�ɂ��邱�ƂōĂїN���悤�ɂ���
            Enemy_TopCreate.instance.enemy_outcount = 1;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Enemy�^�O�����I�u�W�F�N�g�Ƃ̓����蔻��𖳎�
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }    

}
