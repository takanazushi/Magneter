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

        // アニメーションの時間
        float duration = 1.0f;

        //アニメーションの経過時間
        float elapsedTime = 0.0f;

        //materialの設定
        imgCompornent.material = _FadeOut;

        //アニメーションが終了するまでループ
        while (elapsedTime < duration)
        {
            float alpha = elapsedTime / duration;
            _FadeOut.SetFloat("_Alpha", alpha);
            yield return null;
            elapsedTime += Time.deltaTime;
        }

        //アルファ値を1に
        _FadeOut.SetFloat("_Alpha", 1.0f);

        // 暗転完了時にイベントを呼び出す
        if (onFadeInComplete != null)
        {
            // シーン遷移処理を開始（明転の開始前に）
            SceneManager.LoadScene(sceneName);
        }
    }

}
