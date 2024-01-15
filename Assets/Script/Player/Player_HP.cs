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
        //ダメージを受ける
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
        //ダメージを受ける
        GameManager.instance.HP -= damage;

        //無敵時間開始
        StartCoroutine(InviUpdate());

        //HPがなくなった場合
        if (GameManager.instance.HP <= 0)
        {
            //仮置き：自身を消す
            //Destroy(this.gameObject);

            //プレイヤーのHPをリセットする
            GameManager.instance.HP = GameManager.instance.RestHP;
            //todo 前回の経過時間を保存
            PlayerPrefs.SetFloat("PreviousElapsedTime", ClearTime.instance.second);

            //現在のシーンを再度読み込む
            Debug.Log("現在のシーンを再度読み込む");
            Scene activeScene = SceneManager.GetActiveScene();

            SceneManager.LoadScene(activeScene.name);
        }

        //デバック確認用
        Debug.Log("HP:" + GameManager.instance.HP);
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
