using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy_Die : MonoBehaviour
{
    [SerializeField, Header("�f�X�|�[������܂ł̎���")]
<<<<<<< HEAD
    private float despawnTime;
=======
    private int despawnTime;
>>>>>>> origin/FromTopEnemy

    private GameObject parent;

    private void Start()
    {
        //�e�I�u�W�F�N�g���擾
<<<<<<< HEAD
        parent = transform.root.gameObject;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name != parent.name && collision.gameObject.name != "Player" &&
            collision.gameObject.tag != "BlueBllet" && collision.gameObject.tag != "RedBullet")  
        {
            Debug.Log("Enemy�߂荞��" + collision.gameObject.name);

            //��莞�Ԍ�ɐe�I�u�W�F�N�g������
            //DestroyObject(parent, despawnTime);
        }
    }

=======
        //parent = transform.root.gameObject;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.name != parent.name && collision.gameObject.name != "Player" &&
        //    collision.gameObject.tag != "BlueBllet" && collision.gameObject.tag != "RedBullet")  
        //{
        //    Debug.Log("Enemy�߂荞��" + collision.gameObject.name);

        //    //��莞�Ԍ�ɐe�I�u�W�F�N�g������
        //    //DestroyObject(parent, despawnTime);
        //}
        //�d�C���̏���  GameObject Electric Floor�͉��ō��܂���
        if(collision.gameObject.name == "Electric Floor")
        {
            //�G�ꂽ�G������
            Destroy(gameObject);
            //�o������1�ɂ��邱�ƂōĂїN���悤�ɂ���
            Enemy_TopCreate.instance.enemy_outcount = 1;
        }
    }
>>>>>>> origin/FromTopEnemy
}
