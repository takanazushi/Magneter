using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Movefloor : MonoBehaviour
{
    //Œ»İ’n‚ğŠi”[‚·‚é•Ï”
    private Vector2 defaultpass;

    float a;

    void Start()
    {
        defaultpass = transform.position;
    }

    void Update()
    {
        //XÀ•W‚Ì‚İ‰¡ˆÚ“®                                       PingPong    ‘¬‚³,    ”ÍˆÍ
        transform.position = (new Vector2(defaultpass.x + Mathf.PingPong(Time.time * 2, 3), defaultpass.y));
    }
}
