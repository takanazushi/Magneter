using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Checkpoint : MonoBehaviour
{
    bool collisionflg = true;
    int no;

    private Renderer renderer;

    [SerializeField, Header("チェックポイントNo")]
    private int checkNo = 0;

    /// <summary>
    /// カメラ番号
    /// </summary>
    [SerializeField, Header("カメラ"),
        Tooltip("チェックポイントから開始時のカメラ")]
    private CinemachineVirtualCamera Stert_Camera;

    [SerializeField, Header("通過後スプライト")]
    private Sprite passdSprite;

    [SerializeField, Header("通過後Effect")]
    private GameObject passEffect;

    [SerializeField]
    private AudioClip passSE;

    private SpriteRenderer spriteRenderer;

    private Light2D myLight;

    private GameObject effect;

    AudioSource audioSource;

    //デバック用
    private void Start()
    {
        renderer = GetComponent<Renderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        myLight = GetComponent<Light2D>();
        myLight.enabled = false;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("checkPointにAudioSourceついてない");
        }

        if (GameManager.instance.checkpointNo >= checkNo)
        {
            spriteRenderer.sprite = passdSprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && GameManager.instance.checkpointNo < checkNo)
        {
            GameManager.instance.checkpointNo = checkNo;
            //GameManager.instance.SetStaetCamera();

            //デバック用
            //通ったら赤
            effect=Instantiate(passEffect, transform.position, Quaternion.identity);
            effect.SetActive(true);
            audioSource.PlayOneShot(passSE);
            spriteRenderer.sprite = passdSprite;
            myLight.enabled = true;
            StartCoroutine(ShowTargetAfterDelay(0.4f));

        }
    }

    private IEnumerator ShowTargetAfterDelay(float delay)
    {
        // 指定した時間だけ待つ
        yield return new WaitForSeconds(delay);

        // GameObjectを表示する
        effect.SetActive(false);
    }
}
        //    no = gameObject.name.IndexOf(GameManager.instance.checkpointNo.ToString());
        //if (no != -1)
        //{

        //    Debug.Log(no);

        //    if (collision.gameObject.name == "Player" && collisionflg)
        //    {
        //        string st = gameObject.name[no].ToString();
        //        GameManager.instance.checkpointNo = int.Parse(st);
        //        //int.Parse(gameObject.name[no]);
        //        collisionflg = false;
        //        Debug.Log(GameManager.instance.checkpointNo);
        //    }
        //}
    

