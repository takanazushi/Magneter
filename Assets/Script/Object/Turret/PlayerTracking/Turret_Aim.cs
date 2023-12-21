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

    private void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, 90);
    }

    private void Update()
    {
        //todo
        // ���������������v�Z
        Vector3 dir = (ballTrans.position - arrowTrans.position);

        // ��]�𐧌�����
        float angle = Vector3.Angle(Vector3.right, dir);
        float sign = Mathf.Sign(Vector3.Cross(Vector3.right, dir).y);
        angle = Mathf.Clamp(angle * sign, minRotation, maxRotation);

        // ��]��K�p
        transform.rotation = Quaternion.Euler(0, 0, -angle);
    }
}
