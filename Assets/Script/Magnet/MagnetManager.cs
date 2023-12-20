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

    [SerializeField]
    List<Transform> Magne_Parent;

    //[SerializeField]
    //private GameObject MagnetS;

    //[SerializeField]
    //private GameObject MagnetN;

    //[SerializeField]
    //private GameObject MagnetNon;

    ////��������G�I�u�W�F�N�g
    //[SerializeField]
    //private GameObject EnemyMagnet;

    //[SerializeField]
    //private GameObject EnemyMagnetN;

    //[SerializeField]
    //private GameObject EnemyMagnetNon;

    private void Awake()
    {
        //�q�I�u�W�F�N�g��Rigidbody2D,Magnet,tag���擾�����X�g�ɓo�^
        if (Magne_Parent.Count != 0)
        {
            foreach (Transform mg_child in Magne_Parent)
            {
                if (mg_child == null) { Debug.Log("mag__Parent"); continue; }

                foreach (Transform child in mg_child.GetComponentsInChildren<Transform>())
                {
                    if (child == null) { Debug.Log("mg_child"); continue; }

                    GameObject childObject = child.gameObject;

                    //�e�I�u�W�F�N�g�̏ꍇ�̓X�L�b�v
                    if (childObject.name == mg_child.name) { continue; }

                    //���擾
                    MagnetUpdateData magnetUpData = new()
                    {
                        gbMagnet = childObject.GetComponent<Magnet>(),
                        gbRid = childObject.GetComponent<Rigidbody2D>(),
                        gbtag = childObject.tag
                    };

                    //�擾���s
                    if(magnetUpData.gbMagnet==null||
                        magnetUpData.gbRid == null)
                    {
                        continue;
                    }

                    gbMagnet.Add(magnetUpData);
                }




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
