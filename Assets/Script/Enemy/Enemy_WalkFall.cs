using Unity.VisualScripting;
using UnityEngine;
using static Magnet;

///<summary>
/// 接触した時に反転する敵
/// </summary>
public class Enemy_WalkFall : MonoBehaviour
{
    /// <summary>
    /// 移動速度
    /// </summary>
    [SerializeField, Header("移動速度")]
    private float Speed;

    /// <summary>
    /// 磁力抵抗限界値
    /// </summary>
    [SerializeField, Header("磁力抵抗限界値")
        , Tooltip("この値以上の力を受けると反転します")]
    float X_Resist;

    /// <summary>
    /// 左向きフラグ
    /// </summary>
    [SerializeField]
    private bool Left;

    /// <summary>
    /// 敵用マグネット
    /// </summary>
    [SerializeField, HideInInspector]
    private Magnet magnet;

    //自身のRigidbody2D
    [SerializeField, HideInInspector]
    private Rigidbody2D rb;

    private void Reset()
    {
        rb = GetComponent<Rigidbody2D>();
        magnet = GetComponent<Magnet>();
        Speed = 1;
        X_Resist = 0.3f;
    }

    private void Start()
    {
        if (!Left)
        {
            transform.localScale = new
                (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
    }

    // 物理演算をしたい場合のFixedUpdate
    void FixedUpdate()
    {
        Vector2 mg_speed = Vector2.zero;

        //磁力の影響ある場合
        if (magnet)
        {
            if(magnet.PuroTypeManet!= Type_Magnet.None&&
                magnet.PuroTypeManet != Type_Magnet.Exc)
            {
                mg_speed = magnet.Magnet_Power(new string[] { tag });

                //横方向に一定以上の力を受けた場合
                //todo:抵抗値を設定して平面上ならダイジョブですが、
                //自分が磁気を付与などで、抵抗値を越える場所に長く滞在する場合ブルブルしちゃう
                if (Mathf.Abs(mg_speed.x) >= Mathf.Abs(X_Resist))
                {
                    //左右反転
                    Turn();
                }
            }
        }

        float X_speed;
        //左移動
        if (Left)
        {
            X_speed = -Speed;

        }
        //右移動
        else
        {
            X_speed = Speed;

        }

        //敵の移動値
        Vector2 total = new(X_speed, rb.velocity.y);

        //磁力の力を合算
        total += mg_speed;
        rb.velocity = total;

        //落下後ｙが-10時点で削除
        if (transform.position.y < -10)
        {
            //todo:要修正
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Enemyタグを持つオブジェクトとの当たり判定を無視
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }

        //床との処理は無視
        if (collision.gameObject.name == "Floor")
        {
            return;
        }


        Turn();
        

    }

    /// <summary>
    /// 左右反転
    /// </summary>
    private void Turn()
    {
        Left = !Left;
        transform.localScale = new
            (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}
