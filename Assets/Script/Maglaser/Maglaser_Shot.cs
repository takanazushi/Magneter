using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maglaser_Shot : MonoBehaviour
{
    [SerializeField]
    private GameObject redBulletPrefab; // �Ԃ��e�̃v���n�u

    [SerializeField]
    private GameObject blueBulletPrefab; // ���e�̃v���n�u

    private Vector3 mousePosition; // �}�E�X�̈ʒu��ۑ����邽�߂̕ϐ�

    enum BulletColor
    {
        Red,
        Blue
    }

    BulletColor color;

    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {

            color = BulletColor.Blue;
            ShootBullet(blueBulletPrefab);

        }
        else if (Input.GetMouseButtonDown(1))
        {
            color = BulletColor.Red;
            ShootBullet(redBulletPrefab);
        }
    }

    void ShootBullet(GameObject BulletPrefab)
    {

        GameObject bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);

        if (color == BulletColor.Blue)
        {
            Maglaser_BlueBullet blueBullet = bullet.GetComponent<Maglaser_BlueBullet>();
            blueBullet.SetTargetPosition(mousePosition);
            blueBullet.BulletUpdate();
        }
        else if (color == BulletColor.Red)
        {
            Maglaser_RedBullet redBullet = bullet.GetComponent<Maglaser_RedBullet>();
            redBullet.SetTargetPosition(mousePosition);
            redBullet.BulletUpdate();
        }

    }
}
