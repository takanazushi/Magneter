using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static Magnet;

public class Enemy_Walk_turn : MonoBehaviour
{
    [SerializeField]
    private float speed;

    /// <summary>
    /// 磁力抵抗限界値
    /// </summary>
    [SerializeField, Header("磁力抵抗限界値")
        , Tooltip("この値以上の力を受けると反転します")]
    float X_Resist;

    //最初に左右どちらを進むか選べる変数　　✔で右　✔無し左
    [SerializeField,Header("チェックで右に移動開始")]
    private bool isMovingRight = false;

    //Capsuleのインスペクターのスクリプト、Squareのレイヤーをstageにしてます
    [SerializeField, Header("床レイヤーを設定")]
    private LayerMask stageLayer;

    /// <summary>
    /// アニメーター
    /// </summary>
    [SerializeField]
    private Animator animator;

    /// <summary>
    /// Light2D
    /// </summary>
    [SerializeField]
    private Light2D light2D;

    /// <summary>
    /// 敵用マグネット
    /// </summary>
    [SerializeField, HideInInspector]
    private Magnet magnet;

    [SerializeField, HideInInspector]
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        magnet = GetComponent<Magnet>();
        X_Resist = 0.3f;

        MagnetColorChange();
    }

    private void Update()
    {
        //todo
        if (IsGroundEnding())
        {
            FlipDirection();
        }

        MagnetColorChange();
    }

    private void FixedUpdate()
    {
        EnemyMove();
    }

    ///<summary>
    ///　地面に接地していないかどうかの判定を行います
    ///</summary>
    /// <returns>レイが地面に当たっていなければTrue</returns>
    private bool IsGroundEnding()
    {
        Vector3 direction;

        //右向きの場合、右方向にRayを飛ばす
        if (isMovingRight)
        {
            direction = Vector3.right;
        }
        else 
        {
            direction = Vector3.left;
        }

        //Rayの開始地点設定
        Vector3 startposition = transform.position + direction * 0.3f;

        //Rayの終了地点設定
        Vector3 endposition = startposition - transform.up * 1.5f;

        //デバック用の線
        Debug.DrawLine(startposition, endposition, Color.red);

        //Rayが地面に当たらなければTrueを返す
        return !Physics2D.Linecast(startposition, endposition, stageLayer);
    }

    ///<summary>
    ///　反転処理を行います
    ///</summary>
    private void FlipDirection()
    {
        //現在の移動方向を反転
        isMovingRight = !isMovingRight;

        //向きの変更
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    ///<summary>
    ///　敵の移動処理を行います
    ///</summary>
    private void EnemyMove()
    {
        Vector2 mg_speed = Vector2.zero;

        //磁力の影響ある場合
        if (magnet)
        {
            if (magnet.PuroTypeManet != Type_Magnet.None &&
                magnet.PuroTypeManet != Type_Magnet.Exc)
            {
                //除外に自身のタグを設定
                mg_speed = magnet.Magnet_Power(new string[] { tag });

                //横方向に一定以上の力を受けた場合
                //todo:抵抗値を設定して平面上ならダイジョブですが、
                //自分が磁気を付与などで、抵抗値を越える場所に長く滞在する場合ブルブルしちゃう
                if (Mathf.Abs(mg_speed.x) >= Mathf.Abs(X_Resist))
                {
                    //左右反転
                    FlipDirection();
                }
            }
        }

        //右方向に移動
        if (isMovingRight)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("当");
        FlipDirection();
    }

    private void SetAnimation(string animationName)
    {
        //アニメーションを再生
        animator.Play(animationName);
    }

    private void MagnetColorChange()
    {
        if (light2D.enabled == false)
        {
            light2D.enabled = true;
        }

        if (magnet.PuroTypeManet == Type_Magnet.S)
        {
            Debug.Log("S極です");
            SetAnimation("Enemy_Blue_walk");
            light2D.color = Color.blue;

        }
        else if (magnet.PuroTypeManet == Type_Magnet.N)
        {
            Debug.Log("N極です");
            SetAnimation("Enemy_Red_walk");
            light2D.color = Color.red;
        }
        else if (magnet.PuroTypeManet == Type_Magnet.None)
        {
            Debug.Log("極指定してないです");
            SetAnimation("Enemy_None_walk");
            light2D.enabled = false;
        }
    }
}
