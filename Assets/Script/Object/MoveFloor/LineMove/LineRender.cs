using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField, Header("巡回フラグ"),
        Tooltip("巡回する場合チェック")]
    bool Loop;


    private Transform[] transforms;

    //GameObject LineRenderで使用
    //void Start()
    //{
    //    int num = 0;

    //    //コンポーネントLineRendererを得る
    //    LineRenderer renderer = gameObject.GetComponent<LineRenderer>();
    //    // 線の幅
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

    //    // 頂点の数
    //    renderer.positionCount = len;

    //    for(int i = 0; i < transforms.Length; i++)
    //    {
    //        renderer.SetPosition(i, transforms[i].position);
    //    }

    //    //巡回の場合はループさせる
    //    if (Loop)
    //    {
    //        renderer.SetPosition(transforms.Length, transforms[0].position);
    //    }

    //}
}
