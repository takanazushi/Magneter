using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSceneChange : MonoBehaviour
{
    //[SerializeField]
    //private AudioClip buttonClickSE;

    [SerializeField]
    private SceneNameManager nextScene;

    [SerializeField]
    private GameObject Image;

    private FadeOut fadeOut;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        fadeOut = Image.GetComponent<FadeOut>();
        //audioSource = GetComponent<AudioSource>();

        if (fadeOut == null)
        {
            Debug.LogError("Image�Ƀt�F�[�h�A�E�g�̃X�N���v�g���Ă܂����`");
        }

        //if (audioSource == null)
        //{
        //    Debug.LogError("�{�^���ɃI�[�f�B�I�\�[�X���ĂȂ���`");
        //}
    }

    // Update is called once per frame
    public void OnClick()
    {
        //audioSource.PlayOneShot(buttonClickSE);
        StartCoroutine(fadeOut.Execute(nextScene.ToString()));
    }

    public void OnClickEnd()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;

        #else
            Application.Quit();

        #endif
    }
}
