using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField, Header("����t���O"),
        Tooltip("���񂷂�ꍇ�`�F�b�N")]
    bool Loop;


    private Transform[] transforms;

    //GameObject LineRender�Ŏg�p
    //void Start()
    //{
    //    int num = 0;

    //    //�R���|�[�l���gLineRenderer�𓾂�
    //    LineRenderer renderer = gameObject.GetComponent<LineRenderer>();
    //    // ���̕�
    //    renderer.SetWidth(0.1f, 0.1f);

    //    int len;
    //    if (Loop)
    //    {
    //        len = transforms.Length+1;
    //    }
    //    else
    //    {
    //        len = transforms.Length;
    //    }

    //    // ���_�̐�
    //    renderer.positionCount = len;

    //    for(int i = 0; i < transforms.Length; i++)
    //    {
    //        renderer.SetPosition(i, transforms[i].position);
    //    }

    //    //����̏ꍇ�̓��[�v������
    //    if (Loop)
    //    {
    //        renderer.SetPosition(transforms.Length, transforms[0].position);
    //    }

    //}
}
