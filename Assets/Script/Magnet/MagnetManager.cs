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

    [SerializeField]
    List<Transform> Magne_Parent;

    //[SerializeField]
    //private GameObject MagnetS;

    //[SerializeField]
    //private GameObject MagnetN;

    //[SerializeField]
    //private GameObject MagnetNon;

    ////生成する敵オブジェクト
    //[SerializeField]
    //private GameObject EnemyMagnet;

    //[SerializeField]
    //private GameObject EnemyMagnetN;

    //[SerializeField]
    //private GameObject EnemyMagnetNon;

    private void Awake()
    {
        //子オブジェクトのRigidbody2D,Magnet,tagを取得しリストに登録
        if (Magne_Parent.Count != 0)
        {
            foreach (Transform mg_child in Magne_Parent)
            {
                if (mg_child == null) { Debug.Log("mag__Parent"); continue; }

                foreach (Transform child in mg_child.GetComponentsInChildren<Transform>())
                {
                    if (child == null) { Debug.Log("mg_child"); continue; }

                    GameObject childObject = child.gameObject;

                    //親オブジェクトの場合はスキップ
                    if (childObject.name == mg_child.name) { continue; }

                    //情報取得
                    MagnetUpdateData magnetUpData = new()
                    {
                        gbMagnet = childObject.GetComponent<Magnet>(),
                        gbRid = childObject.GetComponent<Rigidbody2D>(),
                        gbtag = childObject.tag
                    };

                    //取得失敗
                    if(magnetUpData.gbMagnet==null||
                        magnetUpData.gbRid == null)
                    {
                        continue;
                    }

                    gbMagnet.Add(magnetUpData);
                }




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
