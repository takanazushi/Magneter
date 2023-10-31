using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionArea : MonoBehaviour
{
    [SerializeField]
    GameObject Collision_GameObject;

    public bool Is_Colliding { get; set; } = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //設定したオブジェクトの名前と同じだった場合
        if (collision.gameObject.name == Collision_GameObject.name)
        {
            Is_Colliding = true;
        }

    }
}
