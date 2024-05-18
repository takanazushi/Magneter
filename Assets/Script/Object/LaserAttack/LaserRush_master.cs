using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LaserRush_master : MonoBehaviour
{
    [System.Serializable]
    struct LaserRush_Data
    {
        //����I�u�W�F�N�g
        public CollisionArea CollisionArea;

        //�G�z��
        public List<GameObject> LaserRush;

    }

    //�N���Ԋu
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
            //�ڐG�����ꍇ
            if (LaserRush_data[i].CollisionArea.Is_Colliding)
            {
                //�G�ړ�
                StartCoroutine(StartLaserRush(i, 0));

                //�t���O�����Z�b�g
                LaserRush_data[i].CollisionArea.Is_Colliding = false;
            }
        }
    }

    IEnumerator StartLaserRush(int d_i,int l_i)
    {
        //�N��
        LaserRush_data[d_i].LaserRush[l_i].SetActive(true);

        yield return wait;

        //���ȏ�ɂȂ�Ȃ��悤��
        if (LaserRush_data[d_i].LaserRush.Count <= l_i+1)
            yield break;


        StartCoroutine(StartLaserRush(d_i, l_i + 1));

    }

}
