using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Walk_turn : MonoBehaviour
{
    [SerializeField]
    private float speed;

    //最初に左右どちらを進むか選べる変数　　✔で右　✔無し左
    [SerializeField,Header("チェックで右に移動開始")]
    private bool isMovingRight = false;

    //Capsuleのインスペクターのスクリプト、Squareのレイヤーをstageにしてます
    [SerializeField, Header("床レイヤーを設定")]
    private LayerMask stageLayer;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //todo
        if (IsGroundEnding())
        {
            FlipDirection();
        }
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
        Debug.Log("当");
        FlipDirection();
    }
}
