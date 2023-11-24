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

    [SerializeField, Header("レイの長さ")]
    float rayLength = 1.0f;

    [SerializeField, Header("壁当たったか判定")]
    public Player_kabehan player_Kabehan;

    private float speed;

    private int jumpCount = 0;

    bool jumpflag = false;
    bool kabeflag = false;

    private Rigidbody2D rb;

    private RaycastHit2D[] raycastHit2D = new RaycastHit2D[2];

    private SpriteRenderer spriteRenderer;

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
        jumpflag = false;
    }

    // 物理演算をしたい場合はFixedUpdateを使うのが一般的
    void FixedUpdate()
    {
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

        raycastHit2D = CheckGroundStatus();

        PlayerJump();

        for(int i = 0; i < 2; i++)
        {
            if (kabeflag && raycastHit2D[i].collider || kabeflag == false && raycastHit2D[i].collider|| player_Kabehan.isOn == false)
            {
                PlayerWalk();
            }
            else if (kabeflag && raycastHit2D[i].collider == null)
            {
                Debug.Log("ずりおち");
            }
        }

        

        

        //Debug.Log(raycastHit2D.collider);

    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "kabe")
        {
            kabeflag = true;
        }

        jumpCount = 0;
        jumpflag = false;

        Debug.Log("ジャンプフラグは：" + jumpflag);

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "kabe")
        {
            kabeflag = false;
        }
    }

    private void PlayerWalk()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float xSpeed = 0;

        if (jumpflag)
        {
            speed = jumpMoveX;
        }
        else
        {
            speed = walkMoveX;
        }

        if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            xSpeed = speed;
        }
        else if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            xSpeed = -speed;
        }
        else
        {
            xSpeed = 0;
        }

        //speed = horizontalInput * speed;


        rb.velocity = new Vector3(xSpeed, rb.velocity.y, 0);
    }

    private void PlayerJump()
    {
        if (Input.GetKey(KeyCode.Space) && this.jumpCount < 1)
        {
            jumpflag = true;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(transform.up * jumpForce);
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

        //Debug.DrawRay(startPos, direction * rayLength, Color.red); // Rayをシーンビューに表示

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
            if (childTransform != transform) // プレイヤー自身のTransform以外を操作
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
