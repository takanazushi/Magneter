using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Floor_Fade : MonoBehaviour
{   
    
    //α値
    [SerializeField,Header("α値")]
    private float cloa = 0.0f;
    
    //消えていく速度
    [SerializeField,Header("消えていく速度")]
    private float fadeSp = 0.01f;
    
    //表れる速度
    [SerializeField,Header("表れる速度")]
    private float EmergeSp = 0.01f;
    
    //現れている時間
    [SerializeField,Header("現れている時間")]
    private float EmergeT = 3.0f;

    //消えている時間
    [SerializeField,Header("消えている時間")]
    private float FadeT = 10.0f;


    private SpriteRenderer spRenderer;
    private BoxCollider2D boxC;


    void Start()
    {
        spRenderer = GetComponent<SpriteRenderer>();
        boxC = GetComponent<BoxCollider2D>();
    }

    /*α値は内部的に1～0の間を行き来するが、値を少し多く入れることで
            消え始めるまで、また現れ初めるまでにラグを生じさせている*/
    void Update()
    {   //表れたとき
        if (boxC.enabled == true)
        {
            //消えはじめる
            cloa = EmergeT;
            StartCoroutine(Fade());

        }
        //消えたとき
        else if (boxC.enabled == false)
        {   //現れはじめる
            cloa = -FadeT;
            StartCoroutine(Emerge());

            return;
        }


    }
    /*Fade
     *消えていく際に0まで判定が生きている
     * 
     *Emerge
     *完全に表れるまで(1.0)まで当たり判定はなし
     *つまり、0から1に移行するまでは当たり判定はない
     */
    IEnumerator Fade()
    {
        while (cloa > 0f)
        {   //α値を減らす
            cloa -= fadeSp;
            spRenderer.color = new Color(spRenderer.color.r, spRenderer.color.g, spRenderer.color.b, cloa);
            //1フレーム更新する    
            yield return null;

        }

        if (cloa < 0.0f)
        {
            //当たり判定を一時的に消す
            boxC.enabled = false;


        }

    }

    IEnumerator Emerge()
    {

        while (cloa < 1f)
        {//α値を増やす
            cloa += EmergeSp;
            spRenderer.color = new Color(spRenderer.color.r, spRenderer.color.g, spRenderer.color.b, cloa);
            //1フレーム更新する 
            yield return null;


        }

        if (cloa >= 1)
        {
            cloa = 1f;
            //当たり判定を一時復活させる
            boxC.enabled = true;

        }



    }


}