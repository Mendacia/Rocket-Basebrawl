using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fielderPeltingBallBehaviour : MonoBehaviour
{
    [SerializeField] private float ballSpeed;
    [SerializeField] private float expireAfterSeconds = 20;

    void Start()
    {
        var body = GetComponent<Rigidbody>();
        body.velocity = transform.forward * ballSpeed * 100 * Time.deltaTime;
        StartCoroutine(Expire());
    }

    IEnumerator Expire()
    {
        yield return new WaitForSeconds(expireAfterSeconds);
        Destroy(gameObject);
    }
}
