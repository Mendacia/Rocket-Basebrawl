using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBallHit : MonoBehaviour
{
    [SerializeField] private float DestroyWaitTime = 0.5f;

    void Start()
    {
        StartCoroutine(DestroyBall());
    }

    IEnumerator DestroyBall()
    {
        yield return new WaitForSeconds(DestroyWaitTime);
        Destroy(this.gameObject);
    }
}
