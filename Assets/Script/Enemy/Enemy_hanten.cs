using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_hanten : MonoBehaviour
{
    /// <summary>
    /// ������ɓG���ǂ�����
    /// </summary>
    [HideInInspector] public bool isOn = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("��������");

        if (collision.gameObject.tag == "Enemy")
        {
           // Debug.Log("�G�Ɠ�������");
        }
        else
        {
            isOn = true;
        }
        

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        isOn = false;

    }
}