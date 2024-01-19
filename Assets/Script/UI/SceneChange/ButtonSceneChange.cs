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
            Debug.LogError("Imageにフェードアウトのスクリプトついてませんよ～");
        }

        //if (audioSource == null)
        //{
        //    Debug.LogError("ボタンにオーディオソースついてないよ～");
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
