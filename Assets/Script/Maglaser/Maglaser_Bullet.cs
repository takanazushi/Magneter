using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maglaser_Bullet : MonoBehaviour
{
    //��{�N���X�ł��B�Ԓe�e�ɂ͂��ꂼ���Script�����Ă����Ă��������B

    //�}�O���[�U�[�̃g�����X�t�H�[��
    protected Transform gunTransform;

    [SerializeField,Header("�e��RigidBody2D")]
    protected Rigidbody2D bulletRigidBody;

    [SerializeField,Header("�e�̑��x")]
    protected float bulletSpeed = 10.0f;

    // �}�E�X�̈ʒu��ۑ����邽�߂̕ϐ�
    private Vector3 targetPosition; 

    public virtual void BulletStart()
    {
        FindGunTranform();
    }

    public virtual void SetTargetPosition(Vector3 position)
    {
        //�e�̖ڕW�ʒu��ݒ肷��
        targetPosition = position;
    }

    public virtual void BulletUpdate()
    {
        UpdateBulletVelocity();
    }

    /// <summary>
    /// �e�̃g�����X�t�H�[���������Đݒ肷��
    /// </summary>
    protected void FindGunTranform()
    {
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            //�v���C���[�̏e�̃g�����X�t�H�[�����擾
            gunTransform = player.transform.Find("Maglaser");
        }
        else
        {
            //�G���[���b�Z�[�W��\��
            Debug.LogError("Player �Q�[���I�u�W�F�N�g��������܂���B");
        }
    }

    /// <summary>
    /// �e�̑��x���X�V����
    /// </summary>
    protected void UpdateBulletVelocity()
    {
        Vector3 direction = (targetPosition - gunTransform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bulletRigidBody.velocity = Quaternion.Euler(0, 0, angle) * Vector2.right * bulletSpeed;
    }


}
