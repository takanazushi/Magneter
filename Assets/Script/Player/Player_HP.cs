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

    private Animator anim = null;

    private void Start()
    {
        anim = GetComponent<Animator>();
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
        if (GameManager.instance.HP <= 0)
        {
            return;
        }

        //�_���[�W���󂯂�
        GameManager.instance.HP -= damage;

        //���G���ԊJ�n
        StartCoroutine(InviUpdate());

        //HP���Ȃ��Ȃ����ꍇ
        if (GameManager.instance.HP <= 0)
        {
            //���u���F���g������
            //Destroy(this.gameObject);

            //�V�[�����Z�b�g
            GameManager.instance.ActiveSceneReset(SceneManager.GetActiveScene().name);
            anim.SetBool("damage", true);
            //anim.Play("damage", -1, 0.1f);
            //anim.CrossFade("damage", 0.0f, 0, 0.6f);
            //anim.Play("damage",0,1.0f);
            //animator.Play("AnimationName", -1, normalizedTime);

            //�A�j���[�V������~�����Ă������J��

        }
        //�f�o�b�N�m�F�p
        //Debug.Log("HP:" + GameManager.instance.HP);
    }

    //���G���ԍX�V
    private IEnumerator InviUpdate()
    {
        //���G�t���O�Z�b�g
        inviflg = true;
        anim.SetBool("damage", true);

        //���G���ԕ���~
        yield return new WaitForSeconds(invi_Time);

        //���G�t���O�Z�b�g
        inviflg = false;
        anim.SetBool("damage", false);

    }

}
