using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetectionCanceled : MonoBehaviour
{
    [SerializeField,Header("�R���C�_�[�𖳌��ɂ��鎞��")]
    private float disableTime = 0.1f;

    //�R���C�_�[�̎Q��
    private Collider2D collider2;

    private void Awake()
    {
        //�R���C�_�[�擾
        collider2 = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        //�R���C�_�[�����������J�n
        StartCoroutine(DisableCollision());
    }

    /// <summary>
    /// �R���C�_�[��u��������������
    /// </summary>
    /// <returns>�Ȃ�</returns>
    private IEnumerator DisableCollision()
    {
        collider2.enabled = false;
        yield return new WaitForSeconds(disableTime);
        collider2.enabled = true;
    }
}
