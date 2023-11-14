using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
{
    [SerializeField, Header("���X�|�[������܂ł̎���")]
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
        //���݂̃V�[�����ēx�ǂݍ���
        Debug.Log("���݂̃V�[�����ēx�ǂݍ���");
        Scene activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.name);
    }
}
