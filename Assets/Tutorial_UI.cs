using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_UI : MonoBehaviour
{
    [SerializeField, Header("表示テキストオブジェクト")]
    private GameObject Text;

    [SerializeField, Header("通過フラグ")]
    public bool PointFlg = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            Text.SetActive(true);
            PointFlg = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Text.SetActive(false);
            PointFlg = false;
        }
    }
}
