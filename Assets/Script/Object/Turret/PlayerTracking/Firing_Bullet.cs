using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Firing_Bullet : MonoBehaviour
{
    //Prefabsで複製する物を入れる
    [SerializeField, Header("弾Prefabを入れてください")]
    protected  GameObject BulletObj;

    void Start()
    {   
        StartCoroutine(ShotCoroutine());
    }

    public virtual IEnumerator ShotCoroutine()
    {
        while (true)
        {
            // Prefabを実体化
            Instantiate(BulletObj);

            // 1.5秒待機する
            yield return new WaitForSeconds(1.5f);
        }
    }

}
