using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattingOnClick : MonoBehaviour
{
    public float speed;
    Transform ball;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

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
        if (Detector.isHittable == true)
        {
            ball = GetClosestBall(Detector.ballCols, this.transform);
            var body = ball.GetComponent<Rigidbody>();
            Vector3 camForward = Camera.main.transform.rotation * transform.forward;
            body.velocity = camForward * speed;
            body.useGravity = true;
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
}
