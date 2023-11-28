using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maglaser_DestroyOutCamera : MonoBehaviour
{
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        CheckOutOfView();
    }

    private void CheckOutOfView()
    {
        Vector3 viewPos = mainCamera.WorldToViewportPoint(transform.position);

        // オブジェクトがビューの外に出たら破棄
        if (viewPos.x < 0 || viewPos.x > 1 || viewPos.y < 0 || viewPos.y > 1)
        {
            Destroy(gameObject);
        }
    }
}
