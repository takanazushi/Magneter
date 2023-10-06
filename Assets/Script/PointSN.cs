using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//マウスでマグネットオブジェクトのSNを変更する
public class PointSN : MonoBehaviour
{
    private void Update()
    {
        Magnet.Type_Magnet changeType = Magnet.Type_Magnet.Exc;

        //左クリックでN
        if (Input.GetMouseButton(0))
        {
            changeType= Magnet.Type_Magnet.N;
        }
        //右クリックでS
        else if (Input.GetMouseButton(1))
        {
            changeType = Magnet.Type_Magnet.S;
        }
        //中クリックでなし
        else if (Input.GetMouseButton(2))
        {
            changeType = Magnet.Type_Magnet.None;
        }

        //極がセットされた場合
        if (changeType!=Magnet.Type_Magnet.Exc)
        {
            //マウス位置と重なるオブジェクトを取得
            RaycastHit2D hit =
                Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            //オブジェクトがある場合
            if (hit)
            {
                //ヒットしたオブジェクトのマグネットを取得
                Magnet hitObMg = hit.transform.GetComponent<Magnet>();

                //マグネットがある場合
                if (hitObMg != null)
                {
                    //指定した極をセット
                    hitObMg.SetType_Magnat(changeType);
                }
            }

        }        
    }

}
