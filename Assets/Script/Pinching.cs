using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Pinching : MonoBehaviour
{
    [SerializeField, Header("���X�|�[������܂ł̎���")]
    private float respawnTime;

    //��`���߂荞�񂾏ꍇ�ꍇ�Q�[���V�[���ēǂݍ��݂Ń��X�|�[������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Invoke("GameRestart", respawnTime);
        }
    }

    public void GameRestart()
    {
        //���݂̃V�[�����ēx�ǂݍ���
        Debug.Log("���݂̃V�[�����ēx�ǂݍ���");
        Scene activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.name);
    }

}