using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fielderPeltingBallBehaviour : MonoBehaviour
{
    public float ballSpeed;
    public Transform fielder = null;
    public bool isHittable = true;

    // Update is called once per frame
    void Start()
    {
        transform.rotation = fielder.rotation;
        var body = GetComponent<Rigidbody>();
        body.velocity = transform.forward * ballSpeed;
    }
}
