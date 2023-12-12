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
            //インスタンスが存在する場合は破棄
            Destroy(gameObject);
        }
    }

}
