using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maglaser_Bullet : MonoBehaviour
{
    //基本クラスです。赤弾青弾にはそれぞれのScriptをあてがってください。

    //マグレーザーのトランスフォーム
    protected Transform gunTransform;

    [SerializeField,Header("弾のRigidBody2D")]
    protected Rigidbody2D bulletRigidBody;

    [SerializeField,Header("弾の速度")]
    protected float bulletSpeed = 10.0f;

    // マウスの位置を保存するための変数
    private Vector3 targetPosition; 

    public virtual void BulletStart()
    {
        FindGunTranform();
    }

    public virtual void SetTargetPosition(Vector3 position)
    {
        //弾の目標位置を設定する
        targetPosition = position;
    }

    public virtual void BulletUpdate()
    {
        UpdateBulletVelocity();
    }

    /// <summary>
    /// 銃のトランスフォームを見つけて設定する
    /// </summary>
    protected void FindGunTranform()
    {
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            //プレイヤーの銃のトランスフォームを取得
            gunTransform = player.transform.Find("Maglaser");
        }
        else
        {
            //エラーメッセージを表示
            Debug.LogError("Player ゲームオブジェクトが見つかりません。");
        }
    }

    /// <summary>
    /// 弾の速度を更新する
    /// </summary>
    protected void UpdateBulletVelocity()
    {
        Vector3 direction = (targetPosition - gunTransform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bulletRigidBody.velocity = Quaternion.Euler(0, 0, angle) * Vector2.right * bulletSpeed;
    }


}
