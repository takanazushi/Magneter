using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// �X�e�[�W�J�n�����o�p
/// </summary>
public class UI_MoveActive : MonoBehaviour
{
    
    RectTransform rectTransform;
    [SerializeField,Header("�ړ����x")]
    float move;

    WaitForSeconds wait;

    /// <summary>
    /// �s������
    /// </summary>
    [SerializeField]
    float time;

    /// <summary>
    /// �X�V���t���Otrue:�X�V��
    /// </summary>
    bool Actionflg;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        if (GameManager.instance.Is_Ster_camera_end)
        {
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        //�X�V���̂�
        if (!Actionflg) { return; }

        //�ړ��l���ړ�
        rectTransform.position += new Vector3(0, move * Time.timeScale, 0);
    }

    /// <summary>
    /// �s���J�n
    /// </summary>
    /// <param name="callback">�s�����I���R�[���o�b�N</param>
    public void ActionStart(UnityAction callback)
    {
        Actionflg=true;
        wait = new(time);
        StartCoroutine(ActionEnd(callback));
    }

    /// <summary>
    /// �s���I��
    /// </summary>
    /// <param name="callback">�R�[���o�b�N</param>
    /// <returns></returns>
    IEnumerator ActionEnd(UnityAction callback)
    {
        yield return wait;
        //�I�u�W�F�N�g��񊈐�
        gameObject.SetActive(false);
        callback();
    }
}
