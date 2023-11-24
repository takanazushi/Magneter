using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear_Rotation : MonoBehaviour
{   //左回りフラグ
    [SerializeField,Header("チェックで左回りに")]
    private bool Left=false;
    //回転速度
    [SerializeField,Header("回転速度")]
    private float RotSp = 0.01f;

    void Update()
    {   //右回り
        if (!Left)
        {
            transform.eulerAngles += new Vector3(0, 0, -RotSp);
        }
        //左回り
        else
        {
            transform.eulerAngles += new Vector3(0, 0, RotSp);
        }
    }
}
