using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_FollowerBullet : MonoBehaviour
{
    //プレイヤーの座標格納
    private Transform target;

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
        if (!GameManager.instance.Is_Ster_camera_end) { return; }
        //画面内にいるときtrue
        audioSource.PlayOneShot(ShotSE);
        InField = true;
    }

    //画面外で消す処理
    private void OnBecameInvisible()
    {
        //消す
        Destroy(rb.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

        //画面内で実行
        if (InField&&GameManager.instance.Is_Ster_camera_end)
        {
            //向き固定
            bulletDirection.Normalize();

            //その向きにスピードを掛ける
            rb.velocity = bulletDirection * moveSpeed;

            //10秒後に砲弾を破壊する
            Destroy(gameObject, 5.0f);
        }   
    }
}
