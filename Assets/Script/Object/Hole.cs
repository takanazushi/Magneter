using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hole : MonoBehaviour
{
    [SerializeField, Header("���X�|�[������܂ł̎���")]
    private float respawnTime;

    //�v���C���[�ƂԂ������ꍇ�Q�[���V�[���ēǂݍ��݂Ń��X�|�[������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            GameManager.instance.ActiveSceneReset(SceneManager.GetActiveScene().name);
        }
    }

    public void GameRestart()
    {
        //���݂̃V�[�����ēx�ǂݍ���
        Debug.Log("���݂̃V�[�����ēx�ǂݍ���");
        //�V�[�����Z�b�g
        //GameManager.instance.ActiveSceneReset();

        //Scene activeScene = SceneManager.GetActiveScene();
        //SceneManager.LoadScene(activeScene.name);
    }
}