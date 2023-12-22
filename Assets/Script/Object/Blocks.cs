using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{

    
    private Block_Fade[] fades;
    
    private bool flg;

    void Start()
    {
        //�q��Block_Fade�X�N���v�g���擾
        fades = gameObject.GetComponentsInChildren<Block_Fade>();
        flg = false;
        
            }

   
    void Update()
    {//�v���C���[�������
        foreach (Block_Fade fade in fades)
        {
            if (flg)
            {
                fade.SetStart();
            }
        }
        

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {//������̂��v���C���[
        if (collision.gameObject.tag == "Player")
        {
            flg = true;
        }
    }
}
