using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Self_Destruction : MonoBehaviour
{

    Player_HP player_HP = null;
    // Start is called before the first frame update
    void Start()
    {
        player_HP=GetComponent<Player_HP>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            player_HP.HitDamage(30);
        }
    }
}
