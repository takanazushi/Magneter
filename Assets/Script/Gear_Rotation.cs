using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{   //回転速度
    [SerializeField]
    private float RotSP=0.1f;
    //左回転フラグ
    [SerializeField]
    private bool Left=false;
    void Start()
    {
       

    }

    
    void Update()
    {   //右回り
        if (!Left)
        {
            transform.eulerAngles += new Vector3(0, 0, -RotSP);
        }
        else
        {
            //左回り
            transform.eulerAngles += new Vector3(0, 0, RotSP);
        }

    }
}
