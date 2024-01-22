using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Pause_ButtonMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Image buttonImage;

    [SerializeField]
    private Sprite nomalSprite;

    [SerializeField]
    private Sprite changeSprite;

    private void Start()
    {
     
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonImage.sprite = changeSprite;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.sprite = nomalSprite;
    }
}
