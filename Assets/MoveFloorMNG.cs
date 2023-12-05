using System.Collections;
using UnityEngine;
using System;
using Unity.Mathematics;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class MoveFloorMNG : MonoBehaviour
{
    /// <summary>
    /// 指示する足場親オブジェクト
    /// </summary>
    GameObject Parent_MoveFloors;

    /// <summary>
    /// 指示する足場
    /// </summary>
    LineMoveFloor[] moveFloors;

    /// <summary>
    /// ポイントの親オブジェクト
    /// </summary>
    private GameObject Parent_Position;

    /// <summary>
    /// ポイントのTransform
    /// </summary>
    Transform[] Transform_Targets;

    //動く床のスピード
    [SerializeField]
    private float speed = 3;

    public enum MoveType
    {
        /// <summary>
        /// 巡回
        /// </summary>
        /// 最後のポイントと最初のポイントが繋がりループする
        [InspectorName("巡回")]
        Patrol,

        /// <summary>
        /// 往復
        /// </summary>
        /// 最後のポイント到達で最初のポイントに向かって戻る
        [InspectorName("往復")]
        Round_Trip,

        /// <summary>
        /// 一方通行
        /// </summary>
        /// 最後のポイント到達で非アクティブになる
        [InspectorName("一方通行")]
        One_Way
    };
    /// <summary>
    /// 移動タイプ
    /// </summary>
    [SerializeField]
    MoveType moveType = MoveType.Patrol;

    /// <summary>
    /// 足場を生成する頻度
    /// </summary>
    [SerializeField]
    private float wait = 3;

    //待機コルーチン
    WaitForSeconds WaitSeconds;

    #region エディタ用変数

    //折り畳みメニュー1
    [SerializeField]
    private bool accmenu;
    //折り畳みメニュー2
    [SerializeField]
    private bool accmenu1;
    //デバック表示フラグ
    [SerializeField]
    private bool OpenDebug = true;
    //線表示色
    [SerializeField]
    private Color32 ColorArrowDebug=Color.green;
    //ポイント表示色
    [SerializeField]
    private Color32 ColorPointDebug=Color.yellow;

    #endregion

    private void Reset()
    {
        //動く床のスピード
        //speed = 3;


    }

    private void Awake()
    {
        WaitSeconds = new WaitForSeconds(wait);

        //指示する足場の親オブジェクトを設定
        Parent_MoveFloors = SearchChild("MoveFloors").gameObject;

        //ポイントの親オブジェクトを設定
        Parent_Position = SearchChild("MovePoint").gameObject;

        SetTargets();

        //足場参照セット
        moveFloors = new LineMoveFloor[Parent_MoveFloors.transform.childCount];
        MoveFloorInit();
    }

    private void Start()
    {
        //コルーチン開始
        StartCoroutine(StartMove());
    }

    private void FixedUpdate()
    {
        foreach (LineMoveFloor item in moveFloors)
        {
            //終了フラグ
            if (item.EndMoveFLG)
            {
                //非アクティブに
                item.gameObject.SetActive(false);
            }

        }
    }

    /// <summary>
    /// 足場の移動を開始する
    /// </summary>
    /// <returns></returns>
    IEnumerator StartMove()
    {
        yield return WaitSeconds;

        GameObject floor = SearchFloor();

        //非アクティブな足場があれば
        if (floor)
        {
            //有効化
            floor.SetActive(true);
        }

        StartCoroutine(StartMove());
    }

    /// <summary>
    /// 非アクティブな足場オブジェクトを探索
    /// </summary>
    /// <returns></returns>
    GameObject SearchFloor()
    {
        for (int i = 0; i < moveFloors.Length; i++)
        {
            if (!moveFloors[i].gameObject.activeSelf)
            {
                return moveFloors[i].gameObject;
            }
        }

        return null;
    }

    /// <summary>
    /// 子オブジェクトから指定したTransformを取得
    /// </summary>
    /// <param name="tname">オブジェクト名</param>
    /// <returns>Transform:ない場合NULL</returns>
    private Transform SearchChild(string tname)
    {
        foreach (Transform item in transform)
        {
            if (item.name == tname)
                return item;
        }

        return null;
    }

    /// <summary>
    /// 方向転換位置を取得
    /// </summary>
    private void SetTargets()
    {
        int num = 0;
        Transform_Targets = new Transform[Parent_Position.transform.childCount];

        // 子オブジェクトを全て取得する
        foreach (Transform child in Parent_Position.transform)
        {
            Transform_Targets[num] = child;

            num++;
        }

    }

    private void OnDrawGizmos()
    {
        //デバック非表示フラグ
        if (!OpenDebug) { return; }


        //ポイントの親オブジェクトを設定
        Parent_Position = SearchChild("MovePoint").gameObject;

        //ポイント取得
        SetTargets();

        for (int i = 0; i < Transform_Targets.Length; i++)
        {
            Gizmos.color = ColorArrowDebug;

            //往復、一方通行のどちらか、かつ最後の場合は終了
            if ((moveType == MoveType.Round_Trip || moveType == MoveType.One_Way) &&
                i == Transform_Targets.Length - 1)
            {
                break;
            }
            //開始地点
            Vector3 Dlstart = Transform_Targets[i].transform.position;

            //終了地点
            int Endnum = i + 1 < Transform_Targets.Length ? i + 1 : 0;
            Vector3 Dlend = Transform_Targets[Endnum].transform.position;

            //線
            Gizmos.DrawLine(Dlstart, Dlend);

            //分割数を指定
            int Division = 2;

            if (moveType == MoveType.Round_Trip)
            {
                Division = 3;
            }

            //矢印移動
            Vector3 addlen = (Dlstart - Dlend) / Division;
            //矢印用
            Gizmos.DrawLine(Dlend + addlen, Dlend + Quaternion.Euler(0, 0, 40) * ((Dlstart - Dlend).normalized * 0.5f) + addlen);
            Gizmos.DrawLine(Dlend + addlen, Dlend + Quaternion.Euler(0, 0, -40) * ((Dlstart - Dlend).normalized * 0.5f) + addlen);

            //往復の場合は追加で矢印表示
            if (moveType == MoveType.Round_Trip)
            {
                addlen += addlen;
                //矢印用
                Gizmos.DrawLine(Dlend + addlen, Dlend + Quaternion.Euler(0, 0, 220) * ((Dlstart - Dlend).normalized * 0.5f) + addlen);
                Gizmos.DrawLine(Dlend + addlen, Dlend + Quaternion.Euler(0, 0, 140) * ((Dlstart - Dlend).normalized * 0.5f) + addlen);
            }

            Gizmos.color = ColorPointDebug;

            Gizmos.DrawSphere(Dlstart, 0.1f);
        }
    }

    /// <summary>
    /// 足場初期化
    /// </summary>
    private void MoveFloorInit()
    {
        int num = 0;
        foreach (Transform item in Parent_MoveFloors.transform)
        {
            moveFloors[num] = item.GetComponent<LineMoveFloor>();
            moveFloors[num].SetTransform_Targets = Transform_Targets;
            moveFloors[num].SetMovetype = moveType;
            moveFloors[num].Setspeed = speed;
            moveFloors[num].gameObject.SetActive(false);

            num++;
        }

    }

    /// <summary>
    /// inspectorで値が変更されたときに再設定する
    /// </summary>
    private void OnInspectorChange()
    {
        // 実行中のみ
        if (!Application.isPlaying) { return; }

        //再設定
        WaitSeconds = new WaitForSeconds(wait);
        int num = 0;
        foreach (Transform item in Parent_MoveFloors.transform)
        {
            moveFloors[num].SetTransform_Targets = Transform_Targets;
            moveFloors[num].SetMovetype = moveType;
            moveFloors[num].Setspeed = speed;

            num++;
        }

    }


    #region エディタ
#if UNITY_EDITOR

    [CustomEditor(typeof(MoveFloorMNG))]
    public class MoveFloorMNG_Editor : Editor
    {
        private MoveFloorMNG _target;

        //待機時間最小値
        private readonly float _wait_Min = 0.01f;
        private void Awake()
        {
            _target = target as MoveFloorMNG;
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            serializedObject.Update();

            serializedObject.FindProperty("speed").floatValue =
                 EditorGUILayout.FloatField("足場移動速度", _target.speed);

            //説明文追加
            string tooiptext="エラー";
            switch (_target.moveType) 
            {
                case MoveType.Patrol:
                    tooiptext = "最後のポイントと最初のポイントが繋がりループする";
                    break;
                case MoveType.Round_Trip:
                    tooiptext = "最後のポイント到達で最初のポイントに向かって戻る";
                    break;
                case MoveType.One_Way:
                    tooiptext = "最後のポイント到達で非アクティブになる";
                    break;
                    
            }

            serializedObject.FindProperty("moveType").enumValueIndex = Convert.ToInt32(
                 EditorGUILayout.EnumPopup("移動タイプ", _target.moveType));
            EditorGUILayout.HelpBox(tooiptext, MessageType.Info);


            //最小値を設定
            float _wait = math.max(EditorGUILayout.FloatField(
                new GUIContent("足場を生成する頻度", "x秒間隔で足場の移動を開始させる"), _target.wait),
                _wait_Min);
            serializedObject.FindProperty("wait").floatValue = _wait;
              


            //デバック折り畳み
            serializedObject.FindProperty("accmenu").boolValue =
                 EditorGUILayout.Foldout(_target.accmenu, "デバック");
            //デバック
            if (_target.accmenu)
            {
                serializedObject.FindProperty("OpenDebug").boolValue =
                     EditorGUILayout.ToggleLeft("表示", _target.OpenDebug);

                //色設定折り畳み
                serializedObject.FindProperty("accmenu1").boolValue =
                     EditorGUILayout.Foldout(_target.accmenu1, "色設定");
                if (_target.accmenu1)
                {
                    serializedObject.FindProperty("ColorArrowDebug").colorValue =
                        EditorGUILayout.ColorField("矢印表示色", _target.ColorArrowDebug);

                    serializedObject.FindProperty("ColorPointDebug").colorValue =
                        EditorGUILayout.ColorField("ポイント表示色", _target.ColorPointDebug);

                }
            }


            if (EditorGUI.EndChangeCheck())
            {
                AssetDatabase.SaveAssets();
                Undo.RecordObject(_target, "test_");
                EditorUtility.SetDirty(_target);
                
                if (serializedObject.ApplyModifiedProperties())
                {
                    _target.OnInspectorChange();
                }
            }
        }
    }




#endif
    #endregion

}
