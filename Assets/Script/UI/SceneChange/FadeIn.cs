using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    [SerializeField]
    private Material _FadeIn;

    private Image image;

    private void Start()
    {
        _FadeIn.SetFloat("_Alpha", 0.0f);
        image = GetComponent<Image>();
        StartCoroutine(AnimateTransitionOut());
    }

    IEnumerator AnimateTransitionOut()
    {
        // アニメーションの時間
        float duration = 1.0f;

        //アニメーションの経過時間
        float elapsedTime = 0.0f;

        //materialの設定
        image.material = _FadeIn;

        while (elapsedTime < duration)
        {
            // 透明度を逆に設定
            float alpha = elapsedTime / duration;
            _FadeIn.SetFloat("_Alpha", alpha);
            yield return null;
            elapsedTime += Time.deltaTime;
        }

        image.enabled = false;

    }
}
