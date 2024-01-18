using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Menu : MonoBehaviour
{
    /// <summary>
    /// �w�i�D�F�摜
    /// </summary>
    [SerializeField]
    GameObject back_ash;

    [SerializeField]
    List<GameObject> BottaList;



    //���j���[�t���O
    bool menuflg = false;

    // Start is called before the first frame update
    void Start()
    {
        menuflg = false;
        back_ash.SetActive(false);

        foreach (GameObject botta in BottaList)
        {
            botta.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //�X�^�[�g���̃J�����J�ڏI����
        //�|�[�Y���łȂ��Ƃ�
        //�|�[�Y�������j���[���J����Ă��鎞
        if (Input.GetKeyDown(KeyCode.Escape) &&
            GameManager.instance.Is_Ster_camera_end)
        {
            if (menuflg)
            {
                MenuEnd();
            }
            else if(!GameTimeControl.instance.IsPaused)
            {
                MenuStart();
            }
        }

        if (menuflg)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {

            }

        }
    }

    /// <summary>
    /// ���j���[�I��
    /// </summary>
    public void MenuEnd()
    {
        GameTimeControl.instance.GameTime_Start();
        menuflg = false;
        back_ash.SetActive(false);

        foreach (GameObject botta in BottaList)
        {
            botta.SetActive(false);
        }
    }
    /// <summary>
    /// ���j���[�J�n
    /// </summary>
    public void MenuStart()
    {
        GameTimeControl.instance.GameTime_Stop();
        menuflg = true;
        back_ash.SetActive(true);

        foreach (GameObject botta in BottaList)
        {
            botta.SetActive(true);
        }
    }

    /// <summary>
    /// �Q�[�����X�^�[�g
    /// </summary>
    public void GameReStart() 
    {
        GameTimeControl.instance.GameTime_Start();
        menuflg = false;
        back_ash.SetActive(false);

        foreach (GameObject botta in BottaList)
        {
            botta.SetActive(false);
        }
        GameManager.instance.checkpointNo = -1;
        GameManager.instance.Is_Ster_camera_end = false;
        GameManager.instance.ActiveSceneReset(SceneManager.GetActiveScene().name);
    }

    public void StageSelect_SceneLoad()
    {
        GameTimeControl.instance.GameTime_Start();
        GameManager.instance.ActiveSceneReset("StageSelect");
    }
}
