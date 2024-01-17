using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Self_Destruction : MonoBehaviour
{
    float keyHoldTime = 0f;
    public SpriteRenderer spriteRenderer;
    public Color originalColor = Color.white;
    public Color pressedColor = Color.red;

    Player_HP player_HP = null;
    

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
        if (Input.GetKey(KeyCode.G))
        {
            keyHoldTime += Time.deltaTime;
            ChangeColor(pressedColor);
            Debug.Log(keyHoldTime);

            if (keyHoldTime >= 3f)
            {
                keyHoldTime = 0;
                player_HP.HitDamage(30);
                Debug.Log("自爆");
            }
            
        }
        else if (Input.GetKeyUp(KeyCode.G))
        {
            ChangeColor(originalColor);
            keyHoldTime = 0;
        }
    }

    private void ChangeColor(Color color)
    {
        // スプライトの色を変更
        spriteRenderer.color = color;

        // 子要素がある場合、同様の処理を再帰的に適用
        foreach (Transform child in transform)
        {
            SpriteRenderer childSpriteRenderer = child.GetComponent<SpriteRenderer>();
            if (childSpriteRenderer != null)
            {
                childSpriteRenderer.color = color;
            }
        }
    }

}
