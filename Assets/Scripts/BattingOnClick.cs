using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattingOnClick : MonoBehaviour
{
    public float speed;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            HitBall();
        }
    }

    void HitBall()
    {
        //playAnimation
        if(Detector.isHittable == true)
        {
            GameObject ball = GameObject.FindGameObjectWithTag("Ball");
            if(GameObject.FindGameObjectWithTag("Ball") == true)
            {
                var body = ball.GetComponent<Rigidbody>();
                body.velocity = transform.forward * -speed;
                Debug.Log("Success");
            }
        }
    }
}
