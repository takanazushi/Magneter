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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootBullet(blueBulletPrefab);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            ShootBullet(redBulletPrefab);
        }
    }

    private void ShootBullet(GameObject BulletPrefab)
    {
        //銃口の位置と向きを取得
        Vector2 gunPosition=gunPoint.position;
        Quaternion gunRotation = gunPoint.rotation;

        //弾作成
        GameObject bullet=Instantiate(BulletPrefab, gunPosition, gunRotation);

        //弾発射
        //まだ処理作成してない
    }
}
