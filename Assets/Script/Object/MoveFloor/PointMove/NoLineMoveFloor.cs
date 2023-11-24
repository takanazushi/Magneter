using UnityEngine;
using System.Collections;
using System.Collections.Generic;

 public class NoLineMoveFloor : MonoBehaviour
{
    //GameObject PointMoveFloorで使用
    //GameObject PointMoveFloorRigitBodyのボディタイプをキネマティックに変更してます
    //キネマティックに変更することで重力の影響を無くす
    //GameObject PointMoveFloorにPlatformEffector2Dを入れてます
    //PlatformEffector2Dで当たり判定を上だけに限定してます

    //GameObject MovePoint1,MovePoint2を入れる変数
    [SerializeField]
    [Header("移動経路")] private GameObject[] movePoint;
    //速さ
    [SerializeField]
    [Header("速さ")] private float speed;

    private Rigidbody2D rb;
    //Pointの数を管理
    private int nowPoint = 0;
    //折り返しを管理
    private bool returnPoint = false;

    //前の値を入れる変数
    private Vector2 oldpos = Vector2.zero;
    //進んだ距離を入れる変数
    private Vector2 newpos = Vector2.zero;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //最初のpointを入れる
        if (movePoint != null && movePoint.Length > 0 && rb != null)
        {
            rb.position = movePoint[0].transform.position;
        }
        //初期位置
        oldpos = rb.position;
    }

    public Vector2 GetVelocity()
    {
        return newpos;
    }

    private void FixedUpdate()
    {
        if (movePoint != null && movePoint.Length > 1 && rb != null)
        {
            //通常進行
            if (!returnPoint)
            {
                int nextPoint = nowPoint + 1;

                //目標ポイントとの誤差がわずかになるまで移動
                if (Vector2.Distance(transform.position, movePoint[nextPoint].transform.position) > 0.1f)
                {
                    //現在地から次のポイントへのベクトルを作成
                    Vector2 toVector = Vector2.MoveTowards(transform.position, movePoint[nextPoint].transform.position, speed * Time.deltaTime);

                    //次のポイントへ移動
                    rb.MovePosition(toVector);
                }
                //次のポイントを１つ進める
                else
                {
                    //次のポイントの座標を読む
                    rb.MovePosition(movePoint[nextPoint].transform.position);
                    ++nowPoint;

                    //現在地が配列の最後だった場合
                    if (nowPoint + 1 >= movePoint.Length)
                    {
                        returnPoint = true;
                    }
                }
            }
            //折返し進行
            else
            {
                int nextPoint = nowPoint - 1;

                //目標ポイントとの誤差がわずかになるまで移動
                if (Vector2.Distance(transform.position, movePoint[nextPoint].transform.position) > 0.1f)
                {
                    //現在地から次のポイントへのベクトルを作成
                    Vector2 toVector = Vector2.MoveTowards(transform.position, movePoint[nextPoint].transform.position, speed * Time.deltaTime);

                    //次のポイントへ移動
                    rb.MovePosition(toVector);
                }
                //次のポイントを１つ戻す
                else
                {
                    //ポイントを1つ戻す
                    rb.MovePosition(movePoint[nextPoint].transform.position);
                    --nowPoint;

                    //現在地が配列の最初だった場合
                    if (nowPoint <= 0)
                    {
                        returnPoint = false;
                    }
                }
            }
        }
        //進んだ距離
        newpos = (rb.position - oldpos) / Time.deltaTime;
        //前の位置
        oldpos = rb.position;
    }
}