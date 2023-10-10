using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpController : MonoBehaviour
{
    private Rigidbody2D rb;
    //�W�����v��
    [SerializeField]
    private float jumpForce = 450f;
    //�W�����v��
    private int jumpCount = 0;
    
    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
        if (Input.GetKey(KeyCode.Space)&& jumpCount < 1)
        {//transform.up�ŏ�����ɑ΂��āAjumpForce�̗͂������܂��B
            rb.AddForce(transform.up * jumpForce);
            jumpCount++;
        }
    }
    //Collider�I�u�W�F�N�g���m�������������A
    private void OnCollisionEnter2D(Collision2D other)
    { //���̃I�u�W�F�N�g�̖��O��Floor�̏ꍇ
        if (other.gameObject.name=="Floor")
        {
            jumpCount = 0;
        }
    }
   
}