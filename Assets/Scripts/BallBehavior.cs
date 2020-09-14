using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    public float speed;
    public GameObject pitcher;
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
            isHittable = false;
            Detector.ballCols.Remove(this.gameObject.transform);
        }
    }
}
