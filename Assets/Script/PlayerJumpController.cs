using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpController : MonoBehaviour
{
    private Rigidbody2D rbody2D;

    [SerializeField]
    private float jumpForce = 450f; //�W�����v��

    private int jumpCount = 0;
    

    void Start()
    {
        rbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
        if (Input.GetKey(KeyCode.Space)&& this.jumpCount < 1)
        {
            //transform.up�ŏ�����ɑ΂��āAjumpForce�̗͂������܂��B
            this.rbody2D.AddForce(transform.up * jumpForce);
            jumpCount++;
        }
    }
    //Collider�I�u�W�F�N�g���m�������������A
    private void OnCollisionEnter2D(Collision2D other)
    {
        //���̃I�u�W�F�N�g�̖��O��Floor�̏ꍇ
        if (other.gameObject.name=="Floor")
        {
            jumpCount = 0;
        }
    }
}