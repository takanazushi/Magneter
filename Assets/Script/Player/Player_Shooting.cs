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

    private Player_Direction direction;

    private void Start()
    {
        direction = GetComponent<Player_Direction>();
    }

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
        //銃口の位置と向きを取得
        Vector2 gunPosition=gunPoint.position;
        Quaternion gunRotation = Quaternion.identity;

        if (!direction.Rightflag)
        {
            gunRotation = Quaternion.Euler(0, 0, 180);
        }

        //弾作成
        GameObject bullet=Instantiate(BulletPrefab, gunPosition, gunRotation);

        //弾発射
        //まだ処理作成してない
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
