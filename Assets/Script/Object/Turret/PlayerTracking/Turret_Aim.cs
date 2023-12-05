using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Aim : MonoBehaviour
{
    [SerializeField, Header("タレットに付随している銃口のトランスフォーム")]
    private Transform arrowTrans; // 動かすオブジェクトのトランスフォーム

    [SerializeField, Header("目標になるオブジェクトのトランスフォーム")]
    private Transform ballTrans; // ターゲットのオブジェクトのトランスフォーム

    private void Update()
    {
        // 向きたい方向を計算
        Vector3 dir = (ballTrans.position - arrowTrans.position);

        // ここで向きたい方向に回転させてます
        arrowTrans.rotation = Quaternion.FromToRotation(new Vector3(1,0,0), dir);
    }
}
