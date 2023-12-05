using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_hanten : MonoBehaviour
{
    //���͂������������Ă���Ƃ��̔��]����

    /// <summary>
    /// ������ɓG���ǂ�����
    /// </summary>
    [HideInInspector] public bool isOn = false;

    private GameObject parent;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�Փ˂����I�u�W�F�N�g��MagnetComponent���擾
        Magnet magnet = collision.gameObject.GetComponent<Magnet>();
        //�e�I�u�W�F�N�g���擾
        parent = transform.root.gameObject;
        //�e�̃}�O�l�b�g�����ƏՓ˂����}�O�l�b�g�̑������ꏏ�̏ꍇ
        if (parent.gameObject.GetComponent<Magnet>().Gat_Magnet() == magnet.Gat_Magnet())
        {
            isOn = true;
        }
        else //if(parent.gameObject.GetComponent<Magnet>().Gat_Magnet() != magnet.Gat_Magnet())
        {
            isOn = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isOn)
        {
            isOn = false;
        }
    }
}