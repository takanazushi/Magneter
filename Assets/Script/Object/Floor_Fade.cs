using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Floor_Fade : MonoBehaviour
{   
    
    //���l
    [SerializeField,Header("���l")]
    private float cloa = 0.0f;
    
    //�����Ă������x
    [SerializeField,Header("�����Ă������x")]
    private float fadeSp = 0.01f;
    
    //�\��鑬�x
    [SerializeField,Header("�\��鑬�x")]
    private float EmergeSp = 0.01f;
    
    //����Ă��鎞��
    [SerializeField,Header("����Ă��鎞��")]
    private float EmergeT = 3.0f;

    //�����Ă��鎞��
    [SerializeField,Header("�����Ă��鎞��")]
    private float FadeT = 10.0f;


    private SpriteRenderer spRenderer;
    private BoxCollider2D boxC;


    void Start()
    {
        spRenderer = GetComponent<SpriteRenderer>();
        boxC = GetComponent<BoxCollider2D>();
    }

    /*���l�͓����I��1�`0�̊Ԃ��s�������邪�A�l��������������邱�Ƃ�
            �����n�߂�܂ŁA�܂����ꏉ�߂�܂łɃ��O�𐶂������Ă���*/
    void Update()
    {   //�\�ꂽ�Ƃ�
        if (boxC.enabled == true)
        {
            //�����͂��߂�
            cloa = EmergeT;
            StartCoroutine(Fade());

        }
        //�������Ƃ�
        else if (boxC.enabled == false)
        {   //����͂��߂�
            cloa = -FadeT;
            StartCoroutine(Emerge());

            return;
        }


    }
    /*Fade
     *�����Ă����ۂ�0�܂Ŕ��肪�����Ă���
     * 
     *Emerge
     *���S�ɕ\���܂�(1.0)�܂œ����蔻��͂Ȃ�
     *�܂�A0����1�Ɉڍs����܂ł͓����蔻��͂Ȃ�
     */
    IEnumerator Fade()
    {
        while (cloa > 0f)
        {   //���l�����炷
            cloa -= fadeSp;
            spRenderer.color = new Color(spRenderer.color.r, spRenderer.color.g, spRenderer.color.b, cloa);
            //1�t���[���X�V����    
            yield return null;

        }

        if (cloa < 0.0f)
        {
            //�����蔻����ꎞ�I�ɏ���
            boxC.enabled = false;


        }

    }

    IEnumerator Emerge()
    {

        while (cloa < 1f)
        {//���l�𑝂₷
            cloa += EmergeSp;
            spRenderer.color = new Color(spRenderer.color.r, spRenderer.color.g, spRenderer.color.b, cloa);
            //1�t���[���X�V���� 
            yield return null;


        }

        if (cloa >= 1)
        {
            cloa = 1f;
            //�����蔻����ꎞ����������
            boxC.enabled = true;

        }



    }


}