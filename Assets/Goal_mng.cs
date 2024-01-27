using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class Goal_mng : MonoBehaviour
{
    public static Goal_mng instance;

    [SerializeField,Header("���U���g�ړ�����܂ł̑ҋ@���ԁi�b�j")]
    private float LoadWait;

    [SerializeField, Tooltip("�t�F�[�h�A�E�g�p�摜")]
    private GameObject Image;
    private string Image_Name = "FadeOutImage";
    private FadeOut fadeOut;

    [SerializeField, Header("�ʉߌ�X�v���C�g")]
    private Sprite passdSprite;

    [SerializeField, Header("�ʉߌ�Effect")]
    private GameObject passEffect;

    [SerializeField, Header("�ʉߌ�SE")]
    private AudioClip passSE;

    private SpriteRenderer spriteRenderer;
    private GameObject effect;
    private Light2D myLight;
    private AudioSource audioSource;

    /// <summary>
    /// �S�[���σt���O
    /// true:�S�[����
    /// </summary>
    bool Goalflg;

    WaitForSeconds wait;

    public bool Is_Goal
    {
        get { return Goalflg; }
        set { Goalflg = value; }
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
        if (SceneManager.GetActiveScene().name == "Title" || SceneManager.GetActiveScene().name == "StageSelect" || SceneManager.GetActiveScene().name == "Option" || SceneManager.GetActiveScene().name == "Result")
        {
            return;
        }

        if (Image == null)
        {
            Debug.Log("�t�F�[�h�A�E�g��Image�Ȃ�");

            // GameObject(1)��������
            GameObject parentObject = GameObject.Find("Canvas");

            // Camera_Child��������
            Transform childObject = parentObject.transform.Find("FadeImage");

            // Start_Camera_List��������
            GameObject fadeoutImage = childObject.Find("FadeOutImage").gameObject;

            // StartCamera�ɑ������
            Image = fadeoutImage;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("checkPoint��AudioSource���ĂȂ�");
        }

        wait =new WaitForSeconds(LoadWait);

        if (SceneManager.GetActiveScene().name == "Title" || SceneManager.GetActiveScene().name == "StageSelect" || SceneManager.GetActiveScene().name == "Option" || SceneManager.GetActiveScene().name == "Result")
        {
            return;
        }

        fadeOut = Image.GetComponent<FadeOut>();
        Image_Name = Image.name;

        spriteRenderer = GetComponent<SpriteRenderer>();
        myLight = GetComponent<Light2D>();
        myLight.enabled = false;
    }

    
    public void ResultStart()
    {
        Goalflg = true;
        effect = Instantiate(passEffect, transform.position, Quaternion.identity);
        effect.SetActive(true);
        audioSource.PlayOneShot(passSE);
        myLight.enabled = true;

        spriteRenderer.sprite = passdSprite;

        GameManager.instance.checkpointNo = -1;

        Debug.Log("�S�[��");

        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            GameManager.instance.stageClearFlag[1] = true;
        }
        else if (SceneManager.GetActiveScene().name == "Stage1")
        {
            GameManager.instance.stageClearFlag[2] = true;
        }
        else if(SceneManager.GetActiveScene().name == "Stage2")
        {
            GameManager.instance.stageClearFlag[3] = true;
        }

        StartCoroutine(ShowTargetAfterDelay(0.4f));

        StartCoroutine(ResultLoad());
    }

    private IEnumerator ShowTargetAfterDelay(float delay)
    {
        // �w�肵�����Ԃ����҂�
        yield return new WaitForSeconds(delay);

        // GameObject��\������
        effect.SetActive(false);
    }

    IEnumerator ResultLoad()
    {
        yield return wait;

        //todo:���U���g�V�[���Ɉړ�
        Debug.Log("���U���g�V�[���Ɉړ�");

        StartCoroutine(fadeOut.Execute("Result"));
    }

}
