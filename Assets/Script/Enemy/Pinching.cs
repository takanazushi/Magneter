using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Pinching : MonoBehaviour
{
    [SerializeField, Header("リスポーンするまでの時間")]
    private float respawnTime;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name != "Player")
        {
            Debug.Log("Playerがめり込んだ" + collision.gameObject.name);
            Invoke(nameof(GameRestart), respawnTime);
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