using System.Collections;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class MagLaserVar_Shot : MonoBehaviour
{
    [SerializeField, Header("�Ə��̐�")]
    public LineRenderer aimLine;

    /// <summary>
    /// ����̃��C����
    /// </summary>
    [SerializeField,Header("���[�U�[�̎˒�")]
    private float raylen;

    [SerializeField,Header("���肷�郌�C���[")]
    LayerMask mask;

    [SerializeField,Header("Muzzle2�̃I�u�W�F�N�g")]
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
    /// �v���C���[�ʒu���}�E�X�ʒu�����x�N�g��
    /// </summary>
    Vector2 aimDirection;

    /// <summary>
    /// ��\���R���[�`��
    /// </summary>
    private Coroutine Coline_erase = null;

    /// <summary>
    /// �q�b�g�����I�u�W�F�N�g�̃}�O�l�b�g
    /// </summary>
    private Magnet hitmg;

    /// <summary>
    /// ���[�U�[�p�x
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
            Debug.LogError("Muzzle��AudioSource���ĂȂ�");
        }
    }

    private void Update()
    {
        if (gameTime.IsPaused|| !game_manager.Is_Ster_camera_end)
        {
            return;
        }

        //���N���b�N
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
        //�E�N���b�N
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
    /// ���C���Ǝ˂��q�b�g�����ꏊ���J������������
    /// </summary>
    /// <returns>true:�J�������Ńq�b�g����</returns>
    private bool RayHitMag()
    {
        Ray2D ray = new(transform.position, aimDirection);

        RaycastHit2D[] hit = Physics2D.RaycastAll(ray.origin, ray.direction, raylen, mask);

        if (hit.Length >= 1)
        {
            //�}�O�l�b�g�擾
            Debug.Log(hit[0].point);
            if (hit[0].rigidbody == null)
            {
                return false;
            }

            hitmg = hit[0].rigidbody.GetComponent<Magnet>();

            //�}�O�l�b�g�I�u�W�F�N�g������ꍇ
            if (hitmg)
            {
                Vector3 viewPos = Camera.main.WorldToViewportPoint(hit[0].point);
                //�q�b�g�����ꏊ���J������������
                return viewPos.x > 0 && viewPos.x < 1 && viewPos.y > 0 && viewPos.y < 1;
            }

        }

        return false;
    }

    /// <summary>
    /// ���[�U�[��\��
    /// </summary>
    private void LaserDrawRay()
    {

        //�m�Y���̈ʒu����m�Y��2�Ɍ����������x�N�g���v�Z
        aimDirection = (this.transform.position - Muzzle.transform.position).normalized;

        //�x�N�g������p�x���擾(���W�A���p)
        angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        Vector3[] Poss =
        {
            transform.position,
            transform.position+Quaternion.Euler(0, 0, angle)*Vector3.right*raylen
        };

        //�_��ݒ�
        aimLine.SetPositions(Poss);

        //����\���R���[�`���J�n
        Coline_erase ??= StartCoroutine(LaserErase());
    }

    void LaserTexture_Shot(Laser_Texture laser_Texture)
    {
        //�m�Y���ʒu�ɒu������
        laser_Texture.Show(Muzzle.transform.position, angle);
    }

    /// <summary>
    /// �����\���ɂ���
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

        //������
        aimLine.SetPositions(Poss);
        Coline_erase = null;
    }


    private void Reset()
    {
        aimLine=GetComponent<LineRenderer>();
        raylen = 15;
    }
}
