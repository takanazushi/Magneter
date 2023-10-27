using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Magnet;

public class Maglaser_RedBullet : Maglaser_Bullet
{

    private void Awake()
    {
        BulletStart();
    }


    // Update is called once per frame
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
        Debug.Log("ê‘íeÇ™ìñÇΩÇËÇ‹ÇµÇΩÅI");
    }

}
