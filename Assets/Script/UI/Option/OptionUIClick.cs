using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionUIClick : MonoBehaviour
{
    [SerializeField,Header("0���E�E1����"), Range(0, 1)]
    private int type;

    [SerializeField]
    private ResolutionSetter resolutionSetter;

    /// <summary>
    /// �𑜓x�ύX�{�^�������������̏���
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
