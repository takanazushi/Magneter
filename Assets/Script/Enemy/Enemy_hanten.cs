using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//todo:使用してないみたいです
public class Enemy_hanten : MonoBehaviour
{
    ///// <summary>
    ///// 判定内に敵か壁がある
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