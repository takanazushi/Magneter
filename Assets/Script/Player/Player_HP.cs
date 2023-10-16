using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_HP : MonoBehaviour
{
    [SerializeField]
    public int HP;

    //無敵フラグ
    //true:ダメージ無効
    private bool inviflg = false;

    //無敵時間
    [SerializeField, Header("無敵時間"), Tooltip("単位：秒")]
    public float invi_Time;


    private void OnCollisionStay2D(Collision2D collision)
    {

        //無敵時間中は無視
        if (inviflg) { return; }

        //ダメージコンポーネントを取得
        //処理重いかも
        Damage damage = collision.gameObject.GetComponent<Damage>();

        //ある場合
        if (damage != null)
        {
            //設定されたダメージを取得してダメージを受ける
            HitDamage(damage.hit_damage);
        }

    }

    //damage:受けるダメージ
    public void HitDamage(int damage)
    {

        //ダメージを受ける
        HP -= damage;

        //無敵時間開始
        StartCoroutine(InviUpdate());

        //HPがなくなった場合
        if (HP <= 0)
        {
            //仮置き：自身を消す
            Destroy(this.gameObject);
        }

        //デバック確認用
        Debug.Log("HP:" + HP);
    }

    //無敵時間更新
    private IEnumerator InviUpdate()
    {
        //無敵フラグセット
        inviflg = true;

        //無敵時間分停止
        yield return new WaitForSeconds(invi_Time);

        //無敵フラグセット
        inviflg = false;
    }
}
