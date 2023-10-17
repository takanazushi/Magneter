using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Direction : MonoBehaviour
{
    private Transform playerTransform;
    private bool RightFlag; 

    public bool Rightflag
    {
        get { return RightFlag; }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePosition.x < playerTransform.position.x)
        {
            playerTransform.localScale = new Vector3(-1, 1, 1);
            RightFlag = true;
        }
        else
        {
            playerTransform.localScale = new Vector3(1, 1, 1);
            RightFlag = false;
        }
    }
}
