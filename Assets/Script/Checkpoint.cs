using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    bool collisionflg = true;
    int no;

    //�f�o�b�N�p
    private Renderer renderer;

    [SerializeField, Header("�`�F�b�N�|�C���gNo")]
    private int checkNo = 0;

    /// <summary>
    /// �J�����ԍ�
    /// </summary>
    [SerializeField, Header("�J�����ԍ�")]
    private int CameraNo;



    //�f�o�b�N�p
    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && GameManager.instance.checkpointNo <= checkNo)
        {
            GameManager.instance.checkpointNo = checkNo;
            GameManager.instance.StartCamera = CameraNo;

            //�f�o�b�N�p
            //�ʂ������
            renderer.material.color = Color.red;

            Debug.Log(GameManager.instance.checkpointNo);
        }
    }
}
        //    no = gameObject.name.IndexOf(GameManager.instance.checkpointNo.ToString());
        //if (no != -1)
        //{

        //    Debug.Log(no);

        //    if (collision.gameObject.name == "Player" && collisionflg)
        //    {
        //        string st = gameObject.name[no].ToString();
        //        GameManager.instance.checkpointNo = int.Parse(st);
        //        //int.Parse(gameObject.name[no]);
        //        collisionflg = false;
        //        Debug.Log(GameManager.instance.checkpointNo);
        //    }
        //}
    

