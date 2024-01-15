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

    public bool[] stageClearFlag = new bool[4] { true, false, false, false };

    //Scene���L���ɂȂ�����
    private void OnEnable()
    {
        //�����I��Method�Ăяo��
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //Scene�������ɂȂ�����
    private void OnDisable()
    {
        //�����I��Method�폜
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //Scene���ǂݍ��܂��x�ɌĂяo��
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name == "Title" || SceneManager.GetActiveScene().name == "StageSelect"|| SceneManager.GetActiveScene().name == "Option")
        {
            return;
        }
        else if (StartCamera == null && checkpointNo == -1)
        {

            Debug.Log("StartCamera�Ȃ�");

            // GameObject(1)��������
            GameObject parentObject = GameObject.Find("Camera");

            // Camera_Child��������
            Transform childObject = parentObject.transform.Find("Camera_Control");

            // Start_Camera_List��������
            CinemachineVirtualCameraBase startCameraList = childObject.Find("Start_Camera_List").GetComponent<CinemachineVirtualCameraBase>();

            // StartCamera�ɑ������
            StartCamera = startCameraList;
        }
    }

    void Awake()
    {
       
        if (instance == null)
        {
            instance = this;
            //�V�[���ԂŃI�u�W�F�N�g���j������Ȃ��悤�ɂ���
            DontDestroyOnLoad(gameObject);

            Debug.Log("�Q�[���}�l�[�W���[���݂��܂��B");
            Debug.Log("�v���C���[�̃`�F�b�N�|�C���g" + checkpointNo);
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
        if (SceneManager.GetActiveScene().name == "Title" || SceneManager.GetActiveScene().name == "StageSelect"|| SceneManager.GetActiveScene().name == "Option")
        {
            return;
        }

        StartCamera.Priority = 1;


    }

    //HP�擾
    public int GetHP()
    {
        return HP;
    }
}
