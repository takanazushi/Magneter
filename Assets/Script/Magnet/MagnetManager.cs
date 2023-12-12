using System.Collections.Generic;
using UnityEngine;
using static Magnet;

//�}�O�l�b�g�}�l�[�W���[����
public class MagnetManager : MonoBehaviour
{
    //�Ǘ�����}�O�l�b�g
    [SerializeField, Header("�Ǘ��}�O�l�b�g")
     , Tooltip("�蓮�ݒ�\�ł�")]
    List<MagnetUpdateData> gbMagnet;

    //��������I�u�W�F�N�g
    [SerializeField]
    private GameObject MagnetS;

    [SerializeField]
    private GameObject MagnetN;

    [SerializeField]
    private GameObject MagnetNon;


    private void Awake()
    {
        //�q�I�u�W�F�N�g��Rigidbody2D��Magnet���擾�����X�g�ɓo�^
        if (MagnetS)
        {
            foreach (Transform child in MagnetS.GetComponentsInChildren<Transform>())
            {
                GameObject childObject = child.gameObject;
                MagnetUpdateData magnetUpData = new()
                {
                    gbMagnet = childObject.GetComponent<Magnet>(),
                    gbRid = childObject.GetComponent<Rigidbody2D>()
                };

                if (childObject.name != MagnetS.name)
                {
                    gbMagnet.Add(magnetUpData);
                }
            }
        }

        if (MagnetN)
        {
            foreach (Transform child in MagnetN.GetComponentsInChildren<Transform>())
            {
                GameObject childObject = child.gameObject;
                MagnetUpdateData magnetUpData = new()
                {
                    gbMagnet = childObject.GetComponent<Magnet>(),
                    gbRid = childObject.GetComponent<Rigidbody2D>()
                };

                if (childObject.name != MagnetN.name)
                {
                    gbMagnet.Add(magnetUpData);
                }
            }

        }

        if (MagnetNon)
        {
            foreach (Transform child in MagnetNon.GetComponentsInChildren<Transform>())
            {
                GameObject childObject = child.gameObject;
                MagnetUpdateData magnetUpData = new()
                {
                    gbMagnet = childObject.GetComponent<Magnet>(),
                    gbRid = childObject.GetComponent<Rigidbody2D>()
                };

                if (childObject.name != MagnetNon.name)
                {
                    gbMagnet.Add(magnetUpData);
                }
            }

        }
    }

    //�Ώۂ̃I�u�W�F�N�g���擾
    //pos�F�͈͂̒��S�ʒu
    //len�F�͈͂̔��a
    public List<MagnetUpdateData> GetSearchMagnet(Vector2 pos, float len)
    {
        List<MagnetUpdateData> Rgb = new();

        foreach (MagnetUpdateData item in gbMagnet)
        {
            Type_Magnet type_ = item.gbMagnet.PuroTypeManet;
            //�͈͓�and
            //�ɂ��Ȃ��A��O�łȂ��ꍇ
            //�̏ꍇ���X�g�ɒǉ�
            if (type_ != Type_Magnet.None &&
                type_ != Type_Magnet.Exc &&
                len * len >= (item.gbRid.position - pos).sqrMagnitude)
            {
                Rgb.Add(item);
            }
        }

        return Rgb;
    }

}
