using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField,Header("�����X�g�̍ŏ��ƍŌ�͓����|�W�V�����ɂ��邱��")]
    private Transform[] transforms;

    //GameObject LineRender�Ŏg�p
    void Start()
    {
        //�R���|�[�l���gLineRenderer�𓾂�
        LineRenderer renderer = gameObject.GetComponent<LineRenderer>();
        // ���̕�
        renderer.SetWidth(0.1f, 0.1f);
        // ���_�̐�
        renderer.SetVertexCount(transforms.Length);
        renderer.positionCount = transforms.Length;

        for(int i = 0; i < transforms.Length; i++)
        {
            renderer.SetPosition(i, transforms[i].position);
        }

        // ���_��ݒ�
        //renderer.SetPosition(0, new Vector3(7,-3,0));
        //renderer.SetPosition(1, new Vector3(7,0,0));
        //renderer.SetPosition(2, new Vector3(-7,0,0));
        //renderer.SetPosition(3, new Vector3(-7,-3,0));
        //renderer.SetPosition(4, new Vector3(7,-3,0));
    }
}
