using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear_Rotation : MonoBehaviour
{   //�����t���O
    [SerializeField]
    private bool Left=false;
    //��]���x
    [SerializeField]
    private float RotSp=0.01f;
    void Start()
    {
        
    }

   
    void Update()
    {   //�E���
        if (!Left)
        {
            transform.eulerAngles += new Vector3(0, 0, -RotSp);
        }
        //�����
        else
        {
            transform.eulerAngles += new Vector3(0, 0, RotSp);
        }
    }
}
