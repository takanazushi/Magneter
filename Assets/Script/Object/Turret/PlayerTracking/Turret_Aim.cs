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

    private void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, 90);
    }

    private void Update()
    {
        //todo
        // 向きたい方向を計算
        Vector3 dir = (ballTrans.position - arrowTrans.position);

        // 回転を制限する
        float angle = Vector3.Angle(Vector3.right, dir);
        float sign = Mathf.Sign(Vector3.Cross(Vector3.right, dir).y);
        angle = Mathf.Clamp(angle * sign, minRotation, maxRotation);

        // 回転を適用
        transform.rotation = Quaternion.Euler(0, 0, -angle);
    }
}
