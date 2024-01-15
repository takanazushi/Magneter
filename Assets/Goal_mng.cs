using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

        Debug.Log("ゴール");

        StartCoroutine(ResultLoad());
    }

    IEnumerator ResultLoad()
    {
        yield return wait;

        //todo:リザルトシーンに移動
        Debug.Log("リザルトシーンに移動");

        StartCoroutine(fadeOut.Execute("Result"));
    }

}
