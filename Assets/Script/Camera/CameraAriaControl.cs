using Cinemachine;
using UnityEngine;

//

//必要なコンポーネント定義
[RequireComponent(typeof(CompositeCollider2D))]

public class CameraAriaControl : MonoBehaviour
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

        //スタート時のカメラ遷移中
        //プレイヤーの死亡中
        //は判定しない
        if (!GameManager.instance.Is_Ster_camera_end|| GameManager.instance.Is_Player_Death)
        {
            //Debug.Log("フラグはFalseです");
            return;
        }
        

        //注目オブジェクトの場合
        if (m_Camera.Follow != null && collision.name != m_Camera.Follow.name)
        {
            return;
        }

        //カメラを有効化
        m_Camera.Priority = 1;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //注目オブジェクトの場合
        if(m_Camera.Follow != null && collision.name != m_Camera.Follow.name)
        {
            return;
        }

        if (GameManager.instance.Is_Player_Death)
        {
            return;
        }

        //カメラを有効化
        m_Camera.Priority = 0;
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
