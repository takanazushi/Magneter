using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraStart : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCamera[] cameras;

    void Start()
    {
        cameras[GameManager.instance.StartCamera].Priority = 1;
    }


}
