using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class LaserAttack : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;

    [SerializeField]
    private Rigidbody2D Rigidbody2D;

    [SerializeField]
    private float Seed;
    [SerializeField]
    private float MaxSeed;

    private float startValue = 2.0f;//開始
    private float endValue = 0.5f;//終了
    private float lerpDuration = 1.5f; // 補完にかかる時間

    private float currentTime = 0.0f;//経過時間

    //開始地点保存
    private Vector3 StartPos;

    enum Mode{
        Wait,           //待機状態
        Move,           //移動中
        Delete          //削除
    }
    Mode mode = Mode.Wait;

    private void Start()
    {
        //攻撃開始

        Vector3[] positions = new Vector3[]{
            this.transform.position,               // 開始点（自分）
            this.transform.position + transform.up * 50,             // 終了点
        };

        //ラインの太さ設定
        lineRenderer.startWidth = startValue;
        lineRenderer.endWidth = startValue;
        //ライン描画
        lineRenderer.SetPositions(positions);

        //待機状態
        mode = Mode.Wait;

        //開始地点保存
        StartPos = transform.position;
    }

    private void Update()
    {
        switch (mode) { 
        
            case Mode.Wait:
                {
                    //レーザーを細く
                    LaserStart();
                }
                break;

            case Mode.Move:
                {
                    //オブジェクトの前方向に
                    Vector2 force = transform.up * Seed;

                    //加速
                    Rigidbody2D.AddForce(force);

                    //最大速度以上にならないように
                    if (Rigidbody2D.velocity.magnitude > MaxSeed)
                    {
                        Rigidbody2D.velocity = Rigidbody2D.velocity.normalized * MaxSeed;
                    }
                }
                break;

            case Mode.Delete:
                {
                    //レーザーをなくす
                    LaserEnd();
                }
                break;
        
        }
    }

    private void LaserEnd()
    {
        //経過時間を更新
        currentTime += Time.deltaTime;

        //値を補完
        float lerpValue = Mathf.Lerp(endValue, 0, currentTime / lerpDuration);

        //ラインの太さを変更
        lineRenderer.startWidth = lerpValue;
        lineRenderer.endWidth = lerpValue;

        //補完が完了したら値を固定
        if (currentTime >= lerpDuration)
        {
            //初期化して初期点に戻す
            currentTime = 0;
            transform.position = StartPos;
            Start();  
            this.gameObject.SetActive(false);
        }
    }

    private void LaserStart()
    {
        //経過時間を更新
        currentTime += Time.deltaTime;

        //値を補完
        float lerpValue = Mathf.Lerp(startValue, endValue, currentTime / lerpDuration);

        //補完が完了したら値を固定
        if (currentTime >= lerpDuration)
        {
            lerpValue = endValue;

            //移動中にモード変更
            mode= Mode.Move;

            //削除コルーチン開始
            StartCoroutine(My_Delete());

            //リセット
            currentTime = 0;
        }

        //ラインの太さを変更
        lineRenderer.startWidth = lerpValue;
        lineRenderer.endWidth = lerpValue;

    }

    IEnumerator My_Delete()
    {
        //数秒後
        yield return new WaitForSeconds(3);

        //削除モードに変更
        mode = Mode.Delete;

    }

}
