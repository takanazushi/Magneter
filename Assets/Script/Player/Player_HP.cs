using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_HP : MonoBehaviour
{
    /// <summary>
    /// 無敵フラグtrue:ダメージ無効
    /// </summary>
    private bool inviflg = false;

    /// <summary>
    /// 無敵時間
    /// </summary>
    [SerializeField, Header("無敵時間"), Tooltip("単位：秒")]
    public float invi_Time;

    public bool Inviflg
    {
        get { return inviflg; }
    }

    private Animator anim = null;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //HP回復コンポーネントを取得
        HP_Heal heal = collision.gameObject.GetComponent<HP_Heal>();
        if (heal)
        {
            HitHeal(heal.hit_Heal);
        }


        //接触ダメージ判定
        //無敵時間中は無視
        if (inviflg) { return; }

        //ダメージコンポーネントを取得
        Damage damage = collision.gameObject.GetComponent<Damage>();
        if (damage)
        {
            //設定されたダメージを取得してダメージを受ける
            HitDamage(damage.hit_damage);
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //接触ダメージ判定
        //無敵時間中は無視
        if (inviflg) { return; }

        //ダメージコンポーネントを取得
        Damage damage = collision.gameObject.GetComponent<Damage>();
        if (damage)
        {
            //設定されたダメージを取得してダメージを受ける
            HitDamage(damage.hit_damage);
        }
    }

    /// <summary>
    /// 回復を反映
    /// </summary>
    /// <param name="damage">受ける回復</param>
    public void HitHeal(int damage)
    {
        //todo:最大HPを設定
        GameManager.instance.HP += damage;
        GameManager.instance.HP=Mathf.Clamp(GameManager.instance.HP, 0,3);

        //デバック確認用
        Debug.Log("HP:" + GameManager.instance.HP);
    }

    /// <summary>
    /// ダメージを反映
    /// </summary>
    /// <param name="damage">受けるダメージ</param>
    public void HitDamage(int damage)
    {
        if (GameManager.instance.HP <= 0)
        {
            return;
        }

        //ダメージを受ける
        GameManager.instance.HP -= damage;

        //無敵時間開始
        StartCoroutine(InviUpdate());

        //HPがなくなった場合
        if (GameManager.instance.HP <= 0)
        {
            //仮置き：自身を消す
            //Destroy(this.gameObject);

            //シーンリセット
            GameManager.instance.ActiveSceneReset(SceneManager.GetActiveScene().name);
            anim.SetBool("damage", true);
            //anim.Play("damage", -1, 0.1f);
            //anim.CrossFade("damage", 0.0f, 0, 0.6f);
            //anim.Play("damage",0,1.0f);
            //animator.Play("AnimationName", -1, normalizedTime);

            //アニメーション停止させてもいいカモ

        }
        //デバック確認用
        //Debug.Log("HP:" + GameManager.instance.HP);
    }

    //無敵時間更新
    private IEnumerator InviUpdate()
    {
        //無敵フラグセット
        inviflg = true;
        anim.SetBool("damage", true);

        //無敵時間分停止
        yield return new WaitForSeconds(invi_Time);

        //無敵フラグセット
        inviflg = false;
        anim.SetBool("damage", false);

    }

}
