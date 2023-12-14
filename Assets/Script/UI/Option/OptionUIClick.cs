using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionUIClick : MonoBehaviour
{
    [SerializeField,Header("0が右・1が左"), Range(0, 1)]
    private int type;

    [SerializeField]
    private ResolutionSetter resolutionSetter;

    /// <summary>
    /// 解像度変更ボタンを押した時の処理
    /// </summary>
    public void OnButtonDown()
    {
        if (type == 0)
        {
            resolutionSetter.IncrementCount();
        }
        else if (type == 1)
        {
            resolutionSetter.DecrementCount();
        }
    }
}
