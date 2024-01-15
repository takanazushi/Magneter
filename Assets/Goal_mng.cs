using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_mng : MonoBehaviour
{
    public static Goal_mng instance;

    [SerializeField,Header("リザルト移動するまでの待機時間（秒）")]
    private float LoadWait;

    /// <summary>
    /// ゴール済フラグ
    /// true:ゴール済
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

        //todo:リザルトシーンに移動
        Debug.Log("リザルトシーンに移動");
    }

}
