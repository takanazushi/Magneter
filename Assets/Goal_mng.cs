using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_mng : MonoBehaviour
{
    public static Goal_mng instance;

    [SerializeField,Header("���U���g�ړ�����܂ł̑ҋ@���ԁi�b�j")]
    private float LoadWait;

    /// <summary>
    /// �S�[���σt���O
    /// true:�S�[����
    /// </summary>
    bool Goalflg;

    WaitForSeconds wait;

    public bool Is_Goal
    {
        get { return Goalflg; }
        set { Goalflg = value; }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        wait=new WaitForSeconds(LoadWait);

    }

    
    public void ResultStart()
    {
        Goalflg = true;
        StartCoroutine(ResultLoad());
    }

    IEnumerator ResultLoad()
    {
        yield return wait;

        //todo:���U���g�V�[���Ɉړ�
        Debug.Log("���U���g�V�[���Ɉړ�");
    }

}
