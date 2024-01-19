using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_TopCreate : MonoBehaviour
{
    //todo : 全て

    private Rigidbody2D rb;

    //カメラ
    private Camera mainCamera;

    //カメラ座標格納
    Vector3 viewPos;

    //敵の数
    [SerializeField]
    private int enemy_coumt;

    //敵の出現してる数
    public int enemy_outcount;

    //スポーン位置
    private Vector3 spawnpositison;

    //出現させる敵を入れておく
    [SerializeField]
    private GameObject enemy;

    public static Enemy_TopCreate instance;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        //最初の出現位置格納
        spawnpositison = transform.position;
        rb = GetComponent<Rigidbody2D>();
        
        //敵の出現数
        enemy_outcount = 1;
        
        //繰り返す 5秒に1回
        InvokeRepeating("EnemyCreate", 0f, 5f);
    }

    void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        //カメラ座標取得
        viewPos = mainCamera.WorldToViewportPoint(transform.position);
    }

    //敵生成処理
    private void EnemyCreate()
    {
        //カメラ内の時生成
        if (viewPos.x > 0 && viewPos.x < 1 && enemy_coumt > 0 && enemy_outcount > 0)
        {
            //生成処理
            Instantiate(enemy, spawnpositison, Quaternion.identity);
            //敵何体出すか
            enemy_coumt--;
            //出現数を0で出現させなくする→電気床で消滅後カウントを1にする
            enemy_outcount = 0;
        }
    }
}
