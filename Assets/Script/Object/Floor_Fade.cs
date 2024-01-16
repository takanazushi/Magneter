using System.Collections;
using UnityEngine;

[RequireComponent (typeof (BoxCollider2D))]
public class Floor_Fade : MonoBehaviour
{
    /// <summary>
    /// ����Ă��鎞��
    /// </summary>
    [SerializeField, Header("����Ă��鎞��")]
    private float In_Time;

    /// <summary>
    /// �����Ă��鎞��
    /// </summary>
    [SerializeField, Header("�����Ă��鎞��")]
    private float Out_Time;

    /// <summary>
    /// �����Ă������x
    /// </summary>
    [SerializeField,Header("�����鑬�x")]
    private float Out_Speed;
    
    /// <summary>
    /// �\��鑬�x
    /// </summary>
    [SerializeField,Header("����鑬�x")]
    private float In_Speed;

    [SerializeField]
    Sprite[] sprites; 
    
    /// <summary>
    /// �t�F�[�h�X�s�[�h
    /// </summary>
    float Fade_speed;

    /// <summary>
    /// ���g��SpriteRenderer
    /// </summary>
    private SpriteRenderer spRenderer;

    /// <summary>
    /// ���g��BoxCollider2D
    /// </summary>
    private BoxCollider2D boxC;

    /// <summary>
    /// �J�ڒ��t���O
    /// </summary>
    bool fadeflg;

    /// <summary>
    /// �\���ҋ@����
    /// </summary>
    WaitForSeconds In_Wait;

    /// <summary>
    /// �����ҋ@����
    /// </summary>
    WaitForSeconds Out_Wait;

    /// <summary>
    /// ����Ă��鎞�Ԓi�K�J�E���g
    /// </summary>
    int In_Time_count;

    void Start()
    {
        In_Time_count=0;
        spRenderer = GetComponent<SpriteRenderer>();
        boxC = GetComponent<BoxCollider2D>();
        spRenderer.sprite = sprites[In_Time_count];
        In_Wait = new(In_Time / sprites.Length);
        Out_Wait = new(Out_Time);

        fadeflg = false;

        //�\�����ԑҋ@
        StartCoroutine(Fade_InWait());
    }

    /*���l�͓����I��1�`0�̊Ԃ��s�������邪�A�l��������������邱�Ƃ�
            �����n�߂�܂ŁA�܂����ꏉ�߂�܂łɃ��O�𐶂������Ă���*/
    void Update()
    {
        if (fadeflg)
        {
            //�A���t�@�l��ݒ�
            SetRendereAlpha(spRenderer.color.a + Fade_speed * Time.timeScale);

            if (spRenderer.color.a < 0.0f)
            {
                //�����蔻����ꎞ�I�ɏ���
                boxC.enabled = false;

                //�J�ڏI��
                fadeflg = false;

                //�A���t�@�l��ݒ�
                SetRendereAlpha(0.0f);

                //�J�E���g������
                In_Time_count = 0;

                //�摜�ݒ�
                spRenderer.sprite = sprites[In_Time_count];

                //�������ԑҋ@
                StartCoroutine(Fade_OutWait());
            }
            else if (spRenderer.color.a >= 1)
            {
                //�A���t�@�l��ݒ�
                SetRendereAlpha(1.0f);

                //�����蔻����ꎞ����������
                boxC.enabled = true;
                //�J�ڏI��
                fadeflg = false;

                //�\�����ԑҋ@
                StartCoroutine(Fade_InWait());
            }
        }
    }

    /// <summary>
    /// �������ԕ��ҋ@
    /// </summary>
    /// <returns></returns>
    IEnumerator Fade_OutWait()
    {
        yield return Out_Wait;

        //�J�ڒ��Ɉڍs
        fadeflg = true;

        //�J�ڃX�s�[�h��ݒ�
        Fade_speed = In_Speed;
    }

    /// <summary>
    /// �\�����ԕ��ҋ@
    /// </summary>
    /// <returns></returns>
    IEnumerator Fade_InWait()
    {
        yield return In_Wait;
        In_Time_count++;

        if (In_Time_count >= sprites.Length)
        {
            //�J�ڒ��Ɉڍs
            fadeflg = true;

            //�J�ڃX�s�[�h��ݒ�
            Fade_speed = -Out_Speed;
            spRenderer.sprite = sprites[In_Time_count-1];

        }
        else
        {
            spRenderer.sprite = sprites[In_Time_count];
            StartCoroutine(Fade_InWait());
        }
    }

    /// <summary>
    /// �����̃A���t�@�l��ݒ肵�܂�
    /// </summary>
    /// <param name="a"></param>
    void SetRendereAlpha(float a)
    {
        spRenderer.color = new(spRenderer.color.r, spRenderer.color.g, spRenderer.color.b,
            a);
    }

    /// <summary>
    /// inspector�̒l���ύX���ꂽ�Ƃ�
    /// </summary>
    void OnValidate()
    {
        //�Đݒ�
        In_Wait = new(In_Time / sprites.Length);
        Out_Wait = new(Out_Time);
    }

    private void Reset()
    {
        In_Time = 3;
        Out_Time = 3;
        Out_Speed = 0.1f;
        In_Speed = 0.1f;
    }
}