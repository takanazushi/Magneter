using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageButtonScript : MonoBehaviour
{
    [SerializeField]
    private Image[] buttonImage= new Image[4];

    [SerializeField]
    private Sprite button_StageNone;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            if (!GameManager.instance.stageClearFlag[i])
            {
                buttonImage[i].sprite = button_StageNone;
                Debug.Log("Stage" + i);
            }
        }
    }
}
