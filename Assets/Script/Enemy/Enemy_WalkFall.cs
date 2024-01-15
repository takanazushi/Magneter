using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static Magnet;

///<summary>
/// �ڐG�������ɔ��]����G
/// </summary>
public class Enemy_WalkFall : MonoBehaviour
{
    /// <summary>
    /// �ړ����x
    /// </summary>
    [SerializeField, Header("�ړ����x")]
    private float Speed;

    /// <summary>
    /// ���͒�R���E�l
    /// </summary>
    [SerializeField, Header("���͒�R���E�l")
        , Tooltip("���̒l�ȏ�̗͂��󂯂�Ɣ��]���܂�")]
    float X_Resist;

    /// <summary>
    /// �������t���O
    /// </summary>
    [SerializeField]
    private bool Left;

    /// <summary>
    /// �A�j���[�^�[
    /// </summary>
    [SerializeField]
    private Animator animator;


    /// <summary>
    /// Light2D
    /// </summary>
    [SerializeField]
    private Light2D light2D;


    /// <summary>
    /// �G�p�}�O�l�b�g
    /// </summary>
    [SerializeField, HideInInspector]
    private Magnet magnet;

    //���g��Rigidbody2D
    [SerializeField, HideInInspector]
    private Rigidbody2D rb;

    private void Reset()
    {
        rb = GetComponent<Rigidbody2D>();
        magnet = GetComponent<Magnet>();
        Speed = 1;
        X_Resist = 0.3f;

        
    }

    private void Start()
    {
        if (!Left)
        {
            transform.localScale = new
                (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }

        MagnetColorChange();
    }

    private void Update()
    {
        MagnetColorChange();

    }

    // �������Z���������ꍇ��FixedUpdate
    void FixedUpdate()
    {
        Vector2 mg_speed = Vector2.zero;

        //���͂̉e������ꍇ
        if (magnet)
        {
            if(magnet.PuroTypeManet!= Type_Magnet.None&&
                magnet.PuroTypeManet != Type_Magnet.Exc)
            {
                //���O�Ɏ��g�̃^�O��ݒ�
                mg_speed = magnet.Magnet_Power(new string[] { tag });

                //�������Ɉ��ȏ�̗͂��󂯂��ꍇ
                //todo:��R�l��ݒ肵�ĕ��ʏ�Ȃ�_�C�W���u�ł����A
                //���������C��t�^�ȂǂŁA��R�l���z����ꏊ�ɒ����؍݂���ꍇ�u���u�������Ⴄ
                if (Mathf.Abs(mg_speed.x) >= Mathf.Abs(X_Resist))
                {
                    //���E���]
                    Turn();
                }
            }
        }

        float X_speed;
        //���ړ�
        if (Left)
        {
            X_speed = -Speed;

        }
        //�E�ړ�
        else
        {
            X_speed = Speed;

        }

        //�G�̈ړ��l
        Vector2 total = new(X_speed, rb.velocity.y);

        //���̗͂͂����Z
        total += mg_speed;
        rb.velocity = total;

        //�����゙��-10���_�ō폜
        if (transform.position.y < -10)
        {
            //todo:�v�C��
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Enemy�^�O�����I�u�W�F�N�g�Ƃ̓����蔻��𖳎�
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
            return;
        }

        //���Ƃ̏����͖���
        if (collision.gameObject.name == "Floor")
        {
            return;
        }


        Turn();
        

    }

    /// <summary>
    /// ���E���]
    /// </summary>
    private void Turn()
    {
        Left = !Left;
        transform.localScale = new
            (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    private void SetAnimation(string animationName)
    {
        //�A�j���[�V�������Đ�
        animator.Play(animationName);
    }

    private void MagnetColorChange()
    {
        if(light2D.enabled == false)
        {
            light2D.enabled = true;
        }

        if (magnet.PuroTypeManet == Type_Magnet.S)
        {
            //Debug.Log("S�ɂł�");
            SetAnimation("Enemy_Blue_walk");
            light2D.color = Color.blue;

        }
        else if (magnet.PuroTypeManet == Type_Magnet.N)
        {
            //Debug.Log("N�ɂł�");
            SetAnimation("Enemy_Red_walk");
            light2D.color = Color.red;
        }
        else if (magnet.PuroTypeManet == Type_Magnet.None)
        {
            //Debug.Log("�Ɏw�肵�ĂȂ��ł�");
            SetAnimation("Enemy_None_walk");
            light2D.enabled = false;
        }
    }
}
