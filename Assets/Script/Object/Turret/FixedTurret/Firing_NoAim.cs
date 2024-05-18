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
    //Prefabs‚Å•¡»‚·‚é•¨‚ð“ü‚ê‚é(¡‰ñ‚Ìê‡Circle)
    [SerializeField, Header("’ePrefab‚ð“ü‚ê‚Ä‚­‚¾‚³‚¢")]
    private GameObject bulletPrefab;

    [SerializeField]
    private float fireRate = 1.0f;

    [SerializeField]
    private bool shootToLeft;

    //todo
    [SerializeField]
    private bool shoottoup;

    private float nextFireTime;

    //todo
    private Camera mainCamera;

    void Start()
    {
        //todo
        mainCamera = Camera.main;
    }

    void Update()
    {
        //todo
        Vector3 viewPos = mainCamera.WorldToViewportPoint(transform.position);

        if (Time.time > nextFireTime && viewPos.x > 0 && viewPos.x < 1)
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
            //if (shootToLeft)
            //{
            //    bulletScript.SetDirection(Vector2.left);
            //}
            //else
            //{
            //    bulletScript.SetDirection(Vector2.right);
            //}

            if(shoottoup)
            {
                bulletScript.SetDirection(Vector2.up);
            }
            else
            {
                bulletScript.SetDirection(Vector2.down);
            }
        }
    }
}
