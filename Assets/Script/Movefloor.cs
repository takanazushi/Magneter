using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Movefloor : MonoBehaviour
{
    //現在地を格納する変数
    private Vector2 defaultpass;

    float a;

    void Start()
    {
        defaultpass = transform.position;
    }

    void Update()
    {
        //X座標のみ横移動                                       PingPong    速さ,    範囲
        transform.position = (new Vector2(defaultpass.x + Mathf.PingPong(Time.time * 2, 3), defaultpass.y));
    }
}
