using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimation : MonoBehaviour
{
    public Animator animator; // アニメーターへの参照を保持します。
    public float minDelay = 1f; // 最小遅延時間
    public float maxDelay = 5f; // 最大遅延時間

    // スタート時にコルーチンを開始します。
    void Start()
    {
        StartCoroutine(PlayAnimation());
    }

    // ランダムな間隔でアニメーションを再生するコルーチン
    IEnumerator PlayAnimation()
    {
        while (true)
        {
            // ランダムな遅延時間を生成します。
            float delay = Random.Range(minDelay, maxDelay);

            // 遅延時間だけ待機します。
            yield return new WaitForSeconds(delay);

            // アニメーションを再生します。
            animator.Play("TitleAnimation", -1, 0f);
        }
    }
}
