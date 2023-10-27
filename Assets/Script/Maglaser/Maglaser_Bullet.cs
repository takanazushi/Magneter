using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maglaser_Bullet : MonoBehaviour
{
    //基本クラスです。赤弾青弾にはそれぞれのScriptをあてがってください。

    protected Transform gunTransform;

    [SerializeField]
    protected Rigidbody2D bulletRigidBody;

    [SerializeField]
    protected float bulletSpeed = 10.0f;

    private Vector3 targetPosition; // マウスの位置を保存するための変数

    public virtual void BulletStart()
    {
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            gunTransform = player.transform.Find("Maglaser");
        }
        else
        {
            Debug.LogError("Player ゲームオブジェクトが見つかりません。");
        }
    }

    public virtual void SetTargetPosition(Vector3 position)
    {
        targetPosition = position;
    }

    public virtual void BulletUpdate()
    {
        Vector3 direction = (targetPosition - gunTransform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bulletRigidBody.velocity = Quaternion.Euler(0, 0, angle) * Vector2.right * bulletSpeed;
    }


}
