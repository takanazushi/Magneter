using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Laser_Texture : MonoBehaviour
{
    /// <summary>
    /// �ړ��X�s�[�h
    /// </summary>
    [SerializeField]
    float speed;

    /// <summary>
    /// �\������
    /// </summary>
    [SerializeField]
    float Show_TimeS;
    WaitForSeconds wait;

    private void Awake()
    {
        wait=new WaitForSeconds(Show_TimeS);
    }

    private void Update()
    {
        transform.position += transform.rotation*Vector2.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) 
        { 
            return; 
        }
        Debug.Log("�폜");
        gameObject.SetActive(false);
    }

    /// <summary>
    /// �\������
    /// </summary>
    /// <param name="pos">�J�n�ʒu</param>
    /// <param name="ang">�p�x</param>
    public void Show(Vector3 pos,float ang)
    {
        gameObject.SetActive(true);
        transform.SetPositionAndRotation(pos, Quaternion.Euler(0, 0, ang));

        StartCoroutine(Hide());
    }

    /// <summary>
    /// ��\���ɂ���
    /// </summary>
    /// <returns></returns>
    IEnumerator Hide()
    {
        yield return wait;
        gameObject.SetActive(false);
    }

}
