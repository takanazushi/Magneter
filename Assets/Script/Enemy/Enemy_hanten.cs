using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//todo:�g�p���ĂȂ��݂����ł�
public class Enemy_hanten : MonoBehaviour
{
    ///// <summary>
    ///// ������ɓG���ǂ�����
    ///// </summary>
    [HideInInspector] public bool isOn = false;

    private GameObject parent;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�Փ˂����I�u�W�F�N�g��MagnetComponent���擾
        Magnet magnet = collision.gameObject.GetComponent<Magnet>();
        //�e�I�u�W�F�N�g���擾
        parent = transform.root.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isOn)
        {
            isOn = false;
        }
    }
}