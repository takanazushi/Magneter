using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy_Die : MonoBehaviour
{
    [SerializeField, Header("デスポーンするまでの時間")]
    private float despawnTime;

    private GameObject parent;

    private void Start()
    {
        //親オブジェクトを取得
        parent = transform.root.gameObject;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name != parent.name)
        {
            Debug.Log("Enemyめり込んだ" + collision.gameObject.name);

            //一定時間後に親オブジェクトを消す
            DestroyObject(parent, despawnTime);
        }
    }

}
