using System.Collections.Generic;
using UnityEngine;
using static Magnet;

//マグネットマネージャー試作
public class MagnetManager : MonoBehaviour
{
    //管理するマグネット
    List<MagnetUpdateData> gbMagnet;

    [SerializeField,Header("管理するマグネットの親")]
    List<Transform> Magne_Parent;

    [SerializeField,Header("管理するマグネット")]
    List<Transform> Magne_ojt;

    private void Awake()
    {
        gbMagnet = new();
        //子オブジェクトのRigidbody2D,Magnet,tagを取得しリストに登録
        if (Magne_Parent.Count != 0)
        {
            foreach (Transform mg_child in Magne_Parent)
            {
                if (mg_child == null) { continue; }

                foreach (Transform child in mg_child)
                {
                    if (child == null) { continue; }

                    //親オブジェクトの場合はスキップ
                    if (child.name == mg_child.name) { continue; }

                    //情報取得
                    MagnetUpdateData magnetUpData = new()
                    {
                        gbMagnet = child.GetComponent<Magnet>(),
                        gbRid = child.GetComponent<Rigidbody2D>(),
                        gbtag = child.tag
                    };

                    //取得失敗
                    if(magnetUpData.gbMagnet==null||
                        magnetUpData.gbRid == null)
                    {
                        Debug.Log("エラー");
                        continue;
                    }
                    gbMagnet.Add(magnetUpData);
                }
            }
        }

        if (Magne_ojt.Count != 0)
        {
            foreach (Transform child in Magne_ojt)
            {
                if (child == null) {  continue; }

                //情報取得
                MagnetUpdateData magnetUpData = new()
                {
                    gbMagnet = child.GetComponent<Magnet>(),
                    gbRid = child.GetComponent<Rigidbody2D>(),
                    gbtag = child.tag
                };

                //取得失敗
                if (magnetUpData.gbMagnet == null ||
                    magnetUpData.gbRid == null)
                {
                    continue;
                }
                gbMagnet.Add(magnetUpData);
            }
        }
    }

    /// <summary>
    /// マグネット対象のオブジェクトを取得
    /// </summary>
    /// <param name="pos">範囲の中心位置</param>
    /// <param name="len">範囲の半径</param>
    /// <param name="tag">除外タグ</param>
    /// <returns>対象のオブジェクト</returns>
    public List<MagnetUpdateData> GetSearchMagnet(Vector2 pos, float len, string[] tag=null)
    {
        List<MagnetUpdateData> Rgb = new();

        foreach (MagnetUpdateData item in gbMagnet)
        {
            //非アクティブは対象外
            if (!item.gbRid.gameObject.activeSelf) { continue; }

            bool hit = false;
            //タグが一致した場合除外
            if (tag != null)
            {
                foreach(string t_itemin in tag)
                {
                    if (item.gbtag == t_itemin)
                    {
                        hit = true;
                        break;
                    }
                }
            }

            if (!hit)
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
        }


        return Rgb;
    }

}
