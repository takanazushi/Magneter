using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maglaser_Aim : MonoBehaviour
{
    //照準用のLineRenderer
    public LineRenderer aimLine;

    //銃のトランスフォーム
    public Transform gunTransform;

    public Player_Direction p_direction;

    // Update is called once per frame
    void Update()
    {
        //マウスの位置を取得する
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //銃の位置からマウスの位置へ向かうベクトルの計算
        Vector2 direction = mousePosition - (Vector2)gunTransform.position;
        direction.Normalize();

        //銃の向きを設定
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        angle = Mathf.Clamp(angle, -20f, 20f);

        gunTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //照準の線を描画
        aimLine.SetPosition(0,gunTransform.position);

        Vector3 endPosition = gunTransform.position + Quaternion.Euler(0, 0, angle) * Vector3.right * 2f;

        aimLine.SetPosition(1, endPosition);
    }
}
