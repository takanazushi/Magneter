using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UIElements;

public class Maglaser_Aim : MonoBehaviour
{
    [SerializeField, Header("�Ə��̐�")]
    public LineRenderer aimLine;

    [SerializeField,Header("�e��")]
    private Transform tgetopoint;

    /// <summary>
    /// ���g��SpriteRenderer
    /// </summary>
    SpriteRenderer tgetopointSprite;

    ///�e���̊p�x
    private float mazin;

    private void Start()
    {
        //�e���̊p�x���Z�o
        Vector3 nozuru = (tgetopoint.position - transform.position).normalized;
        mazin = Mathf.Atan2(nozuru.y, nozuru.x) * Mathf.Rad2Deg;
        
        tgetopointSprite=GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        UpdateAimDirection();
    }

    /// <summary>
    /// �Ə��̕����X�V
    /// </summary>
    private void UpdateAimDirection()
    {
        //�Q�[������~���͍X�V���Ȃ�
        if (GameTimeControl.instance.IsPaused) { return; }
        //�}�E�X�̈ʒu���擾
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //�����̈ʒu����}�E�X�̈ʒu�Ɍ������x�N�g���v�Z
        Vector3 aimDirection = (mousePosition - transform.parent.position).normalized;

        //�E������łȂ��p�x
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        float kakolu = angle + mazin;

        //�e�̔��]����Ȃ��Ŋp�x��␳
        kakolu = tgetopointSprite.flipX ? kakolu + 180 : kakolu - mazin * 2;

        transform.rotation = Quaternion.Euler(0, 0, kakolu);
    }

    /// <summary>
    /// �Ə����̍X�V
    /// </summary>
    private void UpdateAimLine()
    {
        //�Ə����̈ʒu��ݒ�
        //�J�n�n�_
        Vector3 linestatr = transform.position;
        linestatr.z = -1;
        aimLine.SetPosition(0, linestatr);
        float angle = 0;
        //�I���n�_
        Vector3 endPosition = gameObject.transform.position + Quaternion.Euler(0, 0, angle) * Vector3.right * 2f;
        endPosition.z = -1;
        aimLine.SetPosition(1, endPosition);
    }
}
