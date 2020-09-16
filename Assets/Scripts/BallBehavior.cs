using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    public float speed;
    private GameObject pitcher;
    public bool isHittable = true;

    private void Start()
    {
        pitcher = GameObject.FindGameObjectWithTag("Pitcher");
        transform.rotation = pitcher.transform.rotation;
        var body = GetComponent<Rigidbody>();
        body.velocity = transform.forward * speed * 100 * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            var body = GetComponent<Rigidbody>();
            Vector3 pingDirection = new Vector3(Random.Range(0,-5), Random.Range(0, 2), Random.Range(-5, 5));
            body.velocity += pingDirection * speed * 30 * Time.deltaTime;
            isHittable = false;
            Detector.ballCols.Remove(this.gameObject.transform);
        }
    }
}
