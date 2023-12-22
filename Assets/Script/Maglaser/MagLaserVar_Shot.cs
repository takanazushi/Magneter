using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.ShaderData;

public class MagLaserVar_Shot : MonoBehaviour
{
    [SerializeField, Header("照準の線")]
    public LineRenderer aimLine;

    /// <summary>
    /// 判定のレイ長さ
    /// </summary>
    [SerializeField,Header("レーザーの射程")]
    private float raylen;

    [SerializeField,Header("判定するレイヤー")]
    LayerMask mask;

    [SerializeField, Header("備考"), TextArea(1, 6)]
    private string text;

    /// <summary>
    /// プレイヤー位置→マウス位置方向ベクトル
    /// </summary>
    Vector2 aimDirection;

    /// <summary>
    /// 非表示コルーチン
    /// </summary>
    private Coroutine Coline_erase = null;

    /// <summary>
    /// ヒットしたオブジェクトのマグネット
    /// </summary>
    private Magnet hitmg;

    private void Update()
    {
        //左クリック
        if (Input.GetMouseButtonDown(0))
        {
            LaserDrawRay();

            if (RayHitMag())
            {
                hitmg.SetType_Magnat(Magnet.Type_Magnet.N);
                Debug.Log("Nに変更");
            }

        }
        //右クリック
        else if (Input.GetMouseButtonDown(1))
        {
            LaserDrawRay();

            if (RayHitMag())
            {
                hitmg.SetType_Magnat(Magnet.Type_Magnet.S);
                Debug.Log("Sに変更");
            }

        }

    }

    /// <summary>
    /// レイを照射しヒットした場所がカメラ内か判定
    /// </summary>
    /// <returns>true:カメラ内でヒットした</returns>
    private bool RayHitMag()
    {
        Ray2D ray = new(transform.position, aimDirection);

        RaycastHit2D[] hit = Physics2D.RaycastAll(ray.origin, ray.direction, raylen, mask);

        if (hit.Length >= 1)
        {
            //マグネット取得
            hitmg = hit[0].rigidbody.GetComponent<Magnet>();

            foreach (var item in hit)
            {
                Debug.Log(item.transform.name);
            }

            //マグネットオブジェクトがある場合
            if (hitmg)
            {
                Vector3 viewPos = Camera.main.WorldToViewportPoint(hit[0].point);
                //ヒットした場所がカメラ内か判定
                return viewPos.x > 0 && viewPos.x < 1 && viewPos.y > 0 && viewPos.y < 1;
            }

        }

        return false;
    }

    /// <summary>
    /// レーザーを表示
    /// </summary>
    private void LaserDrawRay()
    {
        float angle;

        //マウスの位置を取得
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //プレイヤーの位置からマウスの位置に向かう方向ベクトル計算
        aimDirection = (mousePosition - this.transform.position).normalized;

        //ベクトルから角度を取得(ラジアン角)
        angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;


        Vector3[] Poss =
        {
            transform.position,
            transform.position+Quaternion.Euler(0, 0, angle)*Vector3.right*raylen
        };

        //点を設定
        aimLine.SetPositions(Poss);

        //線非表示コルーチン開始
        Coline_erase ??= StartCoroutine(LaserErase());
    }

    /// <summary>
    /// 線を非表示にする
    /// </summary>
    /// <returns></returns>
    IEnumerator LaserErase()
    {
        yield return new WaitForSeconds(2.0f);

        Vector3[] Poss =
        {
            new (0,0,0),
            new (0,0,0),
        };

        //初期化
        aimLine.SetPositions(Poss);
        Coline_erase = null;
    }

}
