using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField,Header("※リストの最初と最後は同じポジションにすること")]
    private Transform[] transforms;

    //GameObject LineRenderで使用
    void Start()
    {
        //コンポーネントLineRendererを得る
        LineRenderer renderer = gameObject.GetComponent<LineRenderer>();
        // 線の幅
        renderer.SetWidth(0.1f, 0.1f);
        // 頂点の数
        renderer.SetVertexCount(transforms.Length);
        renderer.positionCount = transforms.Length;

        for(int i = 0; i < transforms.Length; i++)
        {
            renderer.SetPosition(i, transforms[i].position);
        }

        // 頂点を設定
        //renderer.SetPosition(0, new Vector3(7,-3,0));
        //renderer.SetPosition(1, new Vector3(7,0,0));
        //renderer.SetPosition(2, new Vector3(-7,0,0));
        //renderer.SetPosition(3, new Vector3(-7,-3,0));
        //renderer.SetPosition(4, new Vector3(7,-3,0));
    }
}
