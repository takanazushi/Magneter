using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Movefloor : MonoBehaviour
{
    //���ݒn���i�[����ϐ�
    private Vector2 defaultpass;

    float a;

    void Start()
    {
        defaultpass = transform.position;
    }

    void Update()
    {
        //X���W�̂݉��ړ�                                       PingPong    ����,    �͈�
        transform.position = (new Vector2(defaultpass.x + Mathf.PingPong(Time.time * 2, 3), defaultpass.y));
    }
}
