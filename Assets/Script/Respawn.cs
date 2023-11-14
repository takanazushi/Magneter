using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
{
    [SerializeField, Header("リスポーンするまでの時間")]
    private float respawnTime;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Invoke("GameRestart", respawnTime);
        }
    }

    private void GameRestart()
    {
        //現在のシーンを再度読み込む
        Debug.Log("現在のシーンを再度読み込む");
        Scene activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.name);
    }
}
