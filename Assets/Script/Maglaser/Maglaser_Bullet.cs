using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maglaser_Bullet : MonoBehaviour
{
    //��{�N���X�ł��B�Ԓe�e�ɂ͂��ꂼ���Script�����Ă����Ă��������B

    [SerializeField]
    protected float speed = 10f;

    // Update is called once per frame
    public virtual void  BulletUpdate()
    {
        //�e��O�i������
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    public virtual void Fire()
    {
        //
    }

   
}
