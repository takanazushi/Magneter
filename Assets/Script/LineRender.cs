using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    //GameObject LineRender�Ŏg�p
    void Start()
    {
        //�R���|�[�l���gLineRenderer�𓾂�
        LineRenderer renderer = gameObject.GetComponent<LineRenderer>();
        // ���̕�
        renderer.SetWidth(0.1f, 0.1f);
        // ���_�̐�
        renderer.SetVertexCount(5);
        renderer.positionCount = 5;
        // ���_��ݒ�
        renderer.SetPosition(0, new Vector3(7,-3,0));
        renderer.SetPosition(1, new Vector3(7,0,0));
        renderer.SetPosition(2, new Vector3(-7,0,0));
        renderer.SetPosition(3, new Vector3(-7,-3,0));
        renderer.SetPosition(4, new Vector3(7,-3,0));
    }
}
