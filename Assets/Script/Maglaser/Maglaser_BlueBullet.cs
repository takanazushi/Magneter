using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Magnet;

public class Maglaser_BlueBullet : Maglaser_Bullet
{

    void Update()
    {
        BulletUpdate();
    }

    public override void Fire()
    {
        base.Fire();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Magnet magnet = collision.gameObject.GetComponent<Magnet>();

        if (magnet != null)
        {
            magnet.SetType_Magnat(Type_Magnet.S);
        }

        Destroy(gameObject);
        Debug.Log("ê¬íeÇ™ìñÇΩÇËÇ‹ÇµÇΩÅI");
    }

   
}
