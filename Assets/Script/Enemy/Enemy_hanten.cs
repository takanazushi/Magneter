using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_hanten : MonoBehaviour
{
    /// <summary>
    /// ”»’è“à‚É“G‚©•Ç‚ª‚ ‚é
    /// </summary>
    [HideInInspector] public bool isOn = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("“–‚½‚Á‚½");

        if (collision.gameObject.tag == "Enemy")
        {
           // Debug.Log("“G‚Æ“–‚½‚Á‚½");
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