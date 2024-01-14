using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hole : MonoBehaviour
{
    [SerializeField, Header("リスポーンするまでの時間")]
    private float respawnTime;

    //プレイヤーとぶつかった場合ゲームシーン再読み込みでリスポーンする
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Invoke("GameRestart", respawnTime);
        }
    }

    public void GameRestart()
    {
        //現在のシーンを再度読み込む
        Debug.Log("現在のシーンを再度読み込む");
        //シーンリセット
        GameManager.instance.ActiveSceneReset();

        //Scene activeScene = SceneManager.GetActiveScene();
        //SceneManager.LoadScene(activeScene.name);
    }
}