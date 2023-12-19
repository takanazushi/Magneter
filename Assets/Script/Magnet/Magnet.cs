using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using UnityEngine.U2D;

//�}�O�l�b�g
public class Magnet : MonoBehaviour
{

    SpriteRenderer MainSpriteRenderer;
    // public�Ő錾���Ainspector�Őݒ�\�ɂ���
    //�؂�ւ��摜�@S,N,������
    public Sprite MagnetS;
    public Sprite MagnetN;
    public Sprite MagnetNone;
    
    //�}�O�l�b�g�}�l�[�W���[
    [SerializeField]
    private MagnetManager magnetManager;
    public MagnetManager SetMagnetManager
    {
        set { magnetManager = value; }
    }

    //�e����^������͈�
    [SerializeField]
    private float LenMagnrt;
    public float PuroLengthMagnrt
    {
        get => LenMagnrt;
        set => LenMagnrt = value;
    }

    //�ɂ̎��
    public enum Type_Magnet
    {
        S,      //S��
        N,      //N��
        None,   //���͂Ȃ�
        Exc
    }

    //��
    [SerializeField]
    Type_Magnet Type;
    public Type_Magnet PuroTypeManet
    {
        get => Type;
        set => Type = value;
    }

    /// <summary>
    /// ���͉e���l�i�󂯂鋭���j
    /// </summary>
    [SerializeField]
    private float Power;

    /// <summary>
    /// ���C�̌Œ艻
    /// true:�Œ�
    /// </summary>
    [SerializeField]
    private bool Type_Fixed;

    
    [SerializeField]
    private bool Debagu_fla;
    
    /// <summary>
    /// �f�o�b�N�\���p
    /// ���͔͈͓��}�O�l�b�g�I�u�W�F�N�g��
    /// �ʒu�Ɨ͂�ۑ����āAGizumo�Ŏd�l���܂�
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
        // ����object��SpriteRenderer���擾
        MainSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        //�ɂɍ��킹�ĐF��ς���
        SetSprite();
    }

    private void FixedUpdate()
    {
        //�f�o�b�N�p�f�[�^������
        Debagu_list.Clear();

        //���͂Ȃ��̏ꍇ�͏������Ȃ�
        if (Type == Type_Magnet.None)
        {
            return;
        }

        //�Ώۂ̃I�u�W�F�N�g�擾
        List<MagnetUpdateData> list = magnetManager.GetSearchMagnet(this.transform.position, LenMagnrt);

        foreach (MagnetUpdateData pair in list)
        {
            //�I�u�W�F�N�g��
            //or�}�O�l�b�g�ł͂Ȃ��ꍇ�������Ȃ��A�ꉞ
            //or�����̏ꍇ
            //�͏�����������
            if (pair.gbMagnet == null ||
                this.name == pair.gbRid.name)
            {
                continue;
            }

            //�}�O�l�b�g�ʒu
            Vector2 vector_tocl = pair.gbRid.position;

            //���͂̕������v�Z
            Vector2 direction = (Vector2)transform.position - vector_tocl;

            // ����̉e���x���v�Z(�����̓��ɔ����)
            float magneticForce = Power / direction.sqrMagnitude;

            //�^�����
            Vector2 force = direction * magneticForce;

            //����Ɠ����ɂ������ꍇ���]
            if (Type == pair.gbMagnet.Type)
            {
                force *= -1;
            }

            //������҂�
            force = Vector2.ClampMagnitude(force, 1.0f);

            //�͂�������
            pair.gbRid.velocity += force;

            //�f�o�b�N�p�ۑ�
            Debagu_list.Add(pair.gbRid.transform);
        }

    }

    private void OnDrawGizmos()
    {
        if (!Debagu_fla) { return; }

        //�f�o�b�N�p�f�[�^�����݂���ꍇ
        if (EditorApplication.isPlaying&& Debagu_list.Count!=0)
        {
            foreach (Transform pair in Debagu_list)
            {
                //�֌W���Ă���}�O�l�b�g�֐���`��
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, pair.transform.position);
            }
        }
    }

    /// <summary>
    /// �������p�����̃e�N�X�`����ύX����
    /// </summary>
    private void SetSprite()
    {
        switch (Type)
        {
            //S�ɂ͐�
            case Type_Magnet.S:
                MainSpriteRenderer.sprite = MagnetS;
                break;
            //N�ɂ͐�
            case Type_Magnet.N:
                MainSpriteRenderer.sprite = MagnetN;
                break;
            //�Ȃ��͔�
            case Type_Magnet.None:
                MainSpriteRenderer.sprite = MagnetNone;
                break;
        }
    }

    /// <summary>
    /// �ɂɂ���Ď����̐F��ύX����
    /// </summary>
    /// <param name="type">�w�肵����</param>
    public void SetType_Magnat(Type_Magnet type)
    {
        //���C�Œ�t���O�m�F
        if (Type_Fixed) { return; }

        Type = type;
        switch (Type)
        {
            //S�ɂ͐�
            case Type_Magnet.S:
                MainSpriteRenderer.sprite = MagnetS;
                break;
            //N�ɂ͐�
            case Type_Magnet.N:
                MainSpriteRenderer.sprite = MagnetN;
                break;
            //�Ȃ��͔�
            case Type_Magnet.None:
                MainSpriteRenderer.sprite = MagnetNone;
                break;
        }
    }

    #region �G�f�B�^
#if UNITY_EDITOR

    [CustomEditor(typeof(Magnet))]
    public class MoveFloorMNG_Editor : Editor
    {
        private Magnet _target;
        private readonly float _wait_Min = 0.01f;
        private void Awake()
        {
            _target = target as Magnet;
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            serializedObject.Update();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("magnetManager")
                 , new GUIContent("�}�O�l�b�g�}�l�[�W���["));

            EditorGUILayout.PropertyField(serializedObject.FindProperty("MagnetS")
                 , new GUIContent("S�摜"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("MagnetN")
                 , new GUIContent("N�摜"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("MagnetNone")
                 , new GUIContent("None�摜"));

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("Type")
                , new GUIContent("���C"));

            EditorGUILayout.PropertyField(serializedObject.FindProperty("LenMagnrt")
                , new GUIContent("���͉e���͈�"));

            EditorGUILayout.PropertyField(serializedObject.FindProperty("Power")
                , new GUIContent("���͉e���l","�l���傫���قǎ��͂̉e�������󂯂܂�"));

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("Type_Fixed")
                , new GUIContent("���C�̌Œ�"));

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("Debagu_fla")
                , new GUIContent("�f�o�b�N�\��"));

            if (EditorGUI.EndChangeCheck())
            {
                //�l���ύX���ꂽ�ꍇ
                serializedObject.ApplyModifiedProperties();
            }
        }
    }




#endif
    #endregion

}
