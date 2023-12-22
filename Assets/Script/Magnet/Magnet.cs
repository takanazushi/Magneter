using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using Unity.VisualScripting;
using UnityEditor;
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

    //磁気の強さ
    [SerializeField,Header("自分が受ける影響値")]
    private float Power;

    /// <summary>
    /// 磁気の固定化
    /// true:固定
    /// </summary>
    [SerializeField, Header("磁気の固定")]
    private bool Type_Fixed;

    private void Reset()
    {
        LenMagnrt = 10;
        magnetManager = GameObject.Find("MagnetManager").GetComponent<MagnetManager>();
        Power = 0.1f;
        Type = Type_Magnet.None;
        Type_Fixed = false;
    }

    private void Start()
    {
        // このobjectのSpriteRendererを取得
        MainSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        //極に合わせて色を変える
        SetSprite();
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

            //マグネット位置
            Vector2 vector_tocl = pair.gbRid.position;

            //磁力の方向を計算
            Vector2 direction = (Vector2)transform.position - vector_tocl;

            // 磁場の影響度を計算(距離の二乗に反比例)
            float magneticForce = Power / direction.sqrMagnitude;

            //与える力
            Vector2 force = direction * magneticForce;

            //相手と同じ極だった場合反転
            if (Type == pair.gbMagnet.Type)
            {
                force *= -1;
            }

            //くりっぴんぐ
            force = Vector2.ClampMagnitude(force, 1.0f);

            //力を加える
            pair.gbRid.velocity += force;
        }

    }

    /// <summary>
    /// 初期化用自分のテクスチャを変更する
    /// </summary>
    private void SetSprite()
    {
        switch (Type)
        {
            //S極は青
            case Type_Magnet.S:
                MainSpriteRenderer.sprite = MagnetS;
                break;
            //N極は赤
            case Type_Magnet.N:
                MainSpriteRenderer.sprite = MagnetN;
                break;
            //なしは白
            case Type_Magnet.None:
                MainSpriteRenderer.sprite = MagnetNone;
                break;
        }
    }

    /// <summary>
    /// 極によって自分の色を変更する
    /// </summary>
    /// <param name="type">指定した極</param>
    public void SetType_Magnat(Type_Magnet type)
    {
        //磁気固定フラグ確認
        if (Type_Fixed) { return; }

        Type = type;
        switch (Type)
        {
            //S極は青
            case Type_Magnet.S:
                MainSpriteRenderer.sprite = MagnetS;
                break;
            //N極は赤
            case Type_Magnet.N:
                MainSpriteRenderer.sprite = MagnetN;
                break;
            //なしは白
            case Type_Magnet.None:
                MainSpriteRenderer.sprite = MagnetNone;
                break;
        }
    }


}
