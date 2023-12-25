using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Texture : MonoBehaviour
{
    [SerializeField]
    float speed;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        transform.position += transform.rotation*Vector2.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) { return; }

        gameObject.SetActive(false);
    }

    public void Show(Vector3 pos,float ang)
    {

        gameObject.SetActive(true);
        transform.SetPositionAndRotation(pos, Quaternion.Euler(0, 0, ang));
    }
}
