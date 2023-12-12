using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maglaser_Aim : MonoBehaviour
{
    [SerializeField, Header("照準の線")]
    public LineRenderer aimLine;

    //角度
    private float angle;

    private void Update()
    {
        UpdateAimDirection();
        UpdateAimLine();
    }

    /// <summary>
    /// 照準の方向更新
    /// </summary>
    private void UpdateAimDirection()
    {
        //マウスの位置を取得
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //プレイヤーの位置からマウスの位置に向かうベクトル計算
        Vector3 aimDirection = (mousePosition - this.transform.position).normalized;

        //ベクトルから角度を取得
        angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        //角度分+90度回転
        this.transform.rotation = Quaternion.Euler(0, 0, angle + 90f);
    }

    /// <summary>
    /// 照準線の更新
    /// </summary>
    private void UpdateAimLine()
    {
        //照準戦の位置を設定
        //開始地点
        Vector3 linestatr = transform.position;
        linestatr.z = -1;
        aimLine.SetPosition(0, linestatr);

        //終了地点
        Vector3 endPosition = gameObject.transform.position + Quaternion.Euler(0, 0, angle) * Vector3.right * 2f;
        endPosition.z = -1;
        aimLine.SetPosition(1, endPosition);
    }
}
