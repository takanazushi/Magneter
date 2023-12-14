using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// ���c
/// </summary>
public class crumbleFloor : MonoBehaviour
{   /// <summary>
/// �ݒu�������̏���y���W
/// </summary>
    private float CurrPos;
    [SerializeField,Header("�ϋv����(�b)")]
    private float fallTime = 2.0f;
    [SerializeField, Header("�������x")]
    private float fallSP = 0.02f;
    /// <summary>
    /// �v���C���[������Ă��鎞��
    /// </summary>
    private float CurrTime = 0.0f;
    /// <summary>
    /// ���Ƃ��t���O
    /// </summary>
    private bool fall = false;

    void Start()
    {
        CurrPos = transform.position.y;
        CurrTime = 0.0f;
        fall = false;
    }

    
    void Update()
    {//���Ԍo�߂ɂ�闎��
        if (fall)
        {
            transform.position += new Vector3(0, -fallSP, 0);
            //�ŏ��̍��W-10�܂ŗ�����Ώ���
            if (transform.position.y <= CurrPos - 10.0f)
            {
                Destroy(gameObject);
            }
        }
    }
    //�I�u�W�F�N�g���m���d�Ȃ��Ă���ԁA�p��
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name=="Player")
        {
            CurrTime += Time.deltaTime;
            //�v���C���[����肷����Ə��𗎂Ƃ�
            if (CurrTime>=fallTime)
            {
                CurrTime = fallTime;
                fall = true;
            }
        }
    }
    //�I�u�W�F�N�g���m�����ꂽ�^�C�~���O�Ŏ��s
    private void OnCollisionExit2D(Collision2D collision)
    {
        CurrTime = 0.0f;
        
    }
}
