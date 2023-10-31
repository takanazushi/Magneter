using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear_Rotation : MonoBehaviour
{   //左回りフラグ
    [SerializeField]
    private bool Left=false;
    //回転速度
    [SerializeField]
    private float RotSp=0.01f;
    void Start()
    {
        
    }

   
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
