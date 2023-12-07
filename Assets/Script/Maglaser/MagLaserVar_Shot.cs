using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.ShaderData;

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

    [SerializeField, Header("���l"), TextArea(1, 6)]
    private string text;

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

    private void Update()
    {
        //���N���b�N
        if (Input.GetMouseButtonDown(0))
        {
            LaserDrawRay();

            if (RayHitMag())
            {
                hitmg.SetType_Magnat(Magnet.Type_Magnet.N);
                Debug.Log("N�ɕύX");
            }

        }
        //�E�N���b�N
        else if (Input.GetMouseButtonDown(1))
        {
            LaserDrawRay();

            if (RayHitMag())
            {
                hitmg.SetType_Magnat(Magnet.Type_Magnet.S);
                Debug.Log("S�ɕύX");
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
            hitmg = hit[0].rigidbody.GetComponent<Magnet>();

            foreach (var item in hit)
            {
                Debug.Log(item.transform.name);
            }

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
        float angle;

        //�}�E�X�̈ʒu���擾
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //�v���C���[�̈ʒu����}�E�X�̈ʒu�Ɍ����������x�N�g���v�Z
        aimDirection = (mousePosition - this.transform.position).normalized;

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

}
