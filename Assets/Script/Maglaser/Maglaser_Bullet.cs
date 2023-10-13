using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maglaser_Bullet : MonoBehaviour
{
    //基本クラスです。赤弾青弾にはそれぞれのScriptをあてがってください。

    [SerializeField]
    protected float speed = 10f;

    // Update is called once per frame
    public virtual void  BulletUpdate()
    {
        //弾を前進させる
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    public virtual void Fire()
    {
        //
    }

   
}
