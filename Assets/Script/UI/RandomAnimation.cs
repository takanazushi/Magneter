using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimation : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(PlayAnimation());
    }

    IEnumerator PlayAnimation()
    {
        // �������[�v
        while (true)
        {
            // �����_���ȊԊu��҂�
            yield return new WaitForSeconds(Random.Range(1.0f, 5.0f));

            // �A�j���[�V�������Đ�����
            animator.SetTrigger("PlayAnimation");
        }
    }
}