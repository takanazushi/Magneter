using System.Collections.Generic;
using UnityEngine;
using static Magnet;

//マグネットマネージャー試作
public class MagnetManager : MonoBehaviour
{
    //管理するマグネット
    [SerializeField, Header("管理マグネット")
     , Tooltip("手動設定可能です")]
    List<MagnetUpdateData> gbMagnet;

    //生成するオブジェクト
    [SerializeField]
    private GameObject MagnetS;

    [SerializeField]
    private GameObject MagnetN;

    [SerializeField]
    private GameObject MagnetNon;


    private void Awake()
    {
        //子オブジェクトのRigidbody2DとMagnetを取得しリストに登録
        if (MagnetS)
        {
            foreach (Transform child in MagnetS.GetComponentsInChildren<Transform>())
            {
                GameObject childObject = child.gameObject;
                MagnetUpdateData magnetUpData = new()
                {
                    gbMagnet = childObject.GetComponent<Magnet>(),
                    gbRid = childObject.GetComponent<Rigidbody2D>()
                };

                if (childObject.name != MagnetS.name)
                {
                    gbMagnet.Add(magnetUpData);
                }
            }
        }

        if (MagnetN)
        {
            foreach (Transform child in MagnetN.GetComponentsInChildren<Transform>())
            {
                GameObject childObject = child.gameObject;
                MagnetUpdateData magnetUpData = new()
                {
                    gbMagnet = childObject.GetComponent<Magnet>(),
                    gbRid = childObject.GetComponent<Rigidbody2D>()
                };

                if (childObject.name != MagnetN.name)
                {
                    gbMagnet.Add(magnetUpData);
                }
            }

        }

        if (MagnetNon)
        {
            foreach (Transform child in MagnetNon.GetComponentsInChildren<Transform>())
            {
                GameObject childObject = child.gameObject;
                MagnetUpdateData magnetUpData = new()
                {
                    gbMagnet = childObject.GetComponent<Magnet>(),
                    gbRid = childObject.GetComponent<Rigidbody2D>()
                };

                if (childObject.name != MagnetNon.name)
                {
                    gbMagnet.Add(magnetUpData);
                }
            }

        }
    }

    //対象のオブジェクトを取得
    //pos：範囲の中心位置
    //len：範囲の半径
    public List<MagnetUpdateData> GetSearchMagnet(Vector2 pos, float len)
    {
        List<MagnetUpdateData> Rgb = new();

        foreach (MagnetUpdateData item in gbMagnet)
        {
            Type_Magnet type_ = item.gbMagnet.PuroTypeManet;
            //範囲内and
            //極がなし、例外でない場合
            //の場合リストに追加
            if (type_ != Type_Magnet.None &&
                type_ != Type_Magnet.Exc &&
                len * len >= (item.gbRid.position - pos).sqrMagnitude)
            {
                Rgb.Add(item);
            }
        }

        return Rgb;
    }

}
