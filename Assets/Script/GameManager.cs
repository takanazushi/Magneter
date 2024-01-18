using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    /// <summary>
    /// �v���C���[�̑���\����
    /// true:���얳��
    /// </summary>
    bool Player_PlayFlg;
    public bool Is_Player_StopFlg
    {
        get { return Player_PlayFlg; }
        set { Player_PlayFlg = value; }
    }

    [SerializeField, Tooltip("�t�F�[�h�A�E�g�p�摜")]
    private GameObject Image;
    private string Image_Name = "FadeOutImage";
    private FadeOut fadeOut;

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
        if (SceneManager.GetActiveScene().name == "Title" || SceneManager.GetActiveScene().name == "StageSelect"|| SceneManager.GetActiveScene().name == "Option"|| SceneManager.GetActiveScene().name == "Result")
        {
            GameManager.instance.Is_Ster_camera_end = false;
            return;
        }


        if (StartCamera == null)
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

        //�v���C���[��HP�����Z�b�g����
        GameManager.instance.HP = GameManager.instance.RestHP;


        fadeOut = Image.GetComponent<FadeOut>();
        Image_Name = Image.name;

    }

    void Awake()
    {
       
        if (instance == null)
        {
            instance = this;
            //�V�[���ԂŃI�u�W�F�N�g���j������Ȃ��悤�ɂ���
            DontDestroyOnLoad(gameObject);

            //Debug.Log("�Q�[���}�l�[�W���[���݂��܂��B");
            //Debug.Log("�v���C���[�̃`�F�b�N�|�C���g" + checkpointNo);
        }
        else
        {
            //�C���X�^���X�����݂���ꍇ�͔j��
            Destroy(gameObject);
        }

        if (SceneManager.GetActiveScene().name == "Title" || SceneManager.GetActiveScene().name == "StageSelect" || SceneManager.GetActiveScene().name == "Option" || SceneManager.GetActiveScene().name == "Result")
        {
            return;
        }

        if (!Ster_Camera_end)
        {
            //�J�����̑J�ڊJ�n
            SetStaetCamera();
        }

        SceneManager.activeSceneChanged += SetFadeOutOj;

        fadeOut = Image.GetComponent<FadeOut>();
        Image_Name = Image.name;
    }

    
    void SetFadeOutOj(Scene thisScene, Scene nextScene)
    {
        Image = GameObject.Find(Image_Name);

        if(Image != null)
        {
            Debug.Log("�V�[�����ւ����F" + Image.name);
            fadeOut = Image.GetComponent<FadeOut>();
            Image_Name = Image.name;
        }

    }

    /// <summary>
    /// ���݂̃V�[�����ēx�ǂݍ���
    /// </summary>
    public void ActiveSceneReset(string scenename)
    {
        Player_PlayFlg = true;

        Debug.Log(fadeOut.name);
        StartCoroutine(fadeOut.Execute(SceneManager.GetActiveScene().name));

        //todo �O��̌o�ߎ��Ԃ�ۑ�
        PlayerPrefs.SetFloat("PreviousElapsedTime", ClearTime.instance.second);

        
        //���݂̃V�[�����ēx�ǂݍ���
        //Scene activeScene = SceneManager.GetActiveScene();
        //SceneManager.LoadScene(activeScene.name);
    }

    public void SetStaetCamera()
    {
        if (SceneManager.GetActiveScene().name == "Title" || SceneManager.GetActiveScene().name == "StageSelect"|| SceneManager.GetActiveScene().name == "Option" || SceneManager.GetActiveScene().name == "Result")
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
