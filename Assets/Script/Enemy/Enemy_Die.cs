using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy_Die : MonoBehaviour
{
    [SerializeField, Header("デスポーンするまでの時間")]
    private int despawnTime;

    private GameObject parent;

    private void Start()
    {
        //親オブジェクトを取得
        parent = transform.root.gameObject;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Enemyタグを持つオブジェクトとの当たり判定を無視
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }

        if (collision.gameObject.name != parent.name && collision.gameObject.name != "Player" &&
            !collision.gameObject.CompareTag("Enemy"))  
        {
            Debug.Log("Enemyめり込んだ" + collision.gameObject.name);

            //一定時間後に親オブジェクトを消す
            //DestroyObject(parent, despawnTime);
        }
    }    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Enemyタグを持つオブジェクトとの当たり判定を無視
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }

        //if (collision.gameObject.name != parent.name && collision.gameObject.name != "Player" &&
        //    collision.gameObject.tag != "BlueBllet" && collision.gameObject.tag != "RedBullet")  
        //{
        //    Debug.Log("Enemyめり込んだ" + collision.gameObject.name);

        //    //一定時間後に親オブジェクトを消す
        //    //DestroyObject(parent, despawnTime);
        //}
        //電気床の処理  GameObject Electric Floorは仮で作りました
        if (collision.gameObject.name == "Electric Floor")
        {
            //触れた敵を消去
            Destroy(gameObject);
            //出現数を1にすることで再び湧くようにする
            Enemy_TopCreate.instance.enemy_outcount = 1;
        }
    }
}
