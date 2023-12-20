using UnityEngine;

public class Enemy_WalkFall : MonoBehaviour
{
    [SerializeField, Header("“G‚Ì‘¬“x")]
    private float speed = 1;

    /// <summary>
    /// ¶Œü‚«ƒtƒ‰ƒO
    /// </summary>
    [SerializeField]
    private bool Left = false;

    [Header("ÚG”»’è")]
    private Magnet magnet;

    /// <summary>
    /// ¥—Í‚Ì‰e‹¿”ÍˆÍ“à‚©?
    /// </summary>
    public bool inversionWalk = true;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        magnet=GetComponent<Magnet>();
    }

    // •¨—‰‰Z‚ğ‚µ‚½‚¢ê‡‚ÌFixedUpdate
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

        if (!magnet.inversion && !magnet.notType)     
        {
            //¶ˆÚ“®
            if (Left)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
                transform.localScale = new Vector3(1, 1, 1);
            }
            //‰EˆÚ“®
            else
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }

        //—‰ºŒã‚™‚ª-10“_‚Åíœ
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
