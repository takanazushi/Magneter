using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UIElements;

public class Maglaser_Aim : MonoBehaviour
{
    [SerializeField, Header("照準の線")]
    public LineRenderer aimLine;

    [SerializeField,Header("銃口")]
    private Transform tgetopoint;

    /// <summary>
    /// 自身のSpriteRenderer
    /// </summary>
    SpriteRenderer tgetopointSprite;

    ///銃口の角度
    private float mazin;

    private void Start()
    {
        //銃口の角度を算出
        Vector3 nozuru = (tgetopoint.position - transform.position).normalized;
        mazin = Mathf.Atan2(nozuru.y, nozuru.x) * Mathf.Rad2Deg;
        
        tgetopointSprite=GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        UpdateAimDirection();
    }

    /// <summary>
    /// 照準の方向更新
    /// </summary>
    private void UpdateAimDirection()
    {
        //ゲームが停止中は更新しない
        if (GameTimeControl.instance.IsPaused) { return; }
        //マウスの位置を取得
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //自分の位置からマウスの位置に向かうベクトル計算
        Vector3 aimDirection = (mousePosition - transform.parent.position).normalized;

        //右方向基準でなす角度
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        float kakolu = angle + mazin;

        //銃の反転ありなしで角度を補正
        kakolu = tgetopointSprite.flipX ? kakolu + 180 : kakolu - mazin * 2;

        transform.rotation = Quaternion.Euler(0, 0, kakolu);
    }

    /// <summary>
    /// 照準線の更新
    /// </summary>
    private void UpdateAimLine()
    {
        //照準線の位置を設定
        //開始地点
        Vector3 linestatr = transform.position;
        linestatr.z = -1;
        aimLine.SetPosition(0, linestatr);
        float angle = 0;
        //終了地点
        Vector3 endPosition = gameObject.transform.position + Quaternion.Euler(0, 0, angle) * Vector3.right * 2f;
        endPosition.z = -1;
        aimLine.SetPosition(1, endPosition);
    }
}
