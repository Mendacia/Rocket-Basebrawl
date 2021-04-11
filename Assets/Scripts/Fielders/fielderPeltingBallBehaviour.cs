using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fielderPeltingBallBehaviour : MonoBehaviour
{
    [SerializeField] private float ballSpeed;
    public float expireAfterSeconds = 20;
    public Rigidbody body;
    [SerializeField] private GameObject ballHit = null;

    void Start()
    {
        Instantiate(ballHit, transform);
        ballHit.transform.parent = null;
        body.velocity = transform.forward * ballSpeed * 100 * Time.deltaTime;
        StartCoroutine(Expire());
    }

    public void changeDirection()
    {
        body.velocity = transform.forward * ballSpeed * 100 * Time.deltaTime;
    }

    IEnumerator Expire()
    {
        yield return new WaitForSeconds(expireAfterSeconds);
        Destroy(gameObject);
    }

    public void StartTheCoroutine(float seconds, Vector3 spawnLocation)
    {
        StartCoroutine(SecondPartOfArcBall(seconds, spawnLocation));
    }

    public IEnumerator SecondPartOfArcBall(float seconds, Vector3 spawnLocation)
    {
        Debug.Log("My Coroutine is running");
        yield return new WaitForSeconds(seconds);
        Debug.Log("Going");
        transform.position = new Vector3(spawnLocation.x, 50, spawnLocation.z);
        transform.LookAt (new Vector3(spawnLocation.x, 0, spawnLocation.z));
        var body = GetComponent<Rigidbody>();
        body.velocity = transform.forward * ballSpeed * 100 * Time.deltaTime;
    }
}
