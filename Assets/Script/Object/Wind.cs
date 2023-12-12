using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    [HideInInspector]
    public static Wind instance;


    [SerializeField, Header("左に進むスピード")]
    private float windMoveLeftSpeed = 0.5f;

    [SerializeField, Header("右に進むスピード")]
    private float windMoveRightSpeed = 1.5f;

    [SerializeField, Header("風の出現時間")]
    private float onTime = 10f;

    [SerializeField, Header("風のクールタイム")]
    private float outTime = 10f;

    [SerializeField, Header("風の状態")]
    private bool windTimeflg = true;

    //風に当たっている状態の移動速度
    private float movespeed = 0;

    public float getMoveSpeed
    {
        get { return movespeed; }
    }

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        movespeed = 1;

        //デバック 風オブジェクトの色変更
        gameObject.GetComponent<Renderer>().material.color = Color.red;

        //風オン状態のタイマースタート
        StartCoroutine(Loop(onTime));
    }

    private IEnumerator Loop(float second)
    {
        Debug.Log(windTimeflg);

        if (windTimeflg)
        {
            yield return new WaitForSeconds(second);
            windTimeflg = false;
            //風オフ状態のタイマースタート
            StartCoroutine(Loop(outTime));

            //デバック用
            gameObject.GetComponent<Renderer>().material.color = Color.blue;
            Debug.Log(windTimeflg);
        }
        else
        {
            yield return new WaitForSeconds(second);
            windTimeflg = true;
            StartCoroutine(Loop(onTime));

            //デバッグ
            gameObject.GetComponent<Renderer>().material.color = Color.red;
            Debug.Log(windTimeflg);
        }
    }

    

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (windTimeflg)
            {
                //右に進むか、左に進んでいるかのキー入力の取得
                float horizontalInput = Input.GetAxis("Horizontal");

                //右
                if (horizontalInput > 0f)
                {
                    movespeed = windMoveRightSpeed;
                }
                //左
                else if (horizontalInput < 0f)
                {
                    movespeed = windMoveLeftSpeed;
                }
                else
                {
                    movespeed = windMoveLeftSpeed;
                }
            }
            else
            {
                movespeed = 0;
            }

        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            movespeed = 0;

            //デバッグ
            Debug.Log("離" + movespeed);
        }
    }
}

