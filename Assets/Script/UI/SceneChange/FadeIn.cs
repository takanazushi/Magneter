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
        // �A�j���[�V�����̎���
        float duration = 1.0f;

        //�A�j���[�V�����̌o�ߎ���
        float elapsedTime = 0.0f;

        //material�̐ݒ�
        image.material = _FadeIn;

        while (elapsedTime < duration)
        {
            // �����x���t�ɐݒ�
            float alpha = elapsedTime / duration;
            _FadeIn.SetFloat("_Alpha", alpha);
            yield return null;
            elapsedTime += Time.deltaTime;
        }

        image.enabled = false;

    }
}
