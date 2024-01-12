using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
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

    /// <summary>
    /// �J�����J�n�ԍ�
    /// </summary>
    [SerializeField, Header("�J�n�J����")]
    private CinemachineVirtualCameraBase StartCamera;

    /// <summary>
    /// �X�^�[�g���̃J�����J�ڒ��t���O
    /// </summary>
    [SerializeField]
    bool Ster_Camera_end;
    public bool Is_Ster_camera_end
    {
        get { return Ster_Camera_end; }
        set { Ster_Camera_end = value;}
    }

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

        //�J�����̑J�ڊJ�n
        SetStaetCamera();

    }

    public void SetStaetCamera()
    {
        StartCamera.Priority = 1;
    }

    //HP�擾
    public int GetHP()
    {
        return HP;
    }
}
