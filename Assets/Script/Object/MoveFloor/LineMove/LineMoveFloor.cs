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
    /// 前フレームの位置を入れる変数
    /// </summary>
    private Vector3 oldpos = Vector2.zero;

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

    //移動速度
    Vector3 vevold;
    //移動位置
    Vector3 movePos;

    /// <summary>
    /// 一方通行用
    /// 移動終了フラグ
    /// <para>true:終了</para>
    /// </summary>
    private bool EndMoveflg = false;

    public bool EndMoveFLG { get { return EndMoveflg; } }

    //プレイヤーを追跡させるため
    private Player_Move player;

    //レイの衝突情報
    private RaycastHit2D[] raycastHit2D = new RaycastHit2D[2];


    private void Reset()
    {
        this.tag = "MoveFloor";
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
        rb.position=Transform_Targets[0].position;
        //初期設定
        oldpos = rb.position;
        currentIndex = 0;
        EndMoveflg = false;
        vevold = Vector3.zero;

        NextTargetPosition();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name == "Player")
        {
            //プレイヤーのレイを取得
            player = collision.gameObject.GetComponent<Player_Move>();
            raycastHit2D = player?.CheckGroundStatus();
        }


    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.name == "Player")
        {
            player = null;
            raycastHit2D = null;
        }

    }

    private void Update()
    {
        if (Transform_Targets.Length > 1)
        {
            //移動位置計測
            Vector3 newPosition = Vector2.MoveTowards(rb.position, targetPosition, speed * Time.deltaTime);

            //移動
            transform.position = newPosition;

            //速度
            vevold = (newPosition - oldpos) / Time.deltaTime;

            //位置
            movePos = (newPosition - oldpos);

            //次のフレームで使う用現在地の位置
            oldpos = transform.position;

            //プレイヤーの移動を実行
            for (int i = 0; i < 2; i++)
            {
                if (raycastHit2D != null)
                {
                    //レイ接触時のみ
                    if (raycastHit2D[i].collider)
                    {
                        //プレイヤー移動
                        player?.MoveFloorExec(this);
                        break;
                    }

                }
            }

            //目標位置に近づいたら次の頂点を得る
            Vector3 len = transform.position - targetPosition;
            if (len.sqrMagnitude < 0.1 * 0.1)
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
                if (currentIndex == Transform_Targets.Length - 1 ||
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

    //Player側で進んだ速度を得るための関数
    public Vector3 GetVec()
    {
        return vevold;
    }
    public Vector2 GetPos()
    {
        return movePos;
    }
}
