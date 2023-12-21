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
        while (true)
        {
            //�J�������W�擾
            Vector3 viewPos = mainCamera.WorldToViewportPoint(transform.position);

            // todo �J�����͈͓�(�e���v���C���[�ɓ͂�����)�Ő���
            if (viewPos.x > 0.25 && viewPos.x < 1)
            {
                //todo ���W�擾
                BulletObj.transform.position = transform.position;
                //����
                Instantiate(BulletObj);
            }

            // 2.0�b�ҋ@����
            yield return new WaitForSeconds(2.0f);
        }
    }


}
