using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image uiText; // UIテキストへの参照を保持します。

    private void Start()
    {
        uiText.enabled = false;
    }

    // ボタンの上にカーソルが合わさったときに呼び出されます。
    public void OnPointerEnter(PointerEventData eventData)
    {
        uiText.enabled = true; // UIテキストを表示します。
    }

    // ボタンからカーソルが離れたときに呼び出されます。
    public void OnPointerExit(PointerEventData eventData)
    {
        uiText.enabled = false; // UIテキストを非表示にします。
    }
}
