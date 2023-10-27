using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetectionCanceled : MonoBehaviour
{
    [SerializeField]
    private float disableTime = 0.1f;

    private Collider2D collider2;

    private void Awake()
    {
        collider2 = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        StartCoroutine(DisableCollision());
    }

    private IEnumerator DisableCollision()
    {
        collider2.enabled = false;
        yield return new WaitForSeconds(disableTime);
        collider2.enabled = true;
    }
}
