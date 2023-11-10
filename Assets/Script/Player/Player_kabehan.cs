using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_kabehan : MonoBehaviour
{
    /// <summary>
    /// ”»’è“à‚É“G‚©•Ç‚ª‚ ‚é
    /// </summary>
    [HideInInspector] public bool isOn = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "kabe")
        {
            Debug.LogError("•Ç‚Æ“–‚½‚Á‚½");
            isOn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.LogWarning("•Ç‚Æ“–‚½‚Á‚Ä‚È‚¢");
        isOn = false;
    }
}
