using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{   //��]���x
    [SerializeField]
    private float RotSP=0.1f;
    //����]�t���O
    [SerializeField]
    private bool Left=false;
    void Start()
    {
       

    }

    
    void Update()
    {   //�E���
        if (!Left)
        {
            transform.eulerAngles += new Vector3(0, 0, -RotSP);
        }
        else
        {
            //�����
            transform.eulerAngles += new Vector3(0, 0, RotSP);
        }

    }
}
