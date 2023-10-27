using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Magnet;

//マグネットマネージャー試作
public class MagnetManager : MonoBehaviour
{


    //管理するマグネット
    [SerializeField]
    List<MagnetUpdateData> gbMagnet;

    //生成するオブジェクト
    [SerializeField]
    private GameObject Cyob;

    //対象のオブジェクトを取得
    //pos：範囲の中心位置
    //len：範囲の半径
    public List<MagnetUpdateData> GetSearchMagnet(Vector2 pos,float len)
    {
        List<MagnetUpdateData> Rgb = new List<MagnetUpdateData>();

        foreach (MagnetUpdateData item in gbMagnet)
        {
            //範囲内and
            //極がなし、例外でない場合
            //の場合リストに追加
            if (len* len>= (item.gbRid.position-pos).sqrMagnitude&&
                item.gbMagnet.PuroTypeManet != Type_Magnet.None&&
                item.gbMagnet.PuroTypeManet != Type_Magnet.Exc)
            {
                Rgb.Add(item);
            }
        }

        return Rgb;
    }

    private void Update()
    {
        //スペースキーを押したときオブジェクトを生成
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Vector3 vector3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    vector3.z = 0;
        //    //オブジェクトを生成
        //    GameObject InOjb = Instantiate(Cyob, vector3, Quaternion.identity);

        //    //マグネットの設定
        //    Magnet InMg = InOjb.GetComponent<Magnet>();
        //    InMg.SetMagnetManager = this;
        //    InMg.PuroLengthMagnrt = 10;
        //    InMg.SetType_Magnat(Type_Magnet.S);

        //    //マネージャーリストに追加
        //    Rigidbody2D Inrigi2 = InOjb.GetComponentInChildren<Rigidbody2D>();          
        //    MagnetUpdateData magnetUpdateData = new MagnetUpdateData();
        //    magnetUpdateData.gbMagnet = InMg;
        //    magnetUpdateData.gbRid= Inrigi2;
        //    gbMagnet.Add(magnetUpdateData);

        //}
    }
}
