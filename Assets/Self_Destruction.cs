using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Self_Destruction : MonoBehaviour
{
    float keyHoldTime = 0f;
    private SpriteRenderer spriteRenderer;
    private Color originalColor = Color.white;
    private Color pressedColor = Color.red;

    Player_HP player_HP = null;

    /// <summary>
    /// 自爆実行秒数
    /// </summary>
    float maxHoldTime=3.0f;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player_HP=GetComponent<Player_HP>();

        // 初期色を設定
        originalColor = spriteRenderer.color;

    }

    // Update is called once per frame
    void Update()
    {
        if(Goal_mng.instance.Is_Goal && !GameManager.instance.Is_Ster_camera_end)
        {
            return;
        }

        if (Input.GetKey(KeyCode.G))
        {
            //経過時間測定
            keyHoldTime += Time.deltaTime;
        }
        else
        {
            keyHoldTime -= Time.deltaTime;
            keyHoldTime=Mathf.Clamp01(keyHoldTime);
        }


        if (keyHoldTime >= maxHoldTime)
        {
            {
                keyHoldTime = 0;
                player_HP.HitDamage(30);
                Debug.Log("自爆");
            }

        }
        ChangeColor(pressedColor);
    }

    private void ChangeColor(Color color)
    {
        // スプライトの色を変更
        //spriteRenderer.color = color;
        //spriteRenderer.color = new Color(1.0f, 0.5f, 0.5f, 1.0f);

        float lerpFactor = Mathf.Clamp01(keyHoldTime / maxHoldTime);
        spriteRenderer.color = Color.Lerp(originalColor, pressedColor, lerpFactor);

        // 子要素がある場合、同様の処理を再帰的に適用
        foreach (Transform child in transform)
        {
            SpriteRenderer childSpriteRenderer = child.GetComponent<SpriteRenderer>();
            if (childSpriteRenderer != null)
            {
                childSpriteRenderer.color = Color.Lerp(originalColor, pressedColor, lerpFactor); ;
            }
        }
    }

}
