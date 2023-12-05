using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LaserRush_master : MonoBehaviour
{
    [System.Serializable]
    struct LaserRush_Data
    {
        //判定オブジェクト
        public CollisionArea CollisionArea;

        //敵配列
        public List<GameObject> LaserRush;

    }

    //起動間隔
    [SerializeField]
    float Wait_space;


    [SerializeField]
    List<LaserRush_Data> LaserRush_data;

    WaitForSeconds wait;

    private void Start()
    {
        wait = new WaitForSeconds(Wait_space);
    }

    private void Update()
    {
        for (int i = 0;i< LaserRush_data.Count; i++)
        {
            //接触した場合
            if (LaserRush_data[i].CollisionArea.Is_Colliding)
            {
                //敵移動
                StartCoroutine(StartLaserRush(i, 0));

                //フラグをリセット
                LaserRush_data[i].CollisionArea.Is_Colliding = false;
            }
        }
    }

    IEnumerator StartLaserRush(int d_i,int l_i)
    {
        //起動
        LaserRush_data[d_i].LaserRush[l_i].SetActive(true);

        yield return wait;

        //個数以上にならないように
        if (LaserRush_data[d_i].LaserRush.Count <= l_i+1)
            yield break;


        StartCoroutine(StartLaserRush(d_i, l_i + 1));

    }

}
