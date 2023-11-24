using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_HP : MonoBehaviour
{
    [SerializeField]
    public int HP;

    //���G�t���O
    //true:�_���[�W����
    private bool inviflg = false;

    //���G����
    [SerializeField, Header("���G����"), Tooltip("�P�ʁF�b")]
    public float invi_Time;


    private void OnCollisionStay2D(Collision2D collision)
    {

        //���G���Ԓ��͖���
        if (inviflg) { return; }

        //�_���[�W�R���|�[�l���g���擾
        //�����d������
        Damage damage = collision.gameObject.GetComponent<Damage>();

        //����ꍇ
        if (damage != null)
        {
            //�ݒ肳�ꂽ�_���[�W���擾���ă_���[�W���󂯂�
            HitDamage(damage.hit_damage);
        }

    }

    //damage:�󂯂�_���[�W
    public void HitDamage(int damage)
    {

        //�_���[�W���󂯂�
        HP -= damage;

        //���G���ԊJ�n
        StartCoroutine(InviUpdate());

        //HP���Ȃ��Ȃ����ꍇ
        if (HP <= 0)
        {
            //���u���F���g������
            Destroy(this.gameObject);
        }

        //�f�o�b�N�m�F�p
        Debug.Log("HP:" + HP);
    }

    //���G���ԍX�V
    private IEnumerator InviUpdate()
    {
        //���G�t���O�Z�b�g
        inviflg = true;

        //���G���ԕ���~
        yield return new WaitForSeconds(invi_Time);

        //���G�t���O�Z�b�g
        inviflg = false;
    }
}
