using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_HP : MonoBehaviour
{
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

    private void OnTriggerStay2D(Collider2D collision)
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
        GameManager.instance.HP -= damage;

        //���G���ԊJ�n
        StartCoroutine(InviUpdate());

        //HP���Ȃ��Ȃ����ꍇ
        if (GameManager.instance.HP <= 0)
        {
            //���u���F���g������
            //Destroy(this.gameObject);

            //�v���C���[��HP�����Z�b�g����
            GameManager.instance.HP = GameManager.instance.RestHP;
            //todo �O��̌o�ߎ��Ԃ�ۑ�
            PlayerPrefs.SetFloat("PreviousElapsedTime", ClearTime.instance.second);

            //���݂̃V�[�����ēx�ǂݍ���
            Debug.Log("���݂̃V�[�����ēx�ǂݍ���");
            Scene activeScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(activeScene.name);
        }

        //�f�o�b�N�m�F�p
        Debug.Log("HP:" + GameManager.instance.HP);
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
