using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Purchasing;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class fixed2 : MonoBehaviour
{
    private Rigidbody2D rb;

    //画面内
    private bool InField = false;

    //Xスピード
    private float SpeedX;

    //Yスピード
    private float SpeedY;

    //向き
    public bool Direction = false;

    //左斜め
    public bool LeftDiazional = false;

    //右斜め
    public bool RightDiazional = false;

    //Prefabsで複製する物を入れる(今回の場合Circle)
    public GameObject BulletObj;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("Shot", 1.0f, 5.0f);
    }

    //画面外で消す処理
    private void OnBecameInvisible()
    {
        //消す
        Destroy(rb.gameObject);
    }

    //画面内で動かす
    private void OnBecameVisible()
    {
        InField = true;
    }

    ////当たり判定
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    
    //}

    void Shot()
    {
        if(InField)
        {
            //複製処理
            GameObject obj = Instantiate(BulletObj, transform.position, Quaternion.identity);
            //名前をCircleにする
            obj.name = BulletObj.name;
            
            //左向き
            if (Direction)
            {
                SpeedX = -5;
            }
            //右向き
            else
            {
                SpeedX = 5;
            }

            // 弾速は自由に設定
            rb.velocity = new Vector2(rb.velocity.x + SpeedX, rb.velocity.y + SpeedY);
            //8秒後に砲弾を破壊する
            Destroy(obj, 8.0f);
        }   
    }
}
