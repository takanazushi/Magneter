using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Purchasing;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Firing_NoAim: MonoBehaviour
{
    //Prefabs‚Å•¡»‚·‚é•¨‚ð“ü‚ê‚é(¡‰ñ‚Ìê‡Circle)
    [SerializeField, Header("’ePrefab‚ð“ü‚ê‚Ä‚­‚¾‚³‚¢")]
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
