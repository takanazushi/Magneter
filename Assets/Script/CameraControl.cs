using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

//

//�K�v�ȃR���|�[�l���g��`
[RequireComponent(typeof(CompositeCollider2D))]


public class CameraControl : MonoBehaviour
{

    /// <summary>
    /// �G���A��S������J����
    /// </summary>
    [SerializeField,
        Header("�S���J����"),
        Tooltip("�G���A�����ʂ��J����")]
    private CinemachineVirtualCamera m_Camera;

    /// <summary>
    /// ������CompositeCollider2D
    /// </summary>
    [SerializeField,
        Header("���g��CompositeCollider2D"),
        Tooltip("�����ݒ�")]
    private CompositeCollider2D compositeCollider;

    /// <summary>
    /// �f�o�b�N�\���t���O
    /// </summary>
    /// <param name="collision"></param>
    [SerializeField,
        Header("�f�o�b�N�\��"),
        Tooltip("ture,�G���A�̈�\��")]
    bool m_Enable = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //���ڃI�u�W�F�N�g�̏ꍇ
        if (collision.name != m_Camera.Follow.name) { return; }

        //�J������L����
        m_Camera.Priority = 1;


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //���ڃI�u�W�F�N�g�̏ꍇ
        if (collision.name != m_Camera.Follow.name) { return; }

    }

    private void OnDrawGizmos()
    {
        if (!m_Enable) { return; }

        Gizmos.color= Color.red;
        Vector3 size = compositeCollider.bounds.size;

        Gizmos.DrawWireCube(transform.GetChild(0).transform.position, size);
    }

    public void Reset()
    {
        //������CompositeCollider2D���Z�b�g
        compositeCollider = GetComponent<CompositeCollider2D>();
    }
}