using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Pinching : MonoBehaviour
{
    [SerializeField, Header("���X�|�[������܂ł̎���")]
    private float respawnTime;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name != "Player")
        {
            Debug.Log("Player���߂荞��" + collision.gameObject.name);
            Invoke(nameof(GameRestart), respawnTime);
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