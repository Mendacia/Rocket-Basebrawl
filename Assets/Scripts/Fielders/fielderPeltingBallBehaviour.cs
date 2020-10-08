using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fielderPeltingBallBehaviour : MonoBehaviour
{
    public float ballSpeed, pingSpeed;
    public bool isHittable = true;

    // Update is called once per frame
    void Start()
    {
        var body = GetComponent<Rigidbody>();
        body.velocity = transform.forward * ballSpeed * 100 * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BallHit")
        {
            var body = GetComponent<Rigidbody>();
            Vector3 pingDirection = new Vector3(Random.Range(0, -5), Random.Range(0, 2), Random.Range(-5, 5));
            body.velocity += pingDirection * ballSpeed * pingSpeed * Time.deltaTime;
            isHittable = false;
            Detector.ballCols.Remove(this.gameObject.transform);
        }
    }
}
