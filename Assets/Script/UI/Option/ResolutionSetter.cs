using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class ResolutionSetter : MonoBehaviour
{
    [SerializeField, Header("�e�L�X�g�C���[�W�̔z��")]
    private Image[] texts = new Image[3];

    //[SerializeField]
    //private PlayerManager playerManager;

    private int count = 0;

    private void Start()
    {
        SetStartResolution();
    }

    /// <summary>
    /// �E�������{�^�������������̏���
    /// </summary>
    public void IncrementCount()
    {
        count++;

        if (count > 2)
        {
            count = 0;
        }
        //count = (count + 1) % 3;

        Debug.Log(count);


        SetResolution();
    }

    /// <summary>
    /// ���������{�^�������������̏���
    /// </summary>
    public void DecrementCount()
    {
        count--;
        if (count < 0)
        {
            count = 2;
        }

        Debug.Log(count);

        //count = (count - 1 + 3) % 3;

        SetResolution();
    }


    //private void SetResolution()
    //{
    //    switch (count)
    //    {
    //        case 0:
    //            Screen.SetResolution(1920, 1080, false);
    //            texts[0].enabled = true;
    //            texts[1].enabled = false;
    //            texts[2].enabled = false;
    //            break;

    //        case 1:
    //            Screen.SetResolution(1280, 720, false);
    //            texts[0].enabled = false;
    //            texts[1].enabled = true;
    //            texts[2].enabled = false;
    //            break;

    //        case 2:
    //            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.width, true);
    //            texts[0].enabled = false;
    //            texts[1].enabled = false;
    //            texts[2].enabled = true;
    //            break;
    //    }
    //}

    private void SetStartResolution()
    {
        //�����擾
        int screenWidth = Screen.currentResolution.width;

        //�t���T�C�Y���ǂ���
        bool isFullScreen = Screen.fullScreen;

        //�������ݒ�
        //������t���T�C�Y���ۂ��Őݒ���s��
        if (isFullScreen)
        {
            count = 2;
            DisableTexts(0, 1);
        }
        else if (screenWidth == 1080)
        {
            count = 1;
            DisableTexts(0, 2);
        }
        else if (screenWidth == 1920)
        {
            count = 0;
            DisableTexts(1, 2);
        }
    }

    /// <summary>
    /// �𑜓x�̐ݒ�ƁA�\��UI�̐ݒ�
    /// </summary>
    private void SetResolution()
    {
        switch (count)
        {
            case 0:
                SetAndEnableResolution(1920, 1080, false);
                DisableTexts(1, 2);
                break;

            case 1:
                SetAndEnableResolution(1280, 720, false);
                DisableTexts(0, 2);
                break;

            case 2:
                SetAndEnableResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
                DisableTexts(0, 1);
                break;
        }
    }

    /// <summary>
    /// �𑜓x�̐ݒ�E�Ή�����e�L�X�g��L���ɂ��鏈��
    /// </summary>
    /// <param name="width">��ʂ̉���</param>
    /// <param name="height">��ʂ̏c��</param>
    /// <param name="flag">Ture�F�t���T�C�Y�@False�F�E�B���h�E</param>
    private void SetAndEnableResolution(int width, int height, bool flag)
    {
        Screen.SetResolution(width, height, flag);
        texts[count].enabled = true;
    }

    /// <summary>
    /// �w�肵���e�L�X�g���\���ɂ���
    /// </summary>
    /// <param name="count">��\���ɂ������e�L�X�g�̓Y����</param>
    private void DisableTexts(params int[] count)
    {
        foreach (int index in count)
        {
            texts[index].enabled = false;
        }
    }
}
