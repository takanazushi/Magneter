using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_HP : MonoBehaviour
{
    /// <summary>
    /// ���G�t���Otrue:�_���[�W����
    /// </summary>
    private bool inviflg = false;

    /// <summary>
    /// ���G����
    /// </summary>
    [SerializeField, Header("���G����"), Tooltip("�P�ʁF�b")]
    public float invi_Time;

    public bool Inviflg
    {
        get { return inviflg; }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        //HP�񕜃R���|�[�l���g���擾
        HP_Heal heal = collision.gameObject.GetComponent<HP_Heal>();
        if (heal)
        {
            HitHeal(heal.hit_Heal);
        }

        //�ڐG�_���[�W����
        //���G���Ԓ��͖���
        if (inviflg) { return; }

        //�_���[�W�R���|�[�l���g���擾
        Damage damage = collision.gameObject.GetComponent<Damage>();
        if (damage)
        {
            //�ݒ肳�ꂽ�_���[�W���擾���ă_���[�W���󂯂�
            HitDamage(damage.hit_damage);
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //�ڐG�_���[�W����
        //���G���Ԓ��͖���
        if (inviflg) { return; }

        //�_���[�W�R���|�[�l���g���擾
        Damage damage = collision.gameObject.GetComponent<Damage>();
        if (damage)
        {
            //�ݒ肳�ꂽ�_���[�W���擾���ă_���[�W���󂯂�
            HitDamage(damage.hit_damage);
        }
    }

    /// <summary>
    /// �񕜂𔽉f
    /// </summary>
    /// <param name="damage">�󂯂��</param>
    public void HitHeal(int damage)
    {
        //�_���[�W���󂯂�
        //todo:�ő�HP��ݒ�
        GameManager.instance.HP += damage;
        GameManager.instance.HP=Mathf.Clamp(GameManager.instance.HP, 0,3);

        //�f�o�b�N�m�F�p
        Debug.Log("HP:" + GameManager.instance.HP);
    }

    /// <summary>
    /// �_���[�W�𔽉f
    /// </summary>
    /// <param name="damage">�󂯂�_���[�W</param>
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
