using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_WalkFall : MonoBehaviour
{
    [SerializeField]
    private float speed=1;

    [SerializeField]
    private bool Left=false;//¶Œü‚«
    
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // •¨—‰‰Z‚ğ‚µ‚½‚¢ê‡‚ÌFixedUpdate
    void FixedUpdate()
    {
        
        //‰EˆÚ“®
        if (!Left)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        //¶ˆÚ“®
        
        else 
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        
        //—‰ºŒã‚™‚ª-10“_‚Åíœ
        if (transform.position.y<-10) { 
        Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //•Ç‚Æ‚Ô‚Â‚©‚Á‚½‚Æ‚«‚É‹t‚ÉˆÚ“®
        if (collision.gameObject.CompareTag("Wall"))
        {
            Left = (Left == true) ? false : true;
        }
    }

}
