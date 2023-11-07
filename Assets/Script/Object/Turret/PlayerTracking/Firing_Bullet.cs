using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Firing_Bullet : MonoBehaviour
{
    //Prefabs�ŕ������镨������
    [SerializeField, Header("�ePrefab�����Ă�������")]
    protected  GameObject BulletObj;

    void Start()
    {   
        StartCoroutine(ShotCoroutine());
    }

    public virtual IEnumerator ShotCoroutine()
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
