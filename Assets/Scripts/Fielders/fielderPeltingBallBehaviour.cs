using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fielderPeltingBallBehaviour : MonoBehaviour
{
    public float ballSpeed, pingSpeed;
    public Transform fielder = null;
    public bool isHittable = true;

    // Update is called once per frame
    void Start()
    {
        transform.rotation = fielder.rotation;
        var body = GetComponent<Rigidbody>();
        body.velocity = transform.forward * ballSpeed * 100 * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BallHit")
        {
            var body = GetComponent<Rigidbody>();
            Vector3 pingDirection = new Vector3(Random.Range(0, -5), Random.Range(0, 2), Random.Range(-5, 5));
            body.velocity += pingDirection * (ballSpeed / 2) * pingSpeed * Time.deltaTime;
            isHittable = false;
            Detector.ballCols.Remove(this.gameObject.transform);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 0)
        {
            StartCoroutine(DestroyBall());
        }
    }

    IEnumerator DestroyBall()
    {
        yield return new WaitForSeconds(5f);
        var ball = this.gameObject;
        Detector.ballCols.Remove(ball.transform);
        Destroy(ball);
    }
}
