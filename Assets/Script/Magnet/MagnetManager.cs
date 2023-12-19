using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static Magnet;

//�}�O�l�b�g�}�l�[�W���[����
public class MagnetManager : MonoBehaviour
{
    //�Ǘ�����}�O�l�b�g
    [SerializeField]
    List<MagnetUpdateData> gbMagnet;

    //��������I�u�W�F�N�g
    [SerializeField]
    private GameObject MagnetS;

    //[SerializeField]
    //private GameObject MagnetN;

    //[SerializeField]
    //private GameObject MagnetNon;

    //��������G�I�u�W�F�N�g
    [SerializeField]
    private GameObject EnemyMagnet;

    //[SerializeField]
    //private GameObject EnemyMagnetN;

    //[SerializeField]
    //private GameObject EnemyMagnetNon;

    private void Awake()
    {
        foreach (Transform child in MagnetS.GetComponentsInChildren<Transform>())
        {
            GameObject childObject = child.gameObject;
            MagnetUpdateData magnetUpData = new MagnetUpdateData();
            magnetUpData.gbMagnet = childObject.GetComponent<Magnet>();
            magnetUpData.gbRid = childObject.GetComponent<Rigidbody2D>();

            if (childObject.name != MagnetS.name)
            {
                gbMagnet.Add(magnetUpData);
            }
        }

        //foreach (Transform child in MagnetN.GetComponentsInChildren<Transform>())
        //{
        //    GameObject childObject = child.gameObject;
        //    MagnetUpdateData magnetUpData = new MagnetUpdateData();
        //    magnetUpData.gbMagnet = childObject.GetComponent<Magnet>();
        //    magnetUpData.gbRid = childObject.GetComponent<Rigidbody2D>();

        //    if (childObject.name != MagnetN.name)
        //    {
        //        gbMagnet.Add(magnetUpData);
        //    }
        //}

        //foreach (Transform child in MagnetNon.GetComponentsInChildren<Transform>())
        //{
        //    GameObject childObject = child.gameObject;
        //    MagnetUpdateData magnetUpData = new MagnetUpdateData();
        //    magnetUpData.gbMagnet = childObject.GetComponent<Magnet>();
        //    magnetUpData.gbRid = childObject.GetComponent<Rigidbody2D>();

        //    if (childObject.name != MagnetNon.name)
        //    {
        //        gbMagnet.Add(magnetUpData);
        //    }
        //}

        foreach (Transform child in EnemyMagnet.transform)
        {
            GameObject childObject = child.gameObject;
            MagnetUpdateData magnetUpData = new MagnetUpdateData();
            magnetUpData.gbMagnet = childObject.GetComponent<Magnet>();
            magnetUpData.gbRid = childObject.GetComponent<Rigidbody2D>();

            if (childObject.name != EnemyMagnet.name)
            {
                gbMagnet.Add(magnetUpData);
            }
        }
    }

    //�Ώۂ̃I�u�W�F�N�g���擾
    //pos�F�͈͂̒��S�ʒu
    //len�F�͈͂̔��a
    public List<MagnetUpdateData> GetSearchMagnet(Vector2 pos,float len)
    {
        List<MagnetUpdateData> Rgb = new List<MagnetUpdateData>();

        foreach (MagnetUpdateData item in gbMagnet)
        {
            //�͈͓�and
            //�ɂ��Ȃ��A��O�łȂ��ꍇ
            //�̏ꍇ���X�g�ɒǉ�
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
        //�X�y�[�X�L�[���������Ƃ��I�u�W�F�N�g�𐶐�
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Vector3 vector3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    vector3.z = 0;
        //    //�I�u�W�F�N�g�𐶐�
        //    GameObject InOjb = Instantiate(Cyob, vector3, Quaternion.identity);

        //    //�}�O�l�b�g�̐ݒ�
        //    Magnet InMg = InOjb.GetComponent<Magnet>();
        //    InMg.SetMagnetManager = this;
        //    InMg.PuroLengthMagnrt = 10;
        //    InMg.SetType_Magnat(Type_Magnet.S);

        //    //�}�l�[�W���[���X�g�ɒǉ�
        //    Rigidbody2D Inrigi2 = InOjb.GetComponentInChildren<Rigidbody2D>();          
        //    MagnetUpdateData magnetUpdateData = new MagnetUpdateData();
        //    magnetUpdateData.gbMagnet = InMg;
        //    magnetUpdateData.gbRid= Inrigi2;
        //    gbMagnet.Add(magnetUpdateData);

        //}
    }
}
