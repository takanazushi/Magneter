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
    /// �w�����鑫��e�I�u�W�F�N�g
    /// </summary>
    GameObject Parent_MoveFloors;

    /// <summary>
    /// �w�����鑫��
    /// </summary>
    LineMoveFloor[] moveFloors;

    /// <summary>
    /// �|�C���g�̐e�I�u�W�F�N�g
    /// </summary>
    private GameObject Parent_Position;

    /// <summary>
    /// �|�C���g��Transform
    /// </summary>
    Transform[] Transform_Targets;

    //�������̃X�s�[�h
    [SerializeField]
    private float speed;

    public enum MoveType
    {
        /// <summary>
        /// ����
        /// </summary>
        /// �Ō�̃|�C���g�ƍŏ��̃|�C���g���q���胋�[�v����
        [InspectorName("����")]
        Patrol,

        /// <summary>
        /// ����
        /// </summary>
        /// �Ō�̃|�C���g���B�ōŏ��̃|�C���g�Ɍ������Ė߂�
        [InspectorName("����")]
        Round_Trip,

        /// <summary>
        /// ����ʍs
        /// </summary>
        /// �Ō�̃|�C���g���B�Ŕ�A�N�e�B�u�ɂȂ�
        [InspectorName("����ʍs")]
        One_Way
    };
    [SerializeField]
    MoveType moveType = MoveType.Patrol;

    [SerializeField]
    private float wait;

    //�R���[�`���ҋ@����
    WaitForSeconds WaitSeconds;

    #region �G�f�B�^�p

    //�܂��݃��j���[1
    [SerializeField]
    private bool accmenu;
    //�܂��݃��j���[2
    [SerializeField]
    private bool accmenu1;
    //�f�o�b�N�\���t���O
    [SerializeField]
    private bool OpenDebug = false;
    //���\���F
    [SerializeField]
    private Color32 ColorArrowDebug;
    //�|�C���g�\���F
    [SerializeField]
    private Color32 ColorPointDebug;

    #endregion

    private void Awake()
    {
        WaitSeconds = new WaitForSeconds(wait);

        //�w�����鑫��̐e�I�u�W�F�N�g��ݒ�
        Parent_MoveFloors = SearchChild("movefloors").gameObject;

        //�|�C���g�̐e�I�u�W�F�N�g��ݒ�
        Parent_Position = SearchChild("LineMovePoint").gameObject;

        SetTargets();

        //����Q�ƃZ�b�g
        moveFloors = new LineMoveFloor[Parent_MoveFloors.transform.childCount];
        MoveFloorInit();
    }

    private void Start()
    {
        //�R���[�`���J�n
        StartCoroutine(StartMove());
    }

    private void FixedUpdate()
    {
        foreach (LineMoveFloor item in moveFloors)
        {
            //�I���t���O
            if (item.EndMoveFLG)
            {
                //��A�N�e�B�u��
                item.gameObject.SetActive(false);
            }

        }
    }

    /// <summary>
    /// ����̈ړ����J�n����
    /// </summary>
    /// <returns></returns>
    IEnumerator StartMove()
    {
        yield return WaitSeconds;

        GameObject floor = SearchFloor();

        //��A�N�e�B�u�ȑ��ꂪ�����
        if (floor)
        {
            //�L����
            floor.SetActive(true);
        }

        StartCoroutine(StartMove());
    }

    /// <summary>
    /// ��A�N�e�B�u�ȑ���I�u�W�F�N�g��T��
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
    /// �q�I�u�W�F�N�g����w�肵��Transform���擾
    /// </summary>
    /// <param name="tname">�I�u�W�F�N�g��</param>
    /// <returns>Transform:�Ȃ��ꍇNULL</returns>
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
    /// �����]���ʒu���擾
    /// </summary>
    private void SetTargets()
    {
        int num = 0;
        Transform_Targets = new Transform[Parent_Position.transform.childCount];

        // �q�I�u�W�F�N�g��S�Ď擾����
        foreach (Transform child in Parent_Position.transform)
        {
            Transform_Targets[num] = child;

            num++;
        }

    }

    private void OnDrawGizmos()
    {
        //�f�o�b�N��\���t���O
        if (!OpenDebug) { return; }


        //�|�C���g�̐e�I�u�W�F�N�g��ݒ�
        Parent_Position = SearchChild("LineMovePoint").gameObject;

        //�|�C���g�擾
        SetTargets();

        for (int i = 0; i < Transform_Targets.Length; i++)
        {
            Gizmos.color = ColorArrowDebug;

            //�����A����ʍs�̂ǂ��炩�A���Ō�̏ꍇ�͏I��
            if ((moveType == MoveType.Round_Trip || moveType == MoveType.One_Way) &&
                i == Transform_Targets.Length - 1)
            {
                break;
            }
            //�J�n�n�_
            Vector3 Dlstart = Transform_Targets[i].transform.position;

            //�I���n�_
            int Endnum = i + 1 < Transform_Targets.Length ? i + 1 : 0;
            Vector3 Dlend = Transform_Targets[Endnum].transform.position;

            //��
            Gizmos.DrawLine(Dlstart, Dlend);

            //���������w��
            int Division = 2;

            if (moveType == MoveType.Round_Trip)
            {
                Division = 3;
            }

            //���ړ�
            Vector3 addlen = (Dlstart - Dlend) / Division;
            //���p
            Gizmos.DrawLine(Dlend + addlen, Dlend + Quaternion.Euler(0, 0, 40) * ((Dlstart - Dlend).normalized * 0.5f) + addlen);
            Gizmos.DrawLine(Dlend + addlen, Dlend + Quaternion.Euler(0, 0, -40) * ((Dlstart - Dlend).normalized * 0.5f) + addlen);

            //�����̏ꍇ�͒ǉ��Ŗ��\��
            if (moveType == MoveType.Round_Trip)
            {
                addlen += addlen;
                //���p
                Gizmos.DrawLine(Dlend + addlen, Dlend + Quaternion.Euler(0, 0, 220) * ((Dlstart - Dlend).normalized * 0.5f) + addlen);
                Gizmos.DrawLine(Dlend + addlen, Dlend + Quaternion.Euler(0, 0, 140) * ((Dlstart - Dlend).normalized * 0.5f) + addlen);
            }

            Gizmos.color = ColorPointDebug;

            Gizmos.DrawSphere(Dlstart, 0.1f);
        }
    }

    /// <summary>
    /// ���ꏉ����
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
    /// inspector�Œl���ύX���ꂽ�Ƃ��ɍĐݒ肷��
    /// </summary>
    private void OnInspectorChange()
    {
        // ���s���̂�
        if (!Application.isPlaying) { return; }

        //�Đݒ�
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


    #region �G�f�B�^
#if UNITY_EDITOR

    [CustomEditor(typeof(MoveFloorMNG))]
    public class MoveFloorMNG_Editor : Editor
    {
        private MoveFloorMNG _target;
        private readonly float _wait_Min = 0.01f;

        private void OnEnable()
        {
            _target = target as MoveFloorMNG;
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            serializedObject.Update();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("speed")
            , new GUIContent("����ړ����x"));

            //�������ǉ�
            string tooiptext ="�G���[";
            switch (_target.moveType) 
            {
                case MoveType.Patrol:
                    tooiptext = "�Ō�̃|�C���g�ƍŏ��̃|�C���g���q���胋�[�v����";
                    break;
                case MoveType.Round_Trip:
                    tooiptext = "�Ō�̃|�C���g���B�ōŏ��̃|�C���g�Ɍ������Ė߂�";
                    break;
                case MoveType.One_Way:
                    tooiptext = "�Ō�̃|�C���g���B�Ŕ�A�N�e�B�u�ɂȂ�";
                    break;
                    
            }
            EditorGUILayout.PropertyField(serializedObject.FindProperty("moveType")
            , new GUIContent("�ړ��^�C�v"));
            EditorGUILayout.HelpBox(tooiptext, MessageType.Info);


            EditorGUILayout.PropertyField(serializedObject.FindProperty("wait")
            , new GUIContent ("����𐶐�����p�x", "x�b�Ԋu�ő���̈ړ����J�n������"));
            //�ŏ��l��ݒ�
            serializedObject.FindProperty("wait").floatValue =
                math.max(_wait_Min, serializedObject.FindProperty("wait").floatValue);
              
            //�f�o�b�N�܂���
            serializedObject.FindProperty("accmenu").boolValue =
                 EditorGUILayout.Foldout(_target.accmenu, "�f�o�b�N");
            //�f�o�b�N
            if (_target.accmenu)
            {
                serializedObject.FindProperty("OpenDebug").boolValue =
                     EditorGUILayout.ToggleLeft("�\��", _target.OpenDebug);

                //�F�ݒ�܂���
                serializedObject.FindProperty("accmenu1").boolValue =
                     EditorGUILayout.Foldout(_target.accmenu1, "�F�ݒ�");
                if (_target.accmenu1)
                {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("ColorArrowDebug")
                    , new GUIContent("���\���F"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("ColorPointDebug")
                    , new GUIContent("�|�C���g�\���F"));
                }
            }


            if (EditorGUI.EndChangeCheck())
            {
                AssetDatabase.SaveAssets();
                Undo.RecordObject(_target, "test_");
                EditorUtility.SetDirty(_target);
                
                //�l���ύX���ꂽ�ꍇ
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
