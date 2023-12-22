using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetectionCanceled : MonoBehaviour
{
    [SerializeField,Header("コライダーを無効にする時間")]
    private float disableTime = 0.1f;

    //コライダーの参照
    private Collider2D collider2;

    private void Awake()
    {
        //コライダー取得
        collider2 = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        //コライダー無効化処理開始
        StartCoroutine(DisableCollision());
    }

    /// <summary>
    /// コライダー一瞬だけ無効化処理
    /// </summary>
    /// <returns>なし</returns>
    private IEnumerator DisableCollision()
    {
        collider2.enabled = false;
        yield return new WaitForSeconds(disableTime);
        collider2.enabled = true;
    }
}
