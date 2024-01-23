using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickEffect : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameObject effectPrefab;
    private Animator animator;

    [SerializeField]
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        animator = effectPrefab.GetComponent<Animator>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // ボタンをクリックしたときの処理
        Vector2 clickPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        // エフェクトを生成
        GameObject effect = Instantiate(effectPrefab, clickPosition, Quaternion.identity, this.transform);
        // アニメーションを再生
        animator.SetTrigger("Play");
    }
}
