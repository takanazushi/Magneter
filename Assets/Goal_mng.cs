using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_mng : MonoBehaviour
{
    public static Goal_mng instance;

    bool Goalflg;
    public bool Is_Goal
    {
        get { return Goalflg; }
        set { Goalflg = value; }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            //�C���X�^���X�����݂���ꍇ�͔j��
            Destroy(gameObject);
        }
    }

}
