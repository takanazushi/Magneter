using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_kabehan : MonoBehaviour
{
    /// <summary>
    /// ������ɓG���ǂ�����
    /// </summary>
    [HideInInspector] public bool isOn = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "kabe")
        {
            Debug.LogError("�ǂƓ�������");
            isOn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.LogWarning("�ǂƓ������ĂȂ�");
        isOn = false;
    }
}
