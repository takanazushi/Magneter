using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maglaser_Aim : MonoBehaviour
{
    public LineRenderer aimLine;

    private void Update()
    {
        //�}�E�X�̈ʒu���擾
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //�v���C���[�̈ʒu����}�E�X�̈ʒu�Ɍ������x�N�g���v�Z
        Vector3 aimDirection = (mousePosition - this.transform.position).normalized;

        //�x�N�g������p�x���擾
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        //�p�x��+90�x��]
        this.transform.rotation = Quaternion.Euler(0, 0, angle + 90f);

        //�Ə���̈ʒu��ݒ�
        //�J�n�n�_
        Vector3 linestatr = transform.position;
        linestatr.z = -1;
        aimLine.SetPosition(0, linestatr);

        //�I���n�_
        Vector3 endPosition = gameObject.transform.position + Quaternion.Euler(0, 0, angle) * Vector3.right * 2f;
        endPosition.z = -1;
        aimLine.SetPosition(1, endPosition);
        //Debug.Log(endPosition);
    }
}
