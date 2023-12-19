using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    [SerializeField, Header("横移動の速さ")]
    private float walkMoveX;

    [SerializeField, Header("ジャンプ中の横移動の速さ")]
    private float jumpMoveX;

    [SerializeField, Header("ジャンプの高さ")]
    private float jumpForce = 350f;

    [SerializeField, Header("どのレイヤーのオブジェクトと当たり判定をするか")]
    LayerMask groundLayers = 0;

    [SerializeField, Header("レイの長さ"),
        Tooltip("")]
    float rayLength = 1.0f;

    /// <summary>
    /// 足場に触れている場合のみ有効
    /// </summary>
    private LineMoveFloor moveFloor=null;

    [SerializeField, Header("風の減速値")]
    private float windMoveSpeed = 0.0f;

    //ベルトコンベアに乗った時の変数
    private float converspeed;
    //ベルトコンベアに流れてるブロックに乗った時の変数
    private float blockspeed;

    /// <summary>
    /// ジャンプカウント
    /// </summary>
    private int jumpCount = 0;

    /// <summary>
    /// ジャンプフラグ
    /// true:ジャンプ中
    /// </summary>
    bool jumpflag = false;

    private Rigidbody2D rb;

    //レイの衝突情報
    private RaycastHit2D[] raycastHit2D = new RaycastHit2D[2];

    private SpriteRenderer spriteRenderer;

    private Animator anim = null;

    private Player_HP playerHP;

    void Start()
    {
        if (GameManager.instance.checkpointNo > -1)
        {
            // ワープ先のチェックポイントオブジェクトを見つける("checkpoint (1)" のような名前になっているもの）
            GameObject checkpointObject = GameObject.Find("checkpoint (" + GameManager.instance.checkpointNo + ")");

            // チェックポイントオブジェクトが見つかった場合は、プレイヤーをワープさせる
            if (checkpointObject != null)
            {
                transform.position = checkpointObject.transform.position;
            }
            else
            {
                Debug.Log(GameManager.instance.checkpointNo + "チェックポイントを通過していない");
            }
        }

        rb = GetComponent<Rigidbody2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        playerHP = GetComponent<Player_HP>();

        if (spriteRenderer == null)
        {
            Debug.LogError("spriteレンダラーない");
        }

        jumpflag = false;

        anim=GetComponent<Animator>();

        spriteRenderer.flipX = true;
    }

    // 物理演算をしたい場合はFixedUpdateを使うのが一般的
    void FixedUpdate()
    {
        //重力を追加で掛ける
        //Rigidbody2D->GravityScaleからいじるか迷い中・・・
        //rb.velocity = new(rb.velocity.x, rb.velocity.y - 0.5f);

        //マウスの位置を取得
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //プレイヤーの位置よりも右にマウスがある場合
        //右移動keyを押した場合
        //右向き
        if (mousePosition.x > transform.position.x)
        {
            spriteRenderer.flipX = true;
            SetChildObjectRotation(true);
        }
        else if (mousePosition.x < transform.position.x)
        {
            spriteRenderer.flipX = false;
            SetChildObjectRotation(false);
        }

        //レイの処理結果を受け取る
        raycastHit2D = CheckGroundStatus();

        for (int i = 0; i < 2; i++)
        {
            //レイ接触時のみジャンプ可能
            if (raycastHit2D[i].collider || moveFloor != null)
            {
                //ジャンプ初期化
                jumpCount = 0;
                jumpflag = false;

            }
        }

        for (int i = 0; i < 2; i++)
        {
            //レイ接触時のみジャンプ可能
            if (raycastHit2D[i].collider|| moveFloor != null)
            {
                //ジャンプ判定
                PlayerJump();
            }
        }

        if (playerHP.Inviflg == true)
        {
            anim.SetBool("damage", true);
        }
        else
        {
            anim.SetBool("damage", false);
        }

        //移動処理
        PlayerWalk();

        //ベルトコンベアの速度を足して通常速度より速くなった時
        if(rb.velocity.x > 5.0 || rb.velocity.x < -5.0)
        {
            jumpMoveX += 0.1f;
            //最大値
            if(jumpMoveX >= 7.0)
            {
                jumpMoveX = 7.0f;
            }
        }

        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //if (other.gameObject.tag == "MoveFloor")

        //if (other.gameObject.tag == "kabe")
        //{
        //    moveFloor = other.gameObject.GetComponent<LineMoveFloor>();
        //    //Debug.Log("動く床と当たってる");
        //}

        //Debug.Log("ジャンプフラグは：" + jumpflag);

        //todo ゴールに触れたらタイマーストップ
        if(other.gameObject.tag == "Goal")
        {
            Goal_mng.instance.Is_Goal = true;
        }

        //Jump値リセット
        jumpMoveX = 3.0f;
        jumpCount = 0;
        jumpflag = false;
        anim.SetBool("jump", false);

        //Debug.Log("ジャンプフラグは：" + jumpflag);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //ベルトコンベアに流れてるブロックに乗った時
        if (collision.gameObject.name == "ConverBlock")
        {
            //ブロックのスピード取得
            blockspeed = CoverVeltThing.Instance.returnSpeed();
        }
        //右に流れるベルトコンベアの時
        if (collision.gameObject.name == "PlusVeltConver")
        {
            converspeed = 3;
        }
        //左に流れるベルトコンベアの時
        if (collision.gameObject.name == "MinusVeltConver")
        {
            converspeed = -3;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("MoveFloor"))
        {
            moveFloor = null;
            // Debug.Log("動く床と当たってない");
        }

        converspeed = 0;
      
        //ブロックから離れた時
        if (collision.gameObject.name == "ConverBlock")
        {
            blockspeed = 0;
        }

        //if (collision.gameObject.CompareTag("Goal"))
        //{
        //    //Goal_mng.instance.Is_Goal = true;
        //}
    }

    private void PlayerWalk()
    {

        //横移動を取得
        float horizontalInput = Input.GetAxis("Horizontal");

        //風に当たっている状態の速度取得

        if (Wind.instance != null)
        {
            windMoveSpeed = Wind.instance.getMoveSpeed;
        }
        else
        {
            windMoveSpeed = 0;
        }

        //横移動スピード
        float Lateralspeed;

        //ジャンプ中は横移動速度を切り替える
        if (jumpflag)
        {
            Lateralspeed = jumpMoveX;
        }
        else
        {
            Lateralspeed = walkMoveX;
        }

        //左右反転
        if (horizontalInput > 0)
        {
            spriteRenderer.flipX = true;
            anim.SetBool("move", true);
            //transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
        }
        else if (horizontalInput < 0)
        {
            spriteRenderer.flipX = false;
            anim.SetBool("move", true);
            //transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 1);
        }
        else
        {
            anim.SetBool("move", false);
        }
       
        
        //足場に乗っている場合
        Vector2 floorVelocity = Vector2.zero;
        if (moveFloor != null)
        {
            floorVelocity = moveFloor.GetVelocity();
        }

        //速度生成
        Vector2 speed = new (horizontalInput, rb.velocity.y);

        //スピード乗算
        speed.x = speed.x * Lateralspeed;

        if (windMoveSpeed != 0)
        {
            speed.x += Lateralspeed / windMoveSpeed;
        }


        ////足場の移動速度を追加
        speed.x += floorVelocity.x;
        speed.y = rb.velocity.y;

        rb.velocity = speed;

        //Debug.Log(floorVelocity);

        //追加7 コンベアとブロックのスピード加算
        rb.velocity += new Vector2(converspeed + blockspeed, 0);
    }

    private void PlayerJump()
    {
        if (Input.GetKey(KeyCode.Space) && this.jumpCount < 1)
        {
            float pwa = jumpForce;

            jumpflag = true;
            anim.SetBool("jump", true);
            rb.velocity = new Vector2(rb.velocity.x, 0);

            //瞬間的な力を加える
            rb.AddForce(transform.up * pwa, ForceMode2D.Impulse);
            jumpCount++;
        }
       
    }

    RaycastHit2D[] CheckGroundStatus()
    {
        //Vector2 startPos = transform.position;
        Vector2 pos = transform.position;
        Vector2 startPosLeft = pos - new Vector2(transform.localScale.x / 4, 0); // プレイヤーテクスチャの左端
        Vector2 startPosRight = pos + new Vector2(transform.localScale.x / 4, 0); // プレイヤーテクスチャの右端
        Vector2 direction = Vector2.down; // 下方向にRayを発射

        // Rayを発射してヒット情報を取得
        RaycastHit2D hitLeft = Physics2D.Raycast(startPosLeft, direction, rayLength, groundLayers);
        RaycastHit2D hitRight = Physics2D.Raycast(startPosRight, direction, rayLength, groundLayers);

        return new RaycastHit2D[] { hitLeft, hitRight };
    }

    void SetChildObjectRotation(bool isLeft)
    {
        // プレイヤーの子オブジェクトを取得
        SpriteRenderer[] childSpriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        // 各子オブジェクトの向きを設定
        foreach (SpriteRenderer childRenderer in childSpriteRenderers)
        {
            childRenderer.flipX = isLeft;
        }

        // プレイヤーの子オブジェクト座標を取得
        Transform[] childTransforms = GetComponentsInChildren<Transform>();

        // 各子オブジェクトの位置を設定
        foreach (Transform childTransform in childTransforms)
        {
            // プレイヤー自身のTransform以外を操作
            if (childTransform != transform) 
            {
                Vector3 newPosition = childTransform.localPosition;

                newPosition.x = isLeft ? Mathf.Abs(newPosition.x) : -Mathf.Abs(newPosition.x);

                childTransform.localPosition = newPosition;
            }

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // グリズモの色を設定

        //Vector2 startPos = transform.position;
        Vector2 pos = transform.position;
        Vector2 startPosLeft = pos - new Vector2(transform.localScale.x/4, 0); // プレイヤーテクスチャの左端
        Vector2 startPosRight = pos + new Vector2(transform.localScale.x/4, 0); // プレイヤーテクスチャの右端
        //Vector2 direction = Vector2.down; // 下方向にRayを発射

        //Vector2 startPos = transform.position;
        Vector2 direction = Vector2.down * rayLength; // 下方向にRayを表示するためにrayLengthを掛けます

        Gizmos.DrawRay(startPosLeft, direction);
        Gizmos.DrawRay (startPosRight, direction);
    }
}
