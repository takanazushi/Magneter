using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimation : MonoBehaviour
{
    public Animator animator; // �A�j���[�^�[�ւ̎Q�Ƃ�ێ����܂��B
    public float minDelay = 1f; // �ŏ��x������
    public float maxDelay = 5f; // �ő�x������

    // �X�^�[�g���ɃR���[�`�����J�n���܂��B
    void Start()
    {
        StartCoroutine(PlayAnimation());
    }

    // �����_���ȊԊu�ŃA�j���[�V�������Đ�����R���[�`��
    IEnumerator PlayAnimation()
    {
        while (true)
        {
            // �����_���Ȓx�����Ԃ𐶐����܂��B
            float delay = Random.Range(minDelay, maxDelay);

            // �x�����Ԃ����ҋ@���܂��B
            yield return new WaitForSeconds(delay);

            // �A�j���[�V�������Đ����܂��B
            animator.Play("TitleAnimation", -1, 0f);
        }
    }
}
