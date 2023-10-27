using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Magnet;

public class Maglaser_BlueBullet : Maglaser_Bullet
{
    // Update is called once per frame
    private void Awake()
    {
        BulletStart();
    }


    void Update()
    {
        BulletUpdate();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Magnet magnet = collision.gameObject.GetComponent<Magnet>();
        if (magnet != null)
        {
            magnet.SetType_Magnat(Type_Magnet.N);
        }

        Destroy(gameObject);
        Debug.Log("ê¬íeÇ™ìñÇΩÇËÇ‹ÇµÇΩÅI");
    }

}
