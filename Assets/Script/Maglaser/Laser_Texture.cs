using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Laser_Texture : MonoBehaviour
{
    /// <summary>
    /// 移動スピード
    /// </summary>
    [SerializeField]
    float speed;

    /// <summary>
    /// 表示時間
    /// </summary>
    [SerializeField]
    float Show_TimeS;
    WaitForSeconds wait;

    private void Awake()
    {
        wait=new WaitForSeconds(Show_TimeS);
    }

    private void Update()
    {
        transform.position += transform.rotation*Vector2.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) 
        { 
            return; 
        }
        Debug.Log("削除");
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 表示する
    /// </summary>
    /// <param name="pos">開始位置</param>
    /// <param name="ang">角度</param>
    public void Show(Vector3 pos,float ang)
    {
        gameObject.SetActive(true);
        transform.SetPositionAndRotation(pos, Quaternion.Euler(0, 0, ang));

        StartCoroutine(Hide());
    }

    /// <summary>
    /// 非表示にする
    /// </summary>
    /// <returns></returns>
    IEnumerator Hide()
    {
        yield return wait;
        gameObject.SetActive(false);
    }

}
