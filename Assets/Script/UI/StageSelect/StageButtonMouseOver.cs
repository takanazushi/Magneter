using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private Image StageImage;

    [SerializeField]
    private int stageNo;

    Button button;

    private void Start()
    {
        button = GetComponent<Button>();

        if (!GameManager.instance.stageClearFlag[stageNo])
        {
            button.interactable = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GameManager.instance.stageClearFlag[stageNo])
        {
            buttonImage.sprite = changeSprite;
            noiseImage.enabled = false;
            StageImage.enabled = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (GameManager.instance.stageClearFlag[stageNo])
        {
            buttonImage.sprite = nomalSprite;

            StageImage.enabled = false;

            noiseImage.enabled = true;
            
        }
    }



}
