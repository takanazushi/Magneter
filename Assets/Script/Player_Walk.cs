using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class Player_Walk : MonoBehaviour
{
    //スピード
    private float speed = 0;
    private Rigidbody2D rb;

    //Movefloorクラスから動く床の移動速度を入れる変数
    private Vector2 linemovingfloor;
    //LineMoveFloorクラスを取得
    private LineMoveFloor linemovefloor;

    //NoLineMoveFloorクラスから動く床の移動速度を入れる変数
    private Vector2 nolinemovingfloor;
    //NoLineMoveFloorクラスを取得
    private NoLineMoveFloor nolinemovefloor;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        linemovefloor = null;
        nolinemovefloor = null;
    }

    private void Update()
    {
        //右入力で右向きに動く
        if (Input.GetKey(KeyCode.D))
        {
            speed = 5.0f;
        }
        //左入力で左向きに動く
        else if (Input.GetKey(KeyCode.A))
        {
            speed = -5.0f;
        }
        //ボタンを話すと止まる
        else
        {
            speed = 0.0f;
        }

        //movefloorに動く床の情報が入った時
        if (linemovefloor != null)
        {
            LineMoveFloor();
        }
        //nolinemovefloorに動く床の情報が入った時
        else if (nolinemovefloor != null)
        {
            PointMoveFloor();
        }
        //動く床に乗ってないとき
        else 
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
    }
    
    //関数LineMoveFloorとPointMoveFloorはどっちを採用するか分からないので関数で分けてます。
    //処理的には一緒なので不採用の方は消して大丈夫です。
    //変数movefloorかnolinemovefloorを使っているかの違いだけ。
    private void LineMoveFloor()
    {
        //値リセット
        linemovingfloor = Vector2.zero;
        //MoveFloorクラスからGetVelocity()を呼び出し動く床の移動速度を取得
        linemovingfloor = linemovefloor.GetVelocity();

        //動く床の進行方向と逆にPlayerが進んだ時のPlayerの移動が遅くなる問題の速度調整。
        //スピードの値は体感で良さそうなのを入れてます。
        //変えたかったら自由に変えてください。

        //動く床が左に進んでるときPlayerが右に進んだ時
        if (Input.GetKey(KeyCode.D) && linemovingfloor.x < 0)
        { 
            speed = 6.5f;
        }
        //動く床が右に進んでるときPlayerが左に進んだ時
        else if (Input.GetKey(KeyCode.A) && linemovingfloor.x > 0)
        {
            speed = -6.5f;
        }

        rb.velocity = new Vector2(speed + linemovingfloor.x, rb.velocity.y);
    }

    private void PointMoveFloor()
    {
        //値リセット
        nolinemovingfloor = Vector2.zero;
       //nolinemovefloorクラスからGetVelocity()を呼び出し動く床の移動速度取得
       nolinemovingfloor = nolinemovefloor.GetVelocity();

       //動く床が左に進んでるときPlayerが右に進んだ時
       if (Input.GetKey(KeyCode.D) && nolinemovingfloor.x < 0)
       {
           speed = 6.5f;
       }
       //動く床が右に進んでるときPlayerが左に進んだ時
       else if (Input.GetKey(KeyCode.A) && nolinemovingfloor.x > 0)
       {
           speed = -6.5f;
       }
        //移動
        rb.velocity = new Vector2(speed + nolinemovingfloor.x, rb.velocity.y);
    }

    //オブジェクト同士が接触している時
    private void OnTriggerStay2D(Collider2D other)
    {
        //そのオブジェクトの名前がMoveFloorの場合
        if (other.gameObject.name == "LineMoveFloor")
        {
            linemovefloor = other.gameObject.GetComponent<LineMoveFloor>();
        }
        else if (other.gameObject.name == "PointMoveFloor")
        {
            nolinemovefloor = other.gameObject.GetComponent<NoLineMoveFloor>();
        }
    }

    //オブジェクト同士が離れた時
    private void OnTriggerExit2D(Collider2D other)
    {
        //オブジェクトの名前がMoveFloorの場合
        if (other.gameObject.name == "LineMoveFloor")
        {
            //解除
            linemovefloor = null;
        }
        else if (other.gameObject.name == "PointMoveFloor")
        {
            //解除
            nolinemovefloor = null;
        }
    }
}
