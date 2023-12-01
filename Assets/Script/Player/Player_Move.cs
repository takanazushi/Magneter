using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    //変えたところ　todo振ってます

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

    private float speed;
    //todo 追加1 ベルトコンベアに乗った時の変数
    private float converspeed;
    //todo 追加2 ベルトコンベアに流れてるブロックに乗った時の変数
    private float blockspeed;

    private float horizontalInput;

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

    void Start()
    {
        //if (GameManager.instance.checkpointNo > -1)
        //{
        //    // ワープ先のチェックポイントオブジェクトを見つける("checkpoint (1)" のような名前になっているもの）
        //    GameObject checkpointObject = GameObject.Find("checkpoint (" + GameManager.instance.checkpointNo + ")");

        //    // チェックポイントオブジェクトが見つかった場合は、プレイヤーをワープさせる
        //    if (checkpointObject != null)
        //    {
        //        transform.position = checkpointObject.transform.position;
        //    }
        //    else
        //    {
        //        Debug.Log(GameManager.instance.checkpointNo + "チェックポイントを通過していない");
        //    }
        //}

        rb = GetComponent<Rigidbody2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("spriteレンダラーない");
        }

        jumpflag = false;
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
            spriteRenderer.flipX = false;
            SetChildObjectRotation(false);
        }
        else if (mousePosition.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
            SetChildObjectRotation(true);
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

        //移動処理
        PlayerWalk();

        //todo ベルトコンベアの速度を足して通常速度より速くなった時
        if(rb.velocity.x > 5.0 || rb.velocity.x < -5.0)
        {
            jumpMoveX += 0.1f;
            //最大値
            if(jumpMoveX >= 7.0)
            {
                jumpMoveX = 7.0f;
            }
        }

        PlayerJump();

        if (kabeflag && raycastHit2D.collider || !kabeflag && raycastHit2D.collider)
        {
            PlayerWalk();
        }
        else if (kabeflag && raycastHit2D.collider == null)
        {
            Debug.Log("ずりおち");
        }
        Debug.Log(rb.velocity.x);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {


        //if (other.gameObject.tag == "MoveFloor")

        if (other.gameObject.tag == "kabe")
        {
            moveFloor = other.gameObject.GetComponent<LineMoveFloor>();
            //Debug.Log("動く床と当たってる");
        }

        //Debug.Log("ジャンプフラグは：" + jumpflag);

        //todo Jump値リセット
        jumpMoveX = 3.0f;
        jumpCount = 0;
        jumpflag = false;

        //Debug.Log("ジャンプフラグは：" + jumpflag);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //todo ベルトコンベアに流れてるブロックに乗った時
        if (collision.gameObject.name == "ConverBlock")
        {
            //ブロックのスピード取得
            blockspeed = CoverVeltThing.Instance.returnSpeed();
        }
        //todo 右に流れるベルトコンベアの時
        if (collision.gameObject.name == "PlusVeltConver")
        {
            converspeed = 3;
        }
        //todo 左に流れるベルトコンベアの時
        if (collision.gameObject.name == "MinusVeltConver")
        {
            converspeed = -3;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        //if (collision.gameObject.tag == "MoveFloor")
        
        converspeed = 0;
        
        if (collision.gameObject.tag == "kabe")
        {
            moveFloor = null;
            // Debug.Log("動く床と当たってない");
        }

        //todo ブロックから離れた時
        if (collision.gameObject.name == "ConverBlock")
        {
            blockspeed = 0;
        }
    }

    private void PlayerWalk()
    {
        //横移動を取得
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        horizontalInput = Input.GetAxis("Horizontal");

        //横移動スピード
        float speed;

        //ジャンプ中は横移動速度を切り替える
        if (jumpflag)
        {
            speed = jumpMoveX;
        }
        else
        {
            speed = walkMoveX;
        }

        //左右反転
        if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        
        //足場に乗っている場合
        Vector2 floorVelocity = Vector2.zero;
        if (moveFloor != null)
        {
            floorVelocity = moveFloor.GetVelocity();
        }

        //速度生成
        Vector2 velocity = new (horizontalInput, rb.velocity.y);

        //スピード乗算
        velocity.x = velocity.x * speed;

        //足場の移動速度を追加
        velocity.x += floorVelocity.x;
        velocity.y = rb.velocity.y;
        ////velocity.y += floorVelocity.y;

        //if (floorVelocity.y >= 0)
        //{
        //    //velocity.y -= floorVelocity.y;
        //}
        //else
        //{
        //    //velocity.y += floorVelocity.y;
        //}

        rb.velocity = velocity;

        Debug.Log(floorVelocity);

        //todo 追加7 コンベアとブロックのスピード加算
        rb.velocity = new Vector3(speed + converspeed + blockspeed, rb.velocity.y, 0);
    }

    private void PlayerJump()
    {
        if (Input.GetKey(KeyCode.Space) && this.jumpCount < 1)
        {
            float pwa = jumpForce;

            jumpflag = true;
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

        Debug.DrawRay(startPos, direction * rayLength, Color.red); // Rayをシーンビューに表示

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

        // プレイヤーの子オブジェクトを取得
        Transform[] childTransforms = GetComponentsInChildren<Transform>();

        // 各子オブジェクトの位置を設定
        foreach (Transform childTransform in childTransforms)
        {
            // プレイヤー自身のTransform以外を操作
            if (childTransform != transform) 
            {
                Vector3 newPosition = childTransform.localPosition;

                newPosition.x = isLeft ? -Mathf.Abs(newPosition.x) : Mathf.Abs(newPosition.x);

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
