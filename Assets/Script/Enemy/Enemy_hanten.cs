using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//todo:Žg—p‚µ‚Ä‚È‚¢‚Ý‚½‚¢‚Å‚·
public class Enemy_hanten : MonoBehaviour
{
    ///// <summary>
    ///// ”»’è“à‚É“G‚©•Ç‚ª‚ ‚é
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