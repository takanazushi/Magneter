using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shooting : MonoBehaviour
{
    [SerializeField]
    private GameObject redBulletPrefab;

    [SerializeField]
    private GameObject blueBulletPrefab;

    [SerializeField]
    private Transform gunPoint;

    private bool bulletflag = false;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            bulletflag = true;
            ShootBullet(blueBulletPrefab);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            bulletflag = false;
            ShootBullet(redBulletPrefab);
        }
    }

    private void ShootBullet(GameObject BulletPrefab)
    {
        //eŒû‚ÌˆÊ’u‚ÆŒü‚«‚ğæ“¾
        Vector2 gunPosition=gunPoint.position;
        Quaternion gunRotation = gunPoint.rotation;

        //’eì¬
        GameObject bullet=Instantiate(BulletPrefab, gunPosition, gunRotation);

        //’e”­Ë
        //‚Ü‚¾ˆ—ì¬‚µ‚Ä‚È‚¢
        if (bulletflag == true)
        {
            Maglaser_BlueBullet blueBullet = bullet.GetComponent<Maglaser_BlueBullet>();
            blueBullet.Fire();
        }
        else if (bulletflag == false)
        {
            Maglaser_RedBullet redBullet = bullet.GetComponent<Maglaser_RedBullet>();
            redBullet.Fire();
        }
        
    }
}
