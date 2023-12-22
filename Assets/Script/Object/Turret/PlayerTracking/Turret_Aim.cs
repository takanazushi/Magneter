using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Aim : MonoBehaviour
{
    [SerializeField, Header("�^���b�g�ɕt�����Ă���e���̃g�����X�t�H�[��")]
    private Transform arrowTrans; // �������I�u�W�F�N�g�̃g�����X�t�H�[��

    [SerializeField, Header("�ڕW�ɂȂ�I�u�W�F�N�g�̃g�����X�t�H�[��")]
    private Transform ballTrans; // �^�[�Q�b�g�̃I�u�W�F�N�g�̃g�����X�t�H�[��

    [SerializeField, Header("���̍ő��]�p�x")]
    private float maxRotation;

    [SerializeField, Header("���̍Œ��]�p�x")]
    private float minRotation;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        //�J�������W�擾
        Vector3 viewPos = mainCamera.WorldToViewportPoint(transform.position);

        //todo
        // ���������������v�Z
        Vector3 dir = (ballTrans.position - arrowTrans.position);

        //�p�x�ɕϊ�
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

        //����
        angle = Mathf.Clamp(angle, minRotation, maxRotation);

        // ��]��K�p
        transform.rotation = Quaternion.AngleAxis(angle,Vector3.forward);
    }
}
