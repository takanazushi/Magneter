using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_kabehan : MonoBehaviour
{
    /// <summary>
    /// 判定内に敵か壁がある
    /// </summary>
    [HideInInspector] public bool isOn = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "kabe")
        {
            //Debug.LogError("壁と当たった");
        }
        isOn = true;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.LogWarning("壁と当たってない");
        isOn = false;
    }
}
