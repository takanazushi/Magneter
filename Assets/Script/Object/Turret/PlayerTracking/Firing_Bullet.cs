using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Firing_Bullet : MonoBehaviour
{
    private Camera mainCamera;
    //Prefabsで複製する物を入れる
    [SerializeField, Header("弾Prefabを入れてください")]
    protected GameObject BulletObj;

    void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(ShotCoroutine());
    }
    public virtual IEnumerator ShotCoroutine()
    {
        while (true)
        {
            //カメラ座標取得
            Vector3 viewPos = mainCamera.WorldToViewportPoint(transform.position);

            // todo カメラ範囲内(弾がプレイヤーに届く距離)で生成
            if (viewPos.x > 0.25 && viewPos.x < 1)
            {
                //todo 座標取得
                BulletObj.transform.position = transform.position;
                //生成
                Instantiate(BulletObj);
            }

            // 2.0秒待機する
            yield return new WaitForSeconds(2.0f);
        }
    }


}
