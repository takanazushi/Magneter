using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraStart : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCameraBase[] cameras;

    void Start()
    {
        //cameras[GameManager.instance.StartCamera].Priority = 1;
        //cameras[GameManager.instance.StartCamera].Priority = 1;
    }

    public void Camera_Set()
    {
        cameras[GameManager.instance.StartCamera].Priority = 1;
    }

}
