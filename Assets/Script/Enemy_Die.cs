using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy_Die : MonoBehaviour
{
    [SerializeField, Header("�f�X�|�[������܂ł̎���")]
    private float despawnTime;

    private GameObject parent;

    private void Start()
    {
        //�e�I�u�W�F�N�g���擾
        parent = transform.root.gameObject;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name != parent.name)
        {
            Debug.Log("Enemy�߂荞��" + collision.gameObject.name);

            //��莞�Ԍ�ɐe�I�u�W�F�N�g������
            DestroyObject(parent, despawnTime);
        }
    }

}
