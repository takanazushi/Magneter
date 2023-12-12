using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCamera : MonoBehaviour
{
    public Transform playerTransform;
    private Vector3 offset;
    private float initialX;
    private float initialY;

    // Start is called before the first frame update
    void Start()
    {
        // プレイヤーとカメラの初期位置差を保存
        offset = transform.position - playerTransform.position;

        // カメラの初期x座標を保存
        initialX = transform.position.x;

        // カメラの初期y座標を保存
        initialY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーの位置にオフセットを加えてカメラの位置を更新
        Vector3 newPosition = playerTransform.position + offset;

        // x座標が初期位置よりも小さくならないように制限
        if (newPosition.x < initialX)
        {
            newPosition.x = initialX;
        }

        // y座標が初期位置よりも低くならないように制限
        if (newPosition.y < initialY)
        {
            newPosition.y = initialY;
        }

        transform.position = newPosition;
    }
}
