using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMoveFloor : MonoBehaviour
{
    //GameObject LineMoveFloor�Ŏg�p
    //GameObject LineMoveFloorRigitBody�̃{�f�B�^�C�v���L�l�}�e�B�b�N�ɕύX���Ă܂�
    //�L�l�}�e�B�b�N�ɕύX���邱�Ƃŏd�͂̉e���𖳂���
    //GameObject LineMoveFloor��PlatformEffector2D�����Ă܂�
    //PlatformEffector2D�œ����蔻����ゾ���Ɍ��肵�Ă܂�

    //�N���XLineRenderer�̏�������ϐ�
    [SerializeField]
    private LineRenderer lineRenderer;
    //�������̃X�s�[�h
    [SerializeField]
    private float speed;
    //���_�̐�
    private int currentIndex;
    private Rigidbody2D rb;

    //�O�̒l������ϐ�
    private Vector2 oldpos = Vector2.zero;
    //�i�񂾋���������ϐ�
    private Vector2 newpos = Vector2.zero;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //�����ʒu
        oldpos = rb.position;
        currentIndex = 0;
    }

    //Player���Ői�񂾋����𓾂邽�߂̊֐�
    public Vector2 GetVelocity()
    {
        return newpos;
    }

    void Update()
    {
        if (lineRenderer.positionCount > 0)
        {
            //�|�W�V����(���_�̍��W)�ݒ�
            Vector3 targetPosition = lineRenderer.GetPosition(currentIndex);
            
            //���݂̈ʒu����ڕW�̈ʒu�܂ł̎��̃t���[���̈ʒu���v�Z
            Vector2 moveTowards = Vector2.MoveTowards(rb.position, targetPosition,speed * Time.deltaTime);

            // ��Ԃ��g�p���ăX���[�Y�ȑ��x�ω�������
            Vector2 smoothVelocity = Vector2.Lerp(rb.velocity, (moveTowards - rb.position) / Time.deltaTime, 0.1f);

            //���x���
            rb.velocity = smoothVelocity;
            //�i�񂾋���
            newpos = smoothVelocity;
            //�O�̈ʒu
            oldpos = rb.position;
            //�ڕW�ʒu�ɋ߂Â����玟�̒��_�𓾂�
            if (Vector3.Distance(rb.position, targetPosition) < 0.1f)
            {
                currentIndex = (currentIndex + 1) % lineRenderer.positionCount;
            }
        }
    }
}
