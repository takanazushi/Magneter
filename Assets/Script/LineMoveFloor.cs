using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMoveFloor : MonoBehaviour
{
    //GameObject LineMoveFloorで使用
    //GameObject LineMoveFloorRigitBodyのボディタイプをキネマティックに変更してます
    //キネマティックに変更することで重力の影響を無くす
    //GameObject LineMoveFloorにPlatformEffector2Dを入れてます
    //PlatformEffector2Dで当たり判定を上だけに限定してます

    //クラスLineRendererの情報を入れる変数
    [SerializeField]
    private LineRenderer lineRenderer;
    //動く床のスピード
    [SerializeField]
    private float speed;
    //頂点の数
    private int currentIndex;
    private Rigidbody2D rb;

    //前の値を入れる変数
    private Vector2 oldpos = Vector2.zero;
    //進んだ距離を入れる変数
    private Vector2 newpos = Vector2.zero;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //初期位置
        oldpos = rb.position;
        currentIndex = 0;
    }

    //Player側で進んだ距離を得るための関数
    public Vector2 GetVelocity()
    {
        return newpos;
    }

    void Update()
    {
        if (lineRenderer.positionCount > 0)
        {
            //ポジション(頂点の座標)設定
            Vector3 targetPosition = lineRenderer.GetPosition(currentIndex);
            
            //現在の位置から目標の位置までの次のフレームの位置を計算
            Vector2 moveTowards = Vector2.MoveTowards(rb.position, targetPosition,speed * Time.deltaTime);

            // 補間を使用してスムーズな速度変化を実現
            Vector2 smoothVelocity = Vector2.Lerp(rb.velocity, (moveTowards - rb.position) / Time.deltaTime, 0.1f);

            //速度代入
            rb.velocity = smoothVelocity;
            //進んだ距離
            newpos = smoothVelocity;
            //前の位置
            oldpos = rb.position;
            //目標位置に近づいたら次の頂点を得る
            if (Vector3.Distance(rb.position, targetPosition) < 0.1f)
            {
                currentIndex = (currentIndex + 1) % lineRenderer.positionCount;
            }
        }
    }
}
