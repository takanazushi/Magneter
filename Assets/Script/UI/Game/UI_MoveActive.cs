using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Events;

public class UI_MoveActive : MonoBehaviour
{
    
    RectTransform rectTransform;
    [SerializeField,Header("移動速度")]
    float move;

    WaitForSeconds wait;

    /// <summary>
    /// 行動時間
    /// </summary>
    [SerializeField]
    float time;

    /// <summary>
    /// 更新中フラグtrue:更新中
    /// </summary>
    bool Actionflg;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        //更新中のみ
        if (!Actionflg) { return; }

        //移動値分移動
        rectTransform.position += new Vector3(0, move * Time.timeScale, 0);
    }

    /// <summary>
    /// 行動開始
    /// </summary>
    /// <param name="callback">行動を終了コールバック</param>
    public void ActionStart(UnityAction callback)
    {
        Actionflg=true;
        wait = new(time);
        StartCoroutine(ActionEnd(callback));
    }

    /// <summary>
    /// 行動終了
    /// </summary>
    /// <param name="callback">コールバック</param>
    /// <returns></returns>
    IEnumerator ActionEnd(UnityAction callback)
    {
        yield return wait;
        //オブジェクトを非活性
        gameObject.SetActive(false);
        callback();
    }
}
