using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Firing_Bullet : MonoBehaviour
{
    private Camera mainCamera;
    //Prefabs�ŕ������镨������
    [SerializeField, Header("�ePrefab�����Ă�������")]
     protected GameObject BulletObj;

    void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(ShotCoroutine());
    }
    public virtual IEnumerator ShotCoroutine()
    {
        if (!GameManager.instance.Is_Ster_camera_end) 
        {
            yield return new WaitForSeconds(2.0f);
        }

        while (true)
        {
            //�J�������W�擾
            Vector3 viewPos = mainCamera.WorldToViewportPoint(transform.position);

            //�J�����͈͓�(�e���v���C���[�ɓ͂�����)�Ő���
            if(viewPos.x > 0.25 && viewPos.x < 1)
            {
                //���W�擾
                BulletObj.transform.position = transform.position;
                //����
                Instantiate(BulletObj);
            }

            // 2.0�b�ҋ@����
            yield return new WaitForSeconds(3.0f);
        }
    }

}
