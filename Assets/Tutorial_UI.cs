using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_UI : MonoBehaviour
{
    [SerializeField, Header("�\���e�L�X�g�I�u�W�F�N�g")]
    private GameObject Text;

    [SerializeField, Header("�ʉ߃t���O")]
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
