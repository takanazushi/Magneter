using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class Goal_mng : MonoBehaviour
{
    public static Goal_mng instance;

    [SerializeField,Header("リザルト移動するまでの待機時間（秒）")]
    private float LoadWait;

    [SerializeField, Tooltip("フェードアウト用画像")]
    private GameObject Image;
    private string Image_Name = "FadeOutImage";
    private FadeOut fadeOut;

    [SerializeField, Header("通過後スプライト")]
    private Sprite passdSprite;

    [SerializeField, Header("通過後Effect")]
    private GameObject passEffect;

    [SerializeField, Header("通過後SE")]
    private AudioClip passSE;

    private SpriteRenderer spriteRenderer;
    private GameObject effect;
    private Light2D myLight;
    private AudioSource audioSource;

    /// <summary>
    /// ゴール済フラグ
    /// true:ゴール済
    /// </summary>
    bool Goalflg;

    WaitForSeconds wait;

    public bool Is_Goal
    {
        get { return Goalflg; }
        set { Goalflg = value; }
    }

    //Sceneが有効になった時
    private void OnEnable()
    {
        //自動的にMethod呼び出し
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //Sceneが無効になった時
    private void OnDisable()
    {
        //自動的にMethod削除
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //Sceneが読み込まれる度に呼び出し
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name == "Title" || SceneManager.GetActiveScene().name == "StageSelect" || SceneManager.GetActiveScene().name == "Option" || SceneManager.GetActiveScene().name == "Result")
        {
            return;
        }

        if (Image == null)
        {
            Debug.Log("フェードアウトのImageない");

            // GameObject(1)を見つける
            GameObject parentObject = GameObject.Find("Canvas");

            // Camera_Childを見つける
            Transform childObject = parentObject.transform.Find("FadeImage");

            // Start_Camera_Listを見つける
            GameObject fadeoutImage = childObject.Find("FadeOutImage").gameObject;

            // StartCameraに代入する
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
            Debug.LogError("checkPointにAudioSourceついてない");
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

        Debug.Log("ゴール");

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
        // 指定した時間だけ待つ
        yield return new WaitForSeconds(delay);

        // GameObjectを表示する
        effect.SetActive(false);
    }

    IEnumerator ResultLoad()
    {
        yield return wait;

        //todo:リザルトシーンに移動
        Debug.Log("リザルトシーンに移動");

        StartCoroutine(fadeOut.Execute("Result"));
    }

}
