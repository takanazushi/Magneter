using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maglaser_Bullet : MonoBehaviour
{
    //��{�N���X�ł��B�Ԓe�e�ɂ͂��ꂼ���Script�����Ă����Ă��������B

    protected Transform gunTransform;

    [SerializeField]
    protected Rigidbody2D bulletRigidBody;

    [SerializeField]
    protected float bulletSpeed = 10.0f;

    private Vector3 targetPosition; // �}�E�X�̈ʒu��ۑ����邽�߂̕ϐ�

    public virtual void BulletStart()
    {
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            gunTransform = player.transform.Find("Maglaser");
        }
        else
        {
            Debug.LogError("Player �Q�[���I�u�W�F�N�g��������܂���B");
        }
    }

    public virtual void SetTargetPosition(Vector3 position)
    {
        targetPosition = position;
    }

    public virtual void BulletUpdate()
    {
        Vector3 direction = (targetPosition - gunTransform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bulletRigidBody.velocity = Quaternion.Euler(0, 0, angle) * Vector2.right * bulletSpeed;
    }


}
