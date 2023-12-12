using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Bullet : MonoBehaviour
{
    [SerializeField]
    private float Speed;

    private Vector2 direction = Vector2.left; // ’e‚ÌˆÚ“®•ûŒü

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * Speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
