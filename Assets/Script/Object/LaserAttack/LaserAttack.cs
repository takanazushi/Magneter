using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class LaserAttack : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;

    [SerializeField]
    private Rigidbody2D Rigidbody2D;

    [SerializeField]
    private float Seed;
    [SerializeField]
    private float MaxSeed;

    private float startValue = 2.0f;//�J�n
    private float endValue = 0.5f;//�I��
    private float lerpDuration = 1.5f; // �⊮�ɂ����鎞��

    private float currentTime = 0.0f;//�o�ߎ���

    //�J�n�n�_�ۑ�
    private Vector3 StartPos;

    enum Mode{
        Wait,           //�ҋ@���
        Move,           //�ړ���
        Delete          //�폜
    }
    Mode mode = Mode.Wait;

    private void Start()
    {
        //�U���J�n

        Vector3[] positions = new Vector3[]{
            this.transform.position,               // �J�n�_�i�����j
            this.transform.position + transform.up * 50,             // �I���_
        };

        //���C���̑����ݒ�
        lineRenderer.startWidth = startValue;
        lineRenderer.endWidth = startValue;
        //���C���`��
        lineRenderer.SetPositions(positions);

        //�ҋ@���
        mode = Mode.Wait;

        //�J�n�n�_�ۑ�
        StartPos = transform.position;
    }

    private void Update()
    {
        switch (mode) { 
        
            case Mode.Wait:
                {
                    //���[�U�[���ׂ�
                    LaserStart();
                }
                break;

            case Mode.Move:
                {
                    //�I�u�W�F�N�g�̑O������
                    Vector2 force = transform.up * Seed;

                    //����
                    Rigidbody2D.AddForce(force);

                    //�ő呬�x�ȏ�ɂȂ�Ȃ��悤��
                    if (Rigidbody2D.velocity.magnitude > MaxSeed)
                    {
                        Rigidbody2D.velocity = Rigidbody2D.velocity.normalized * MaxSeed;
                    }
                }
                break;

            case Mode.Delete:
                {
                    //���[�U�[���Ȃ���
                    LaserEnd();
                }
                break;
        
        }
    }

    private void LaserEnd()
    {
        //�o�ߎ��Ԃ��X�V
        currentTime += Time.deltaTime;

        //�l��⊮
        float lerpValue = Mathf.Lerp(endValue, 0, currentTime / lerpDuration);

        //���C���̑�����ύX
        lineRenderer.startWidth = lerpValue;
        lineRenderer.endWidth = lerpValue;

        //�⊮������������l���Œ�
        if (currentTime >= lerpDuration)
        {
            //���������ď����_�ɖ߂�
            currentTime = 0;
            transform.position = StartPos;
            Start();  
            this.gameObject.SetActive(false);
        }
    }

    private void LaserStart()
    {
        //�o�ߎ��Ԃ��X�V
        currentTime += Time.deltaTime;

        //�l��⊮
        float lerpValue = Mathf.Lerp(startValue, endValue, currentTime / lerpDuration);

        //�⊮������������l���Œ�
        if (currentTime >= lerpDuration)
        {
            lerpValue = endValue;

            //�ړ����Ƀ��[�h�ύX
            mode= Mode.Move;

            //�폜�R���[�`���J�n
            StartCoroutine(My_Delete());

            //���Z�b�g
            currentTime = 0;
        }

        //���C���̑�����ύX
        lineRenderer.startWidth = lerpValue;
        lineRenderer.endWidth = lerpValue;

    }

    IEnumerator My_Delete()
    {
        //���b��
        yield return new WaitForSeconds(3);

        //�폜���[�h�ɕύX
        mode = Mode.Delete;

    }

}
