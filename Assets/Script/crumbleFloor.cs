using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// 仲田
/// </summary>
public class crumbleFloor : MonoBehaviour
{   /// <summary>
/// 設置した時の床のy座標
/// </summary>
    private float CurrPos;
    [SerializeField,Header("耐久時間(秒)")]
    private float fallTime = 2.0f;
    [SerializeField, Header("落下速度")]
    private float fallSP = 0.02f;
    /// <summary>
    /// プレイヤーが乗っている時間
    /// </summary>
    private float CurrTime = 0.0f;
    /// <summary>
    /// 落とすフラグ
    /// </summary>
    private bool fall = false;

    void Start()
    {
        CurrPos = transform.position.y;
        CurrTime = 0.0f;
        fall = false;
    }

    
    void Update()
    {//時間経過による落下
        if (fall)
        {
            transform.position += new Vector3(0, -fallSP, 0);
            //最初の座標-10まで落ちれば消す
            if (transform.position.y <= CurrPos - 10.0f)
            {
                Destroy(gameObject);
            }
        }
    }
    //オブジェクト同士が重なっている間、継続
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name=="Player")
        {
            CurrTime += Time.deltaTime;
            //プレイヤーが乗りすぎると床を落とす
            if (CurrTime>=fallTime)
            {
                CurrTime = fallTime;
                fall = true;
            }
        }
    }
    //オブジェクト同士が離れたタイミングで実行
    private void OnCollisionExit2D(Collision2D collision)
    {
        CurrTime = 0.0f;
        
    }
}
