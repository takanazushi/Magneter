using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maglaser_Bullet : MonoBehaviour
{
    //��{�N���X�ł��B�Ԓe�e�ɂ͂��ꂼ���Script�����Ă����Ă��������B

    [SerializeField]
    protected float speed = -10f;

    [SerializeField]
    protected Rigidbody2D rb;
    Vector2 direction;

    [SerializeField]
    protected Transform gunTransform;

    // Update is called once per frame
    public virtual void  BulletUpdate()
    {
        //�e��O�i������
        //transform.Translate(Vector2.right * speed * Time.deltaTime);
        rb.velocity = direction * speed;
    }

    public virtual void Fire()
    {
        //�}�E�X�̈ʒu���擾����
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //�e�̈ʒu����}�E�X�̈ʒu�֌������x�N�g���̌v�Z
        direction = mousePosition - (Vector2)gunTransform.position;
        direction.Normalize();

        //�e�̌�����ݒ�
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        angle = Mathf.Clamp(angle, -20f, 20f);

        gunTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        rb.velocity = direction * speed;
    }

   
}
