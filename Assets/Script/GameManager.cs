using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField, Header("�Q�[���̒��ԃ|�C���g��ێ� �����l-1")]
    public int checkpointNo = -1;
    [SerializeField, Header("�f�X��̃v���C���[����HP��ێ�")]
    public int RestHP = 3;
    [SerializeField, Header("�v���C���[HP��ێ�")]
    public int HP = 3;

    public bool[] stageClearFlag = new bool[3] { true, false, false };


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //�V�[���ԂŃI�u�W�F�N�g���j������Ȃ��悤�ɂ���
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //�C���X�^���X�����݂���ꍇ�͔j��
            Destroy(gameObject);
        }
    }

    //HP�擾
    public int GetHP()
    {
        return HP;
    }
}
