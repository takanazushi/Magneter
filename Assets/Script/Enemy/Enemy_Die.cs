using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//敵の子オブジェクトTriggerに触れると敵を削除する

public class Enemy_Die : MonoBehaviour
{
    private Transform parent;

    private void Start()
    {
        //親オブジェクトを取得
        parent = transform.parent;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy"))
        {

            return;

           //Debug.Log("Enemyめり込んだ" + collision.gameObject.name);

            //一定時間後に親オブジェクトを消す
            //DestroyObject(parent, despawnTime);

        }

        //一定時間後に親オブジェクトを消す
        //DestroyObject(parent.gameObject);
        //todo:なるべくこちらで消したい
        parent.gameObject.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //電気床の処理  GameObject Electric Floorは仮で作りました
        //todo：ここダイジョブか心配
        if (collision.gameObject.name == "Electric Floor")
        {
            //触れた敵を消去
            //Destroy(gameObject);
            parent.gameObject.SetActive(false);

            //出現数を1にすることで再び湧くようにする
            Enemy_TopCreate.instance.enemy_outcount = 1;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Enemyタグを持つオブジェクトとの当たり判定を無視
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }    

}
