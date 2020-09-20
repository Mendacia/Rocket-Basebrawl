using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattingOnClick : MonoBehaviour
{
    public float speed;
    Transform ball;
    BallBehavior ballScr;
    public Transform camT;

    public Transform batPlane;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        ball = GetClosestBall(Detector.ballCols, this.transform);
        if (Input.GetButtonDown("Fire1") && ball != null)
        {
            HitBall();
        }

        RotatePlane();
    }

    void HitBall()
    {
        ballScr = ball.GetComponent<BallBehavior>();
        //playAnimation
        if (ballScr.isHittable == true)
        {
            var body = ball.GetComponent<Rigidbody>();
            Vector3 camForward = camT.rotation * transform.forward;
            body.velocity = camForward * speed * 100 * Time.deltaTime;
            body.velocity += new Vector3(0f, 1f * 10, 0f);
            body.useGravity = true;
            ballScr.isHittable = false;
            Detector.ballCols.Remove(ball);
            //AddScore OR AddScore on enemy hit. Depending on what we doing.
        }
    }
    Transform GetClosestBall(List<Transform> balls, Transform fromThis)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = fromThis.position;
        foreach (Transform potentialTarget in balls)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
        return bestTarget;
    }

    void RotatePlane()
    {
        batPlane.eulerAngles += new Vector3(0, 0, -Input.GetAxis("Mouse X") * 5);
    }
}