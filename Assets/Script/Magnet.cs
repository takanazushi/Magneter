using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
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

    //�ɂɂ���Ď����̐F��ύX����i���j
    //type�F�w�肵����
    public void SetType_Magnat(Type_Magnet type)
    {
        // SpriteRender��sprite��ݒ�ς݂̑���sprite�ɕύX
        SpriteRenderer Renderer = GetComponent<SpriteRenderer>();

        Type = type;
        switch (Type)
        {
            //S�ɂ͐�
            case Type_Magnet.S:
                MainSpriteRenderer.sprite = MagnetS;
                //Renderer.color = Color.blue;
                break;
            //N�ɂ͐�
            case Type_Magnet.N:
                MainSpriteRenderer.sprite = MagnetN;
                //Renderer.color = Color.red;
                break;
            //�Ȃ��͔�
            case Type_Magnet.None:
                MainSpriteRenderer.sprite = MagnetNone;
                //Renderer.color = Color.white;
                break;
        }
    }

    //���ƒe�̔���
    void OnCollisionEnter2D(Collision2D collision)
    {

       
        if (collision.gameObject.tag == "RedBullet")
        {
            SetType_Magnat(Type_Magnet.N);
            Debug.Log("��������");
        }
        else if (collision.gameObject.tag == "BlueBullet")
        {
            SetType_Magnat(Type_Magnet.S);
        }
    }

    //���C�̋���
    [SerializeField]
    private float Power;



    private void Start()
    {
        // ����object��SpriteRenderer���擾
        MainSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        //�ɂɍ��킹�ĐF��ς���
        SetType_Magnat(Type);

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

            Vector2 vector_tocl = new Vector2();
            vector_tocl = pair.gbRid.position;

            //�^�C���}�b�v�p�F�^�C���}�b�v�ɑΉ��\���킩��Ȃ�
            if (pair.Til != null) 
            {

                //int torTilx = (int)pair.gbRid.position.x;
                //int torTily = (int)pair.gbRid.position.y;

                ////�^�C���̃��[���h�ʒu���擾
                //Vector3 vector3fol = pair.Til.GetCellCenterWorld(new Vector3Int(torTilx, torTily, 0));
                //Vector3Int vector3Intp = new Vector3Int((int)vector3fol.y, (int)vector3fol.x, 0);

                //Debug.Log(torTilx);
                //Debug.Log(torTily);

                ////�Q�[���I�u�W�F�N�g�擾
                //GameObject asda = pair.Til.GetInstantiatedObject(vector3Intp);

                ////
                //vector_tocl = asda.transform.position;
            }

            //���͂̕������v�Z
            Vector2 lkasd = transform.position;
            Vector2 direction = lkasd - vector_tocl;

            //�x�N�g���̒������v�Z
            float distance = direction.magnitude;

            // ����̉e���x���v�Z(�����̓��ɔ����)
            float magneticForce = Power / (distance * distance);

            Vector2 force = new Vector2();
            force = direction.normalized * magneticForce;

            //����Ɠ����ɂ������ꍇ���]
            if (Type == pair.gbMagnet.Type)
            {
                force *= -1;
            }

            //�͂�������
            pair.gbRid.AddForce(force);
        }

    }



}
