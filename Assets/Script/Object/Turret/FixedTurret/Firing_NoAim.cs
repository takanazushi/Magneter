using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Firing_NoAim: MonoBehaviour
{
    //Prefabsで複製する物を入れる(今回の場合Circle)
    [SerializeField, Header("弾Prefabを入れてください")]
    private GameObject bulletPrefab;

    [SerializeField]
    private float fireRate = 1.0f;

    [SerializeField]
    private bool shootToLeft = true;

    private float nextFireTime;

    void Update()
    {
        if (Time.time > nextFireTime)
        {
            FireBullet();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        Turret_Bullet bulletScript = bullet.GetComponent<Turret_Bullet>();

        if (bulletScript != null)
        {
            if (shootToLeft)
            {
                bulletScript.SetDirection(Vector2.left);
            }
            else
            {
                bulletScript.SetDirection(Vector2.right);
            }

        }
    }
}
