using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

//

//必要なコンポーネント定義
[RequireComponent(typeof(CompositeCollider2D))]


public class CameraControl : MonoBehaviour
{

    /// <summary>
    /// エリアを担当するカメラ
    /// </summary>
    [SerializeField,
        Header("担当カメラ"),
        Tooltip("エリア内を写すカメラ")]
    private CinemachineVirtualCamera m_Camera;

    /// <summary>
    /// 自分のCompositeCollider2D
    /// </summary>
    [SerializeField,
        Header("自身のCompositeCollider2D"),
        Tooltip("自動設定")]
    private CompositeCollider2D compositeCollider;

    /// <summary>
    /// デバック表示フラグ
    /// </summary>
    /// <param name="collision"></param>
    [SerializeField,
        Header("デバック表示"),
        Tooltip("ture,エリア領域表示")]
    bool m_Enable = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //注目オブジェクトの場合
        if (collision.name != m_Camera.Follow.name) { return; }

        //カメラを有効化
        m_Camera.Priority = 1;


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //注目オブジェクトの場合
        if (collision.name != m_Camera.Follow.name) { return; }

    }

    private void OnDrawGizmos()
    {
        if (!m_Enable) { return; }

        Gizmos.color= Color.red;
        Vector3 size = compositeCollider.bounds.size;

        Gizmos.DrawWireCube(transform.GetChild(0).transform.position, size);
    }

    public void Reset()
    {
        //自分のCompositeCollider2Dをセット
        compositeCollider = GetComponent<CompositeCollider2D>();
    }
}
