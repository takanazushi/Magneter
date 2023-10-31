using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Firing_WithAim : MonoBehaviour
{
    //Prefabs�ŕ������镨������
    [SerializeField, Header("�ePrefab�����Ă�������")]
    public GameObject BulletObj;

    void Start()
    {   
        StartCoroutine(ShotCoroutine());
    }

    IEnumerator ShotCoroutine()
    {
        while (true)
        {
            // Prefab�����̉�
            Instantiate(BulletObj);

            // 1.5�b�ҋ@����
            yield return new WaitForSeconds(1.5f);
        }
    }

}
