using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCamera : MonoBehaviour
{
    public Transform playerTransform;
    private Vector3 offset;
    private float initialX;
    private float initialY;

    // Start is called before the first frame update
    void Start()
    {
        // �v���C���[�ƃJ�����̏����ʒu����ۑ�
        offset = transform.position - playerTransform.position;

        // �J�����̏���x���W��ۑ�
        initialX = transform.position.x;

        // �J�����̏���y���W��ۑ�
        initialY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // �v���C���[�̈ʒu�ɃI�t�Z�b�g�������ăJ�����̈ʒu���X�V
        Vector3 newPosition = playerTransform.position + offset;

        // x���W�������ʒu�����������Ȃ�Ȃ��悤�ɐ���
        if (newPosition.x < initialX)
        {
            newPosition.x = initialX;
        }

        // y���W�������ʒu�����Ⴍ�Ȃ�Ȃ��悤�ɐ���
        if (newPosition.y < initialY)
        {
            newPosition.y = initialY;
        }

        transform.position = newPosition;
    }
}
