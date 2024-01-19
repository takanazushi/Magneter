using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeControl : MonoBehaviour
{
    public static GameTimeControl instance;

    /// <summary>
    /// �|�[�Y�t���O
    /// true:�|�[�Y��
    /// </summary>
    [SerializeField]
    private bool isPaused = false;
    public bool IsPaused
    {
        get { return isPaused; }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            //�C���X�^���X�����݂���ꍇ�͔j��
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        //�m�F�p�L�[
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    if (!isPaused)
        //    {
        //        GameTime_Stop();
        //    }
        //    else
        //    {
        //        GameTime_Start();
        //    }
        //}

    }

    /// <summary>
    /// �Q�[�����Ԃ��X�g�b�v
    /// </summary>
    public void GameTime_Stop()
    {
        // �Q�[�����ꎞ��~
        Time.timeScale = 0;
        isPaused = true;
        // �������Z����~
        Time.fixedDeltaTime = 0;
        Debug.Log("Stop");

    }

    /// <summary>
    /// �Q�[�����Ԃ��X�^�[�g
    /// </summary>
    public void GameTime_Start()
    {
        // �Q�[�����ĊJ
        Time.timeScale = 1;
        isPaused = false;
        // �������Z���ĊJ
        Time.fixedDeltaTime = 0.02f; // �ʏ�̒l�ɖ߂�
        Debug.Log("Start");
    }

}
