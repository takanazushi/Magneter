using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Magnet;

public class Enemy_WalkFall : MonoBehaviour
{
    [SerializeField, Header("GÌ¬x")]
    private float speed = 1;

    [SerializeField]
    private bool Left = false;//¶ü«

    [Header("ÚG»è")]
    public Magnet magnet;

    //¥ÍÌe¿ÍÍà©
    public bool inversionWalk = true;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // ¨Zðµ½¢êÌFixedUpdate
    void FixedUpdate()
    {
        if (inversionWalk && magnet.inversion)
        {
            inversionWalk = false;
            Left = !Left;
        }
        else if (!magnet.inversion)
        {
            inversionWalk = true;
        }

        if (!magnet.inversion || !magnet.inversion && !magnet.notType)     
        {
            //EÚ®
            if (!Left)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
                transform.localScale = new Vector3(-1, 1, 1);
            }
            //¶Ú®
            else
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
                transform.localScale = new Vector3(1, 1, 1);
            }
        }

        //ºãª-10_Åí
        if (transform.position.y < -10)
        {
            Destroy(this.gameObject);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Left = !Left;
        if (magnet.inversion)
        {
            inversionWalk = true;
        }
    }
}
