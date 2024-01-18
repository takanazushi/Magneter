using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour, IChangeRoutine
{
    [SerializeField]
    private Material _FadeOut;

    [SerializeField]
    private UnityEvent onFadeInComplete;

    [SerializeField]
    private GameObject image;

    private Image imgCompornent;

    // Start is called before the first frame update
    void Start()
    {
        imgCompornent = image.GetComponent<Image>();

        _FadeOut.SetFloat("_Alpha", 0.0f);

        image.SetActive(false);
    }

    public IEnumerator Execute(string sceneName)
    {
        image.SetActive(true);

        // �A�j���[�V�����̎���
        float duration = 1.0f;

        //�A�j���[�V�����̌o�ߎ���
        float elapsedTime = 0.0f;

        //material�̐ݒ�
        imgCompornent.material = _FadeOut;

        //�A�j���[�V�������I������܂Ń��[�v
        while (elapsedTime < duration)
        {
            float alpha = elapsedTime / duration;
            _FadeOut.SetFloat("_Alpha", alpha);
            yield return null;
            elapsedTime += Time.deltaTime;
        }

        //�A���t�@�l��1��
        _FadeOut.SetFloat("_Alpha", 1.0f);

        // �Ó]�������ɃC�x���g���Ăяo��
        if (onFadeInComplete != null)
        {
            // �V�[���J�ڏ������J�n�i���]�̊J�n�O�Ɂj
            SceneManager.LoadScene(sceneName);
        }
    }

}
