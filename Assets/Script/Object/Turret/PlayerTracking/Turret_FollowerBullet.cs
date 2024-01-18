using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_FollowerBullet : MonoBehaviour
{
    //プレイヤーの座標格納
    private Transform target;

    [SerializeField, Header("弾の最大回転角度")]
    private float maxRotation;

    [SerializeField, Header("弾の最低回転角度")]
    private float minRotation;

    //移動速度
    [SerializeField, Header("弾の発射スピード")]
    private float moveSpeed;

    [SerializeField]
    private AudioClip ShotSE;

    //画面内
    private bool InField = false;

    private Rigidbody2D rb;

    private Vector3 bulletDirection;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("Battery_FollowerBulletにAudioSourceついてない");
        }

        target = GameObject.FindGameObjectWithTag("Player").transform;

        Vector3 dir = (target.position - transform.position);

        //角度に変換
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

        //制限
        angle = Mathf.Clamp(angle, minRotation, maxRotation);

        // 回転を適用
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // 弾の方向を自機から敵へ向ける
        bulletDirection = target.position - transform.position;

        //todo 弾の発射角度の制限
        if(bulletDirection.y > -5)
        {
            bulletDirection.y = -5;
        }

    }

    //画面内で動かす
    private void OnBecameVisible()
    {
        //画面内にいるときtrue
        audioSource.PlayOneShot(ShotSE);

        InField = true;
        Debug.Log("カメラ範囲内");
    }

    //画面外で消す処理
    private void OnBecameInvisible()
    {
        //消す
        Debug.Log("カメラ範囲外");
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.Is_Ster_camera_end)
        {
            Destroy(rb.gameObject);
            return;
        }
        //画面内で実行
        else if (InField)
        {
            //向き固定
            bulletDirection.Normalize();

            //その向きにスピードを掛ける
            rb.velocity = bulletDirection * moveSpeed;

            //10秒後に砲弾を破壊する
            Destroy(gameObject, 5.0f);
        }   
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Destroy (gameObject);
    }
}
