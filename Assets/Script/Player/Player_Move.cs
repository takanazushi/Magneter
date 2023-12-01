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

    [SerializeField, Header("風の減速値")]
    private float windMoveSpeed = 1.0f;

    private float speed;

    private int jumpCount = 0;

    bool jumpflag = false;
    bool kabeflag = false;

    private Rigidbody2D rb;

    private RaycastHit2D raycastHit2D;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
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

        if (kabeflag && raycastHit2D.collider || kabeflag == false && raycastHit2D.collider)
        {
            PlayerWalk();
        }
        else if (kabeflag && raycastHit2D.collider == null)
        {
            Debug.Log("ずりおち");
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

        if (jumpflag)
        {
            speed = jumpMoveX;
        }
        else
        {
            speed = walkMoveX;
        }

        speed = horizontalInput * speed;

        //風に当たっている状態の速度取得
        windMoveSpeed = Wind.instance.getMoveSpeed;
        

        rb.velocity = new Vector3(speed / windMoveSpeed, rb.velocity.y, 0);

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

    RaycastHit2D CheckGroundStatus()
    {
        Vector2 startPos = transform.position;
        Vector2 direction = Vector2.down; // 下方向にRayを発射

        // Rayを発射してヒット情報を取得
        RaycastHit2D hit = Physics2D.Raycast(startPos, direction, rayLength, groundLayers);

        //Debug.DrawRay(startPos, direction * rayLength, Color.red); // Rayをシーンビューに表示

        return hit;
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

        Vector2 startPos = transform.position;
        Vector2 direction = Vector2.down * rayLength; // 下方向にRayを表示するためにrayLengthを掛けます

        Gizmos.DrawRay(startPos, direction);
    }
}
