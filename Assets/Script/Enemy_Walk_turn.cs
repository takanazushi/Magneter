using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Walk_turn : MonoBehaviour
{
    private Rigidbody2D rb;

    //スピード値
    private float speed;

    //最初に左右どちらを進むか選べる変数　　✔で右　✔無し左
    [SerializeField]
    private bool turn;

    //Capsuleのインスペクターのスクリプト、Squareのレイヤーをstageにしてます
    public LayerMask StageLayer;

    //敵の移動状態をenumで管理
    private enum MOVE_TYPE
    {
        RIGHT,
        LEFT,
    }
    //最初は何も無し
    MOVE_TYPE move = 0; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //✔で右
        if(turn)
        {
            move = MOVE_TYPE.RIGHT;
        }
        //✔無しで左
        else if(!turn)
        {
            move = MOVE_TYPE.LEFT;
        }
    }

    //Capsuleと、Squareの始点と終点の線が触れているかのチェック
    //触れなくなったら反転させる
    bool GroundChk()
    {
        //コピペしただけなので説明出来ません。

        // transform.localScaleでサイズを取る
        Vector3 scale = transform.localScale;
        // 始点が常に敵の進行方向         位置   x軸進行方向 右 1 左 -1       サイズ
        Vector3 startposition = transform.position + transform.right * 0.3f * scale.x;
        // startpostionから足元までを終点とする
        Vector3 endposition = startposition - transform.up * 1.5f;

        // Debug用に始点と終点を表示する
        Debug.DrawLine(startposition, endposition, Color.red);

        // Physics2D.Linecastを使い、ベクトルとStageLayerが接触していたらTrueを返す
        return Physics2D.Linecast(startposition, endposition, StageLayer);
    }

    private void Update()
    {
        //falseになったら反転させる
        if (!GroundChk())
        {
            ChgDIrection();
        }
    }

    //反転処理
    void ChgDIrection()
    {
        // 右に移動している時は
        if (move == MOVE_TYPE.RIGHT)
        {
            // 左へ方向転換する
            move = MOVE_TYPE.LEFT;
        }
        else
        {
            // 左へ移動中は右へ方向転換する
            move = MOVE_TYPE.RIGHT;
        }
    }

    private void FixedUpdate()
    {
        // Playerの方向を決めるためにスケールの取り出し
        Vector3 scale = transform.localScale;
        if (move == MOVE_TYPE.RIGHT)
        {
            scale.x = 1; // 右向き
            speed = 3;
        }
        else if (move == MOVE_TYPE.LEFT)
        {
            scale.x = -1; // 左向き
            speed = -3;
        }
        transform.localScale = scale; // scaleを代入
        // rigidbody2Dのvelocity(速度)へ取得したspeedを入れる。y方向は動かないのでそのままにする
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }
}
