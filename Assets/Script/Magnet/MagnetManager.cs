using System.Collections.Generic;
using UnityEngine;
using static Magnet;

//�}�O�l�b�g�}�l�[�W���[����
public class MagnetManager : MonoBehaviour
{
    //�Ǘ�����}�O�l�b�g
    List<MagnetUpdateData> gbMagnet;

    [SerializeField,Header("�Ǘ�����}�O�l�b�g�̐e")]
    List<Transform> Magne_Parent;

    [SerializeField,Header("�Ǘ�����}�O�l�b�g")]
    List<Transform> Magne_ojt;

    private void Awake()
    {
        gbMagnet = new();
        //�q�I�u�W�F�N�g��Rigidbody2D,Magnet,tag���擾�����X�g�ɓo�^
        if (Magne_Parent.Count != 0)
        {
            foreach (Transform mg_child in Magne_Parent)
            {
                if (mg_child == null) { continue; }

                foreach (Transform child in mg_child)
                {
                    if (child == null) { continue; }

                    //�e�I�u�W�F�N�g�̏ꍇ�̓X�L�b�v
                    if (child.name == mg_child.name) { continue; }

                    //���擾
                    MagnetUpdateData magnetUpData = new()
                    {
                        gbMagnet = child.GetComponent<Magnet>(),
                        gbRid = child.GetComponent<Rigidbody2D>(),
                        gbtag = child.tag
                    };

                    //�擾���s
                    if(magnetUpData.gbMagnet==null||
                        magnetUpData.gbRid == null)
                    {
                        Debug.Log("�G���[");
                        continue;
                    }
                    gbMagnet.Add(magnetUpData);
                }
            }
        }

        if (Magne_ojt.Count != 0)
        {
            foreach (Transform child in Magne_ojt)
            {
                if (child == null) {  continue; }

                //���擾
                MagnetUpdateData magnetUpData = new()
                {
                    gbMagnet = child.GetComponent<Magnet>(),
                    gbRid = child.GetComponent<Rigidbody2D>(),
                    gbtag = child.tag
                };

                //�擾���s
                if (magnetUpData.gbMagnet == null ||
                    magnetUpData.gbRid == null)
                {
                    continue;
                }
                gbMagnet.Add(magnetUpData);
            }
        }
    }

    /// <summary>
    /// �}�O�l�b�g�Ώۂ̃I�u�W�F�N�g���擾
    /// </summary>
    /// <param name="pos">�͈͂̒��S�ʒu</param>
    /// <param name="len">�͈͂̔��a</param>
    /// <param name="tag">���O�^�O</param>
    /// <returns>�Ώۂ̃I�u�W�F�N�g</returns>
    public List<MagnetUpdateData> GetSearchMagnet(Vector2 pos, float len, string[] tag=null)
    {
        List<MagnetUpdateData> Rgb = new();

        foreach (MagnetUpdateData item in gbMagnet)
        {
            //��A�N�e�B�u�͑ΏۊO
            if (!item.gbRid.gameObject.activeSelf) { continue; }

            bool hit = false;
            //�^�O����v�����ꍇ���O
            if (tag != null)
            {
                foreach(string t_itemin in tag)
                {
                    if (item.gbtag == t_itemin)
                    {
                        hit = true;
                        break;
                    }
                }
            }

            if (!hit)
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
        }


        return Rgb;
    }

}
