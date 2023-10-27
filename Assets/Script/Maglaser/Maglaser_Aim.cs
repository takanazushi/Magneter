using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maglaser_Aim : MonoBehaviour
{
    public LineRenderer aimLine;

    private void Update()
    {
        //マウスの位置を取得
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //プレイヤーの位置からマウスの位置に向かうベクトル計算
        Vector3 aimDirection = (mousePosition - this.transform.position).normalized;

        //ベクトルから角度を取得
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        //角度分+90度回転
        this.transform.rotation = Quaternion.Euler(0, 0, angle + 90f);

        //照準戦の位置を設定
        //開始地点
        Vector3 linestatr = transform.position;
        linestatr.z = -1;
        aimLine.SetPosition(0, linestatr);

        //終了地点
        Vector3 endPosition = gameObject.transform.position + Quaternion.Euler(0, 0, angle) * Vector3.right * 2f;
        endPosition.z = -1;
        aimLine.SetPosition(1, endPosition);
        //Debug.Log(endPosition);
    }
}
