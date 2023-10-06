using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Tilemaps;


//マグネット試作
public class Magnet : MonoBehaviour
{

    SpriteRenderer MainSpriteRenderer;
    // publicで宣言し、inspectorで設定可能にする
    //切り替え画像　S,N,未属性
    public Sprite MagnetS;
    public Sprite MagnetN;
    public Sprite MagnetNone;



    //マグネットマネージャー
    [SerializeField]
    private MagnetManager magnetManager;
    public MagnetManager SetMagnetManager
    {
        set { magnetManager = value; }
    }

    //影響を与えられる範囲
    [SerializeField]
    private float LenMagnrt;
    public float PuroLengthMagnrt
    {
        get => LenMagnrt;
        set => LenMagnrt = value;
    }

    //極の種類
    public enum Type_Magnet
    {
        S,      //S極
        N,      //N極
        None,   //磁力なし
        Exc
    }

    //極
    [SerializeField]
    Type_Magnet Type;
    public Type_Magnet PuroTypeManet
    {
        get => Type;
        set => Type = value;
    }

    //極によって自分の色を変更する（仮）
    //type：指定した極
    public void SetType_Magnat(Type_Magnet type)
    {
        // SpriteRenderのspriteを設定済みの他のspriteに変更
        SpriteRenderer Renderer = GetComponent<SpriteRenderer>();

        Type = type;
        switch (Type)
        {
            //S極は青
            case Type_Magnet.S:
                MainSpriteRenderer.sprite = MagnetS;
                //Renderer.color = Color.blue;
                break;
            //N極は赤
            case Type_Magnet.N:
                MainSpriteRenderer.sprite = MagnetN;
                //Renderer.color = Color.red;
                break;
            //なしは白
            case Type_Magnet.None:
                MainSpriteRenderer.sprite = MagnetNone;
                //Renderer.color = Color.white;
                break;
        }
    }

    //箱と弾の判定
    void OnCollisionEnter2D(Collision2D collision)
    {

       
        if (collision.gameObject.tag == "RedBullet")
        {
            SetType_Magnat(Type_Magnet.N);
            Debug.Log("当たった");
        }
        else if (collision.gameObject.tag == "BlueBullet")
        {
            SetType_Magnat(Type_Magnet.S);
        }
    }

    //磁気の強さ
    [SerializeField]
    private float Power;



    private void Start()
    {
        // このobjectのSpriteRendererを取得
        MainSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        //極に合わせて色を変える
        SetType_Magnat(Type);

    }

    private void FixedUpdate()
    {
        //磁力なしの場合は処理しない
        if (Type == Type_Magnet.None)
        {
            return;
        }

        //対象のオブジェクト取得
        List<MagnetUpdateData> list = magnetManager.GetSearchMagnet(this.transform.position, LenMagnrt);

        foreach (MagnetUpdateData pair in list)
        {
            //オブジェクトが
            //orマグネットではない場合※多分ない、一応
            //or自分の場合
            //は処理せず次へ
            if (pair.gbMagnet == null ||
                this.name == pair.gbRid.name)
            {
                continue;
            }

            Vector2 vector_tocl = new Vector2();
            vector_tocl = pair.gbRid.position;

            //タイルマップ用：タイルマップに対応可能かわかんない
            if (pair.Til != null) 
            {

                //int torTilx = (int)pair.gbRid.position.x;
                //int torTily = (int)pair.gbRid.position.y;

                ////タイルのワールド位置を取得
                //Vector3 vector3fol = pair.Til.GetCellCenterWorld(new Vector3Int(torTilx, torTily, 0));
                //Vector3Int vector3Intp = new Vector3Int((int)vector3fol.y, (int)vector3fol.x, 0);

                //Debug.Log(torTilx);
                //Debug.Log(torTily);

                ////ゲームオブジェクト取得
                //GameObject asda = pair.Til.GetInstantiatedObject(vector3Intp);

                ////
                //vector_tocl = asda.transform.position;
            }

            //磁力の方向を計算
            Vector2 lkasd = transform.position;
            Vector2 direction = lkasd - vector_tocl;

            //ベクトルの長さを計算
            float distance = direction.magnitude;

            // 磁場の影響度を計算(距離の二乗に反比例)
            float magneticForce = Power / (distance * distance);

            Vector2 force = new Vector2();
            force = direction.normalized * magneticForce;

            //相手と同じ極だった場合反転
            if (Type == pair.gbMagnet.Type)
            {
                force *= -1;
            }

            //力を加える
            pair.gbRid.AddForce(force);
        }

    }



}
