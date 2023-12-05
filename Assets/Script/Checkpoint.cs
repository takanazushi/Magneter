using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    bool collisionflg = true;
    int no;

    //デバック用
    private Renderer renderer;

    [SerializeField, Header("チェックポイントNo")]
    private int checkNo = 0;

    //デバック用
    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && GameManager.instance.checkpointNo <= checkNo)
        {
            GameManager.instance.checkpointNo = checkNo;

            //デバック用
            //通ったら赤
            renderer.material.color = Color.red;

            Debug.Log(GameManager.instance.checkpointNo);
        }
    }
}
        //    no = gameObject.name.IndexOf(GameManager.instance.checkpointNo.ToString());
        //if (no != -1)
        //{

        //    Debug.Log(no);

        //    if (collision.gameObject.name == "Player" && collisionflg)
        //    {
        //        string st = gameObject.name[no].ToString();
        //        GameManager.instance.checkpointNo = int.Parse(st);
        //        //int.Parse(gameObject.name[no]);
        //        collisionflg = false;
        //        Debug.Log(GameManager.instance.checkpointNo);
        //    }
        //}
    

