using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maglaser_Bullet : MonoBehaviour
{
    //基本クラスです。赤弾青弾にはそれぞれのScriptをあてがってください。

    [SerializeField]
    protected float speed = -10f;

    [SerializeField]
    protected Rigidbody2D rb;
    Vector2 direction;

    [SerializeField]
    protected Transform gunTransform;

    // Update is called once per frame
    public virtual void  BulletUpdate()
    {
        //弾を前進させる
        //transform.Translate(Vector2.right * speed * Time.deltaTime);
        rb.velocity = direction * speed;
    }

    public virtual void Fire()
    {
        //マウスの位置を取得する
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //銃の位置からマウスの位置へ向かうベクトルの計算
        direction = mousePosition - (Vector2)gunTransform.position;
        direction.Normalize();

        //銃の向きを設定
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        angle = Mathf.Clamp(angle, -20f, 20f);

        gunTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        rb.velocity = direction * speed;
    }

   
}
