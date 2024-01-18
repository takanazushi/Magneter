using System.Collections;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
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

    [SerializeField,Header("Muzzle2のオブジェクト")]
    GameObject Muzzle;

    [SerializeField]
    Laser_Texture LasetT_R;

    [SerializeField]
    Laser_Texture LasetT_B;

    [SerializeField]
    private AudioClip ShotSE;

    [SerializeField]
    private AudioClip HitSE;

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

    /// <summary>
    /// レーザー角度
    /// </summary>
    float angle;


    GameTimeControl gameTime;
    GameManager game_manager;
    AudioSource audioSource;

    private void Start()
    {
        gameTime = GameTimeControl.instance;
        game_manager=GameManager.instance;

        audioSource=GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("MuzzleにAudioSourceついてない");
        }
    }

    private void Update()
    {
        if (gameTime.IsPaused|| !game_manager.Is_Ster_camera_end)
        {
            return;
        }

        //左クリック
        if (Input.GetMouseButtonDown(0))
        {
            audioSource.PlayOneShot(ShotSE);
            LaserDrawRay();
            LaserTexture_Shot(LasetT_R);
            if (RayHitMag())
            {
                audioSource.PlayOneShot(HitSE);
                hitmg.SetType_Magnat(Magnet.Type_Magnet.N);
            }

        }
        //右クリック
        else if (Input.GetMouseButtonDown(1))
        {
            audioSource.PlayOneShot(ShotSE);
            LaserDrawRay();
            LaserTexture_Shot(LasetT_B);
            if (RayHitMag())
            {
                audioSource.PlayOneShot(HitSE);
                hitmg.SetType_Magnat(Magnet.Type_Magnet.S);
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
            Debug.Log(hit[0].point);
            if (hit[0].rigidbody == null)
            {
                return false;
            }

            hitmg = hit[0].rigidbody.GetComponent<Magnet>();

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

        //ノズルの位置からノズル2に向かう方向ベクトル計算
        aimDirection = (this.transform.position - Muzzle.transform.position).normalized;

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

    void LaserTexture_Shot(Laser_Texture laser_Texture)
    {
        //ノズル位置に置き換え
        laser_Texture.Show(Muzzle.transform.position, angle);
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


    private void Reset()
    {
        aimLine=GetComponent<LineRenderer>();
        raylen = 15;
    }
}
