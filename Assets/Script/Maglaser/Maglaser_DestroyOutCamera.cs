using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maglaser_DestroyOutCamera : MonoBehaviour
{
    private Camera mainCamera;
    private float boundsX;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera=Camera.main;
        boundsX = CalculateCameraBoundsX();
    }

    // Update is called once per frame
    void Update()
    {
        //カメラ外に弾が出たかチェック
        if (transform.position.x > boundsX)
        {
            Destroy(gameObject);
        }
    }

    private float CalculateCameraBoundsX()
    {
        float screenAspect = (float)Screen.width / Screen.height;
        float cameraSize = mainCamera.orthographicSize;
        return screenAspect * cameraSize;
    }
}
