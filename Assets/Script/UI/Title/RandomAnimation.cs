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
        // 無限ループ
        while (true)
        {
            // ランダムな間隔を待つ
            yield return new WaitForSeconds(Random.Range(1.0f, 5.0f));

            // アニメーションを再生する
            animator.SetTrigger("PlayAnimation");
        }
    }
}