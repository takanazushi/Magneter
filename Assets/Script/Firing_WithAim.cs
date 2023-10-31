using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Firing_WithAim : MonoBehaviour
{
    //Prefabs‚Å•¡»‚·‚é•¨‚ğ“ü‚ê‚é
    [SerializeField, Header("’ePrefab‚ğ“ü‚ê‚Ä‚­‚¾‚³‚¢")]
    public GameObject BulletObj;

    void Start()
    {   
        StartCoroutine(ShotCoroutine());
    }

    IEnumerator ShotCoroutine()
    {
        while (true)
        {
            // Prefab‚ğÀ‘Ì‰»
            Instantiate(BulletObj);

            // 1.5•b‘Ò‹@‚·‚é
            yield return new WaitForSeconds(1.5f);
        }
    }

}
