using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear_Rotation : MonoBehaviour
{   //�����t���O
    [SerializeField,Header("�`�F�b�N�ō�����")]
    private bool Left=false;
    //��]���x
    [SerializeField,Header("��]���x")]
    private float RotSp = 0.01f;

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
