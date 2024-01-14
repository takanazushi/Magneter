using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StageButtonMouseOver : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Image buttonImage;

    [SerializeField]
    private Image noiseImage;

    [SerializeField]
    private Sprite nomalSprite;

    [SerializeField]
    private Sprite changeSprite;

    [SerializeField]
    private int stageNo;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GameManager.instance.stageClearFlag[stageNo - 1])
        {
            buttonImage.sprite = changeSprite;
            noiseImage.enabled = false;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (GameManager.instance.stageClearFlag[stageNo - 1])
        {
            noiseImage.enabled = true;
            buttonImage.sprite = nomalSprite;
        }
        
    }


}
