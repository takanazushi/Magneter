using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static MoveFloorMNG;

public class LineMoveFloor : MonoBehaviour
{
    //GameObject LineMoveFloorで使用
    //GameObject LineMoveFloorRigitBodyのボディタイプをキネマティックに変更してます
    //キネマティックに変更することで重力の影響を無くす
    //GameObject LineMoveFloorにPlatformEffector2Dを入れてます
    //PlatformEffector2Dで当たり判定を上だけに限定してます

    //動く床のスピード
    private float speed;
    public float Setspeed
    {
        set { speed = value; }
    }

    /// <summary>
    /// 現在のポイント番号
    /// </summary>
    private int currentIndex;

    /// <summary>
    /// 自分のRigidbody2D
    /// </summary>
    private Rigidbody2D rb;

    /// <summary>
    /// 進んだ距離を入れる変数
    /// </summary>
    private Vector2 oldpos = Vector2.zero;

    /// <summary>
    /// ポイントのTransform
    /// </summary>
    Transform[] Transform_Targets;
    public Transform[] SetTransform_Targets
    {
        set { Transform_Targets = value; }
    }

    /// <summary>
    /// 現在のポイント位置
    /// </summary>
    Vector3 targetPosition;

    /// <summary>
    /// 移動パターン
    /// </summary>
    MoveType Movetype;
    public MoveType SetMovetype
    {
        set { Movetype = value; }
    }

    /// <summary>
    /// 往復用
    /// ポイント移動量
    /// </summary>
    int PointMove = 1;

    /// <summary>
    /// 一方通行用
    /// 移動終了フラグ
    /// <para>true:終了</para>
    /// </summary>
    private bool EndMoveflg = false;

    public bool EndMoveFLG { get { return EndMoveflg; } }

    private void Reset()
    {
        this.tag= "MoveFloor";
        speed = 3;
    }

    private void Awake()
    {
        //自身のRigidbody2D
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        //開始地点に移動
        transform.position = Transform_Targets[0].position;
        //初期設定
        oldpos = rb.position;
        currentIndex = 0;
        EndMoveflg = false;
        targetPosition = Transform_Targets[currentIndex].position;
        
    }

    private void FixedUpdate()
    {
        if (Transform_Targets.Length > 0)
        {
            //移動量計測
            Vector2 newPosition = Vector2.MoveTowards(rb.position, targetPosition, speed * Time.fixedDeltaTime);

            //前の位置
            oldpos = transform.position - new Vector3(newPosition.x, newPosition.y, 0);

            //移動
            rb.MovePosition(newPosition);


            //目標位置に近づいたら次の頂点を得る
            Vector3 len= transform.position - targetPosition;
            if (len.sqrMagnitude < 0.1*0.1)
            {
                NextTargetPosition();
            }
        }

    }

    /// <summary>
    /// 次のポジションを設定
    /// </summary>
    private void NextTargetPosition()
    {

        switch (Movetype) 
        {
            case MoveType.Patrol:
                currentIndex = (currentIndex + 1) % Transform_Targets.Length;
                break;

            case MoveType.Round_Trip:

                currentIndex += PointMove;
                if (currentIndex == Transform_Targets.Length-1||
                    currentIndex <= 0)
                {
                    PointMove = -PointMove;
                }
                break;

            case MoveType.One_Way:
                currentIndex++;
                if (currentIndex + 1 > Transform_Targets.Length)
                {
                    currentIndex--;
                    EndMoveflg = true;
                }
                break;

        }


        //ポジション(頂点の座標)設定
        targetPosition = Transform_Targets[currentIndex].position;

    }

    //Player側で進んだ距離を得るための関数
    public Vector2 GetVelocity()
    {
        return oldpos;
    }


}
