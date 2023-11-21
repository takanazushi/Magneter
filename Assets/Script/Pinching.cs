using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Pinching : MonoBehaviour
{
    [SerializeField, Header("リスポーンするまでの時間")]
    private float respawnTime;

    //矩形がめり込んだ場合場合ゲームシーン再読み込みでリスポーンする
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
        Scene activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.name);
    }

}