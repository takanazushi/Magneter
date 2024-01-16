using System.Collections;
using UnityEngine;

[RequireComponent (typeof (BoxCollider2D))]
public class Floor_Fade : MonoBehaviour
{
    /// <summary>
    /// 現れている時間
    /// </summary>
    [SerializeField, Header("現れている時間")]
    private float In_Time;

    /// <summary>
    /// 消えている時間
    /// </summary>
    [SerializeField, Header("消えている時間")]
    private float Out_Time;

    /// <summary>
    /// 消えていく速度
    /// </summary>
    [SerializeField,Header("消える速度")]
    private float Out_Speed;
    
    /// <summary>
    /// 表れる速度
    /// </summary>
    [SerializeField,Header("現れる速度")]
    private float In_Speed;

    [SerializeField]
    Sprite[] sprites; 
    
    /// <summary>
    /// フェードスピード
    /// </summary>
    float Fade_speed;

    /// <summary>
    /// 自身のSpriteRenderer
    /// </summary>
    private SpriteRenderer spRenderer;

    /// <summary>
    /// 自身のBoxCollider2D
    /// </summary>
    private BoxCollider2D boxC;

    /// <summary>
    /// 遷移中フラグ
    /// </summary>
    bool fadeflg;

    /// <summary>
    /// 表示待機時間
    /// </summary>
    WaitForSeconds In_Wait;

    /// <summary>
    /// 消失待機時間
    /// </summary>
    WaitForSeconds Out_Wait;

    /// <summary>
    /// 現れている時間段階カウント
    /// </summary>
    int In_Time_count;

    void Start()
    {
        In_Time_count=0;
        spRenderer = GetComponent<SpriteRenderer>();
        boxC = GetComponent<BoxCollider2D>();
        spRenderer.sprite = sprites[In_Time_count];
        In_Wait = new(In_Time / sprites.Length);
        Out_Wait = new(Out_Time);

        fadeflg = false;

        //表示時間待機
        StartCoroutine(Fade_InWait());
    }

    /*α値は内部的に1～0の間を行き来するが、値を少し多く入れることで
            消え始めるまで、また現れ初めるまでにラグを生じさせている*/
    void Update()
    {
        if (fadeflg)
        {
            //アルファ値を設定
            SetRendereAlpha(spRenderer.color.a + Fade_speed * Time.timeScale);

            if (spRenderer.color.a < 0.0f)
            {
                //当たり判定を一時的に消す
                boxC.enabled = false;

                //遷移終了
                fadeflg = false;

                //アルファ値を設定
                SetRendereAlpha(0.0f);

                //カウント初期化
                In_Time_count = 0;

                //画像設定
                spRenderer.sprite = sprites[In_Time_count];

                //消失時間待機
                StartCoroutine(Fade_OutWait());
            }
            else if (spRenderer.color.a >= 1)
            {
                //アルファ値を設定
                SetRendereAlpha(1.0f);

                //当たり判定を一時復活させる
                boxC.enabled = true;
                //遷移終了
                fadeflg = false;

                //表示時間待機
                StartCoroutine(Fade_InWait());
            }
        }
    }

    /// <summary>
    /// 消失時間分待機
    /// </summary>
    /// <returns></returns>
    IEnumerator Fade_OutWait()
    {
        yield return Out_Wait;

        //遷移中に移行
        fadeflg = true;

        //遷移スピードを設定
        Fade_speed = In_Speed;
    }

    /// <summary>
    /// 表示時間分待機
    /// </summary>
    /// <returns></returns>
    IEnumerator Fade_InWait()
    {
        yield return In_Wait;
        In_Time_count++;

        if (In_Time_count >= sprites.Length)
        {
            //遷移中に移行
            fadeflg = true;

            //遷移スピードを設定
            Fade_speed = -Out_Speed;
            spRenderer.sprite = sprites[In_Time_count-1];

        }
        else
        {
            spRenderer.sprite = sprites[In_Time_count];
            StartCoroutine(Fade_InWait());
        }
    }

    /// <summary>
    /// 自分のアルファ値を設定します
    /// </summary>
    /// <param name="a"></param>
    void SetRendereAlpha(float a)
    {
        spRenderer.color = new(spRenderer.color.r, spRenderer.color.g, spRenderer.color.b,
            a);
    }

    /// <summary>
    /// inspectorの値が変更されたとき
    /// </summary>
    void OnValidate()
    {
        //再設定
        In_Wait = new(In_Time / sprites.Length);
        Out_Wait = new(Out_Time);
    }

    private void Reset()
    {
        In_Time = 3;
        Out_Time = 3;
        Out_Speed = 0.1f;
        In_Speed = 0.1f;
    }
}