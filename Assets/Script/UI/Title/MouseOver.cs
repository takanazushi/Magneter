using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image uiText; // UI�e�L�X�g�ւ̎Q�Ƃ�ێ����܂��B

    private void Start()
    {
        uiText.enabled = false;
    }

    // �{�^���̏�ɃJ�[�\�������킳�����Ƃ��ɌĂяo����܂��B
    public void OnPointerEnter(PointerEventData eventData)
    {
        uiText.enabled = true; // UI�e�L�X�g��\�����܂��B
    }

    // �{�^������J�[�\�������ꂽ�Ƃ��ɌĂяo����܂��B
    public void OnPointerExit(PointerEventData eventData)
    {
        uiText.enabled = false; // UI�e�L�X�g���\���ɂ��܂��B
    }
}
