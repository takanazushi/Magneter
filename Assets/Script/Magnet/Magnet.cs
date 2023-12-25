using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using UnityEngine.U2D;
using static UnityEditor.PlayerSettings;
using Unity.Burst.CompilerServices;

//マグネット
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

    /// <summary>
    /// 磁力影響値（受ける強さ）
    /// </summary>
    [SerializeField]
    private float Power;

    /// <summary>
    /// 磁力最大影響値
    /// </summary>
    [SerializeField]
    private float MaxPower;

    /// <summary>
    /// 磁気の固定化
    /// true:固定
    /// </summary>
    [SerializeField]
    private bool Type_Fixed;

    /// <summary>
    /// デバック表示フラグ
    /// </summary>
    [SerializeField]
    /// <summary>
    /// マグネットの計算を行うか
    /// </summary>
    private bool Magnet_Updetaflg;

    /// <summary>
    /// デバックフラグ
    /// </summary>
    [SerializeField,Header("デバック表示")]
    private bool Debagu_fla;
    
    /// <summary>
    /// デバック表示用
    /// 磁力範囲内マグネットオブジェクトの
    /// 位置と力を保存して、Gizumoで使用します
    /// </summary>
    private List<Transform> Debagu_list = new();

    private void Reset()
    {
        LenMagnrt = 10;
        magnetManager = GameObject.Find("MagnetManager").GetComponent<MagnetManager>();
        Power = 1;
        Type = Type_Magnet.None;
        Type_Fixed = false;
    }

    private void Start()
    {
        // このobjectのSpriteRendererを取得
        MainSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        //極に合わせて色を変える
        SetSprite();

        //この、コンポーネントがある場合FixedUpdateを行わない
        if (GetComponent<Enemy_WalkFall>())
        {
            Magnet_Updetaflg = true;
        }
    }

    private void FixedUpdate()
    {
        if (Magnet_Updetaflg)
        {
            return;
        }

        //デバック用データ初期化
        Debagu_list.Clear();

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
            force = Vector2.ClampMagnitude(force, MaxPower);

            //力を加える
            pair.gbRid.velocity += force;

            //デバック用保存
            Debagu_list.Add(pair.gbRid.transform);
        }

    }

    private void OnDrawGizmos()
    {
        if (!Debagu_fla) { return; }

        //デバック用データが存在する場合
        if (EditorApplication.isPlaying&& Debagu_list.Count!=0)
        {
            foreach (Transform pair in Debagu_list)
            {
                //関係しているマグネットへ線を描画
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, pair.transform.position);
            }
        }
    }

    /// <summary>
    /// 自分が受ける影響の合算を取得
    /// </summary>
    /// <param name="tag">除外タグ</param>
    /// <returns>受ける影響値</returns>
    public Vector2 Magnet_Power(string[] tag=null)
    {
        Vector2 force = Vector2.zero;

        //対象のオブジェクト取得
        List<MagnetUpdateData> list = magnetManager.GetSearchMagnet(this.transform.position, LenMagnrt, tag);

        foreach (MagnetUpdateData pair in list)
        {
            //オブジェクトが
            //orマグネットではない場合※多分ない、一応
            //or自分の場合
            //は処理せず次へ
            if (pair.gbMagnet == null ||
                name == pair.gbRid.name)
            {
                continue;
            }

            //マグネット位置
            Vector2 vector_tocl = pair.gbRid.position;

            //磁力の方向を計算
            Vector2 direction = vector_tocl - (Vector2)transform.position;

            // 磁場の影響度を計算(距離の二乗に反比例)
            float magneticForce = pair.gbMagnet.Power / direction.sqrMagnitude;

            //与える力
            Vector2 pair_force = direction * magneticForce;

            //相手と同じ極だった場合反転
            if (Type == pair.gbMagnet.Type)
            {
                pair_force *= -1;
            }

            //くりっぴんぐ
            pair_force = Vector2.ClampMagnitude(pair_force, 5.0f);

            force += pair_force;
        }

        return force;
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

    #region エディタ
#if UNITY_EDITOR

    [CustomEditor(typeof(Magnet))]
    [CanEditMultipleObjects]
    public class Magnet_Editor : Editor
    {
        private Magnet _target;
        private readonly float _wait_Min = 0.01f;
        private SerializedProperty _script;

        private void OnEnable()
        {
            _target = target as Magnet;
            _script = serializedObject.FindProperty("m_Script");
        }


        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            serializedObject.Update();

            using (new EditorGUI.DisabledScope(true))
            {
                EditorGUILayout.PropertyField(_script);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("magnetManager")
                 , new GUIContent("マグネットマネージャー"));

            EditorGUILayout.PropertyField(serializedObject.FindProperty("MagnetS")
                 , new GUIContent("S画像"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("MagnetN")
                 , new GUIContent("N画像"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("MagnetNone")
                 , new GUIContent("None画像"));

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("Type")
                , new GUIContent("磁気"));

            EditorGUILayout.PropertyField(serializedObject.FindProperty("LenMagnrt")
                , new GUIContent("磁力影響範囲"));

            EditorGUILayout.PropertyField(serializedObject.FindProperty("Power")
                , new GUIContent("磁力影響値","値が大きいほど磁力の影響をより受けます"));

            EditorGUILayout.PropertyField(serializedObject.FindProperty("MaxPower")
                , new GUIContent("磁力最大影響値"));

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("Type_Fixed")
                , new GUIContent("磁気の固定"));

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("Debagu_fla")
                , new GUIContent("デバック表示"));

            if (EditorGUI.EndChangeCheck())
            {
                //値が変更された場合
                serializedObject.ApplyModifiedProperties();
            }
        }
    }




#endif
    #endregion

}
