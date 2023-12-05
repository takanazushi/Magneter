
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UIElements;

public class Block_Fade : MonoBehaviour
{
    //処理実行フラグ
    private bool start=false;
    private SpriteRenderer Renderer;
    [SerializeField,Header("経過時間")]
    private float time = 0.0f;
   
    [SerializeField,Header("点滅周期")]
    private float cycle = 0.75f;
    [SerializeField,Header("消滅時間")]
    private float delTime=3.0f;

    void Start()
    {
        start = false;     
        //明滅させるためのスプライト取得
        Renderer = gameObject.GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        //明滅スタート
        if (start)
        {
            
            time += Time.deltaTime;
            //周期cycleで繰り返す値の取得
            //0～cycleの範囲の値が得られる
            var repeatValue = Mathf.Repeat(time, cycle);

            //内部時刻timeにおける明滅状態を反映
            Renderer.enabled = repeatValue >= cycle * 0.5f;
            //設定した時間の経過
            if (time >= delTime)
            {

                time = delTime;
                Destroy(gameObject);

            }
        }

    } 
   public  void SetStart()
    {
        start = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //プレイヤーが乗った
        if ( collision.gameObject.tag == "Player")
        {
            //明滅スタート(親子関係にない単一のブロックを消すため)
            SetStart();
            Damage dmg = gameObject.GetComponent<Damage>();
            if (dmg)
            {
                //親子関係の解除 スクリプトDamageの発動ができる
                
                gameObject.transform.parent = null; 

            }
           
        }
    }
}
