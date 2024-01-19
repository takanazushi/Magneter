using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{

    
    private Block_Fade[] fades;
    
    private bool flg;

    void Start()
    {
        //子のBlock_Fadeスクリプトを取得
        fades = gameObject.GetComponentsInChildren<Block_Fade>();
        flg = false;
        
            }

   
    void Update()
    {//プレイヤーが乗った
        foreach (Block_Fade fade in fades)
        {
            if (flg)
            {
                fade.SetStart();
            }
        }
        

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {//乗ったのがプレイヤー
        if (collision.gameObject.tag == "Player")
        {
            flg = true;
        }
    }
}
