using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maglaser_RedBullet : Maglaser_Bullet
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BulletUpdate();
    }

    public override void Fire()
    {
        base.Fire();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
