using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.SceneManagement;

public class ClearTime : MonoBehaviour
{
    //todo GameObjct Timer�Ŏg�p

    public static ClearTime instance;

    private TextMeshProUGUI timerText;
    //�^�C�}�[
    public float second;
    //��
    public int minute;
    //��
    private int hour;

    //�^�C�}�[���Z�b�g
    private bool resetflg;

    // Start is called before the first frame update
    void Start()
    {
        resetflg = false;
        //�o�ߎ��ԓǂݍ���
        ResumeTimer();
        timerText = GetComponent<TextMeshProUGUI>();
    }

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

        if (SceneManager.GetActiveScene().name == "Title" || SceneManager.GetActiveScene().name == "StageSelect" || SceneManager.GetActiveScene().name == "Option")
        {
            ClearTime.instance.second = 0;
            ClearTime.instance.minute = 0;
            return;
        }
    }


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

    // Update is called once per frame
    void Update()
    {
        //false�̎��ɕۑ����Ă�^�C�}�[�����Z�b�g
        if(!resetflg)
        {
            PlayerPrefs.DeleteAll();
            resetflg = true;
        }
        //�S�[���܂Ń^�C�}�[���Z
        if (Goal_mng.instance != null&& !Goal_mng.instance.Is_Goal) 
        {
            second += Time.deltaTime;
        }

        //����60�𒴂����玞��1�𑫂�
        if (minute > 60)
        {
            hour++;
            minute = 0;
        }
        //�b��60�𒴂����番��1�𑫂�
        if (second > 60f)
        {
            minute += 1;
            second = 0;
        }
       
        //�`��
        //timerText.text = hour.ToString() + ":" + minute.ToString("00") + ":" + second.ToString("f2");

    }

    public void ResumeTimer()
    {
        // �ۑ����ꂽ�o�ߎ��Ԃ�ǂݍ��݁Asecond�ɐݒ肷��
        second = PlayerPrefs.GetFloat("PreviousElapsedTime", 0f);
    }

    //private static void BuildPlayerHandler(BuildPlayerOptions options)
    //{
    //    PlayerPrefs.DeleteAll();
    //}
}
