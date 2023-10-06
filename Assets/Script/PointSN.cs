using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�}�E�X�Ń}�O�l�b�g�I�u�W�F�N�g��SN��ύX����
public class PointSN : MonoBehaviour
{
    private void Update()
    {
        Magnet.Type_Magnet changeType = Magnet.Type_Magnet.Exc;

        //���N���b�N��N
        if (Input.GetMouseButton(0))
        {
            changeType= Magnet.Type_Magnet.N;
        }
        //�E�N���b�N��S
        else if (Input.GetMouseButton(1))
        {
            changeType = Magnet.Type_Magnet.S;
        }
        //���N���b�N�łȂ�
        else if (Input.GetMouseButton(2))
        {
            changeType = Magnet.Type_Magnet.None;
        }

        //�ɂ��Z�b�g���ꂽ�ꍇ
        if (changeType!=Magnet.Type_Magnet.Exc)
        {
            //�}�E�X�ʒu�Əd�Ȃ�I�u�W�F�N�g���擾
            RaycastHit2D hit =
                Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            //�I�u�W�F�N�g������ꍇ
            if (hit)
            {
                //�q�b�g�����I�u�W�F�N�g�̃}�O�l�b�g���擾
                Magnet hitObMg = hit.transform.GetComponent<Magnet>();

                //�}�O�l�b�g������ꍇ
                if (hitObMg != null)
                {
                    //�w�肵���ɂ��Z�b�g
                    hitObMg.SetType_Magnat(changeType);
                }
            }

        }        
    }

}
