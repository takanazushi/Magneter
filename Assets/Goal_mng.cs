using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        wait=new WaitForSeconds(LoadWait);

        fadeOut = Image.GetComponent<FadeOut>();
        Image_Name = Image.name;

    }

    
    public void ResultStart()
    {
        Goalflg = true;

        Debug.Log("�S�[��");

        StartCoroutine(ResultLoad());
    }

    IEnumerator ResultLoad()
    {
        yield return wait;

        //todo:���U���g�V�[���Ɉړ�
        Debug.Log("���U���g�V�[���Ɉړ�");

        StartCoroutine(fadeOut.Execute("Result"));
    }

}
