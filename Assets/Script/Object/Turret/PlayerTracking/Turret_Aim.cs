using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Aim : MonoBehaviour
{
    [SerializeField, Header("�^���b�g�ɕt�����Ă���e���̃g�����X�t�H�[��")]
    private Transform arrowTrans; // �������I�u�W�F�N�g�̃g�����X�t�H�[��

    [SerializeField, Header("�ڕW�ɂȂ�I�u�W�F�N�g�̃g�����X�t�H�[��")]
    private Transform ballTrans; // �^�[�Q�b�g�̃I�u�W�F�N�g�̃g�����X�t�H�[��

    private void Update()
    {
        // ���������������v�Z
        Vector3 dir = (ballTrans.position - arrowTrans.position);

        // �����Ō������������ɉ�]�����Ă܂�
        arrowTrans.rotation = Quaternion.FromToRotation(new Vector3(1,0,0), dir);
    }
}
