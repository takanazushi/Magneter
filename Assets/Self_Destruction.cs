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
    /// �������s�b��
    /// </summary>
    float maxHoldTime=3.0f;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player_HP=GetComponent<Player_HP>();

        // �����F��ݒ�
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
            //�o�ߎ��ԑ���
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
                Debug.Log("����");
            }

        }
        ChangeColor(pressedColor);
    }

    private void ChangeColor(Color color)
    {
        // �X�v���C�g�̐F��ύX
        //spriteRenderer.color = color;
        //spriteRenderer.color = new Color(1.0f, 0.5f, 0.5f, 1.0f);

        float lerpFactor = Mathf.Clamp01(keyHoldTime / maxHoldTime);
        spriteRenderer.color = Color.Lerp(originalColor, pressedColor, lerpFactor);

        // �q�v�f������ꍇ�A���l�̏������ċA�I�ɓK�p
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
