using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maglaser_Aim : MonoBehaviour
{
    //�Ə��p��LineRenderer
    public LineRenderer aimLine;

    //�e�̃g�����X�t�H�[��
    public Transform gunTransform;

    public Player_Direction p_direction;

    // Update is called once per frame
    void Update()
    {
        //�}�E�X�̈ʒu���擾����
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //�e�̈ʒu����}�E�X�̈ʒu�֌������x�N�g���̌v�Z
        Vector2 direction = mousePosition - (Vector2)gunTransform.position;
        direction.Normalize();

        //�e�̌�����ݒ�
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        angle = Mathf.Clamp(angle, -20f, 20f);

        gunTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //�Ə��̐���`��
        aimLine.SetPosition(0,gunTransform.position);

        Vector3 endPosition = gunTransform.position + Quaternion.Euler(0, 0, angle) * Vector3.right * 2f;

        aimLine.SetPosition(1, endPosition);
    }
}
