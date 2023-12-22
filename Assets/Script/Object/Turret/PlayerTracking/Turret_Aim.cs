using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Aim : MonoBehaviour
{
    [SerializeField, Header("タレットに付随している銃口のトランスフォーム")]
    private Transform arrowTrans; // 動かすオブジェクトのトランスフォーム

    [SerializeField, Header("目標になるオブジェクトのトランスフォーム")]
    private Transform ballTrans; // ターゲットのオブジェクトのトランスフォーム

    [SerializeField, Header("軸の最大回転角度")]
    private float maxRotation;

    [SerializeField, Header("軸の最低回転角度")]
    private float minRotation;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        //カメラ座標取得
        Vector3 viewPos = mainCamera.WorldToViewportPoint(transform.position);

        //todo
        // 向きたい方向を計算
        Vector3 dir = (ballTrans.position - arrowTrans.position);

        //角度に変換
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

        //制限
        angle = Mathf.Clamp(angle, minRotation, maxRotation);

        // 回転を適用
        transform.rotation = Quaternion.AngleAxis(angle,Vector3.forward);
    }
}
