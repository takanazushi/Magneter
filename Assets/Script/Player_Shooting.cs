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
        //eŒû‚ÌˆÊ’u‚ÆŒü‚«‚ğæ“¾
        Vector2 gunPosition=gunPoint.position;
        Quaternion gunRotation = gunPoint.rotation;

        //’eì¬
        GameObject bullet=Instantiate(BulletPrefab, gunPosition, gunRotation);

        //’e”­Ë
        //‚Ü‚¾ˆ—ì¬‚µ‚Ä‚È‚¢
    }
}
