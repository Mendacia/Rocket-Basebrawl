using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fielderPeltingBallBehaviour : MonoBehaviour
{
    [SerializeField] private float ballSpeed;
    public float expireAfterSeconds = 20;
    [SerializeField] private GameObject ballHit = null;

    void Start()
    {
        var body = GetComponent<Rigidbody>();
        Instantiate(ballHit, this.transform);
        ballHit.transform.parent = null;
        body.velocity = transform.forward * ballSpeed * 100 * Time.deltaTime;
        StartCoroutine(Expire());
    }

    IEnumerator Expire()
    {
        yield return new WaitForSeconds(expireAfterSeconds);
        Destroy(gameObject);
    }
}
