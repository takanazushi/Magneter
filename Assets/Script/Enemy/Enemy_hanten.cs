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
    private bool isOn = false;

    public bool GetIsOn
    {
        get { return isOn; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
          isOn = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isOn)
        {
            isOn = false;
        }
    }
}