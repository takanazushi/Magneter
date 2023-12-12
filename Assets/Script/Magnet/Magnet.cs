using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;


//�}�O�l�b�g����
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

    //���C�̋���
    [SerializeField,Header("�������󂯂�e���l")]
    private float Power;

    /// <summary>
    /// ���C�̌Œ艻
    /// true:�Œ�
    /// </summary>
    [SerializeField, Header("���C�̌Œ�")]
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
        // ����object��SpriteRenderer���擾
        MainSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        //�ɂɍ��킹�ĐF��ς���
        SetSprite();
    }

    private void FixedUpdate()
    {
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


}
