using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_FollowerBullet : MonoBehaviour
{
    //ƒvƒŒƒCƒ„[‚ÌÀ•WŠi”[
    private Transform target;

    [SerializeField, Header("’e‚ÌÅ‘å‰ñ“]Šp“x")]
    private float maxRotation;

    [SerializeField, Header("’e‚ÌÅ’á‰ñ“]Šp“x")]
    private float minRotation;

    //ˆÚ“®‘¬“x
    [SerializeField, Header("’e‚Ì”­ËƒXƒs[ƒh")]
    private float moveSpeed;

    [SerializeField]
    private AudioClip ShotSE;

    //‰æ–Ê“à
    private bool InField = false;

    private Rigidbody2D rb;

    private Vector3 bulletDirection;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("Battery_FollowerBullet‚ÉAudioSource‚Â‚¢‚Ä‚È‚¢");
        }

        target = GameObject.FindGameObjectWithTag("Player").transform;

        Vector3 dir = (target.position - transform.position);

        //Šp“x‚É•ÏŠ·
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

        //§ŒÀ
        angle = Mathf.Clamp(angle, minRotation, maxRotation);

        // ‰ñ“]‚ğ“K—p
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // ’e‚Ì•ûŒü‚ğ©‹@‚©‚ç“G‚ÖŒü‚¯‚é
        bulletDirection = target.position - transform.position;

        //todo ’e‚Ì”­ËŠp“x‚Ì§ŒÀ
        if(bulletDirection.y > -5)
        {
            bulletDirection.y = -5;
        }

    }

    //‰æ–Ê“à‚Å“®‚©‚·
    private void OnBecameVisible()
    {
        //‰æ–Ê“à‚É‚¢‚é‚Æ‚«true
        audioSource.PlayOneShot(ShotSE);

        InField = true;
        Debug.Log("ƒJƒƒ‰”ÍˆÍ“à");
    }

    //‰æ–ÊŠO‚ÅÁ‚·ˆ—
    private void OnBecameInvisible()
    {
        //Á‚·
        Debug.Log("ƒJƒƒ‰”ÍˆÍŠO");
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.Is_Ster_camera_end)
        {
            Destroy(rb.gameObject);
            return;
        }
        //‰æ–Ê“à‚ÅÀs
        else if (InField)
        {
            //Œü‚«ŒÅ’è
            bulletDirection.Normalize();

            //‚»‚ÌŒü‚«‚ÉƒXƒs[ƒh‚ğŠ|‚¯‚é
            rb.velocity = bulletDirection * moveSpeed;

            //10•bŒã‚É–C’e‚ğ”j‰ó‚·‚é
            Destroy(gameObject, 5.0f);
        }   
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Destroy (gameObject);
    }
}
