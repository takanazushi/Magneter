
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UIElements;

public class Block_Fade : MonoBehaviour
{
    //�������s�t���O
    private bool start=false;
    private SpriteRenderer Renderer;
    [SerializeField,Header("�o�ߎ���")]
    private float time = 0.0f;
   
    [SerializeField,Header("�_�Ŏ���")]
    private float cycle = 0.75f;
    [SerializeField,Header("���Ŏ���")]
    private float delTime=3.0f;

    void Start()
    {
        start = false;     
        //���ł����邽�߂̃X�v���C�g�擾
        Renderer = gameObject.GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        //���ŃX�^�[�g
        if (start)
        {
            
            time += Time.deltaTime;
            //����cycle�ŌJ��Ԃ��l�̎擾
            //0�`cycle�͈̔͂̒l��������
            var repeatValue = Mathf.Repeat(time, cycle);

            //��������time�ɂ����閾�ŏ�Ԃ𔽉f
            Renderer.enabled = repeatValue >= cycle * 0.5f;
            //�ݒ肵�����Ԃ̌o��
            if (time >= delTime)
            {

                time = delTime;
                Destroy(gameObject);

            }
        }

    } 
   public  void SetStart()
    {
        start = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //�v���C���[�������
        if ( collision.gameObject.tag == "Player")
        {
            //���ŃX�^�[�g(�e�q�֌W�ɂȂ��P��̃u���b�N����������)
            SetStart();
            Damage dmg = gameObject.GetComponent<Damage>();
            if (dmg)
            {
                //�e�q�֌W�̉��� �X�N���v�gDamage�̔������ł���
                
                gameObject.transform.parent = null; 

            }
           
        }
    }
}
