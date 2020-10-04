using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattingOnClick : MonoBehaviour
{
    public float speed;
    Transform ball;
    fielderPeltingBallBehaviour ballScr;
    public Transform camT;
    private Animator anim;
    public GameObject animReference;

    public static bool tempAnimCheck = false;

    public Transform batPlane;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        anim = animReference.gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            anim.Play("Swinging");
            tempAnimCheck = true;
            StartCoroutine(TempWaitFor());
        }
        ball = GetClosestBall(Detector.ballCols, this.transform);
        if (Input.GetButtonDown("Fire1") && ball != null)
        {
            HitBall();
        }

        RotatePlane();
    }

    IEnumerator TempWaitFor()
    {
        yield return new WaitForSeconds(1);
        tempAnimCheck = false;
    }

    void HitBall()
    {
        ballScr = ball.GetComponent<fielderPeltingBallBehaviour>();
        //playAnimation
        anim.Play("Swinging");
        if (ballScr.isHittable == true)
        {
            tempAnimCheck = true;
            var body = ball.GetComponent<Rigidbody>();
            Vector3 camForward = camT.rotation * transform.forward;
            body.velocity = camForward * speed * 150 * Time.deltaTime;
            body.velocity += new Vector3(0f, 3f, 0f);
            body.useGravity = true;
            ballScr.isHittable = false;
            Detector.ballCols.Remove(ball);
            //AddScore OR AddScore on enemy hit. Depending on what we doing.
            StartCoroutine(TempWaitFor());
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
        //batPlane.eulerAngles += new Vector3(0, 0, -Input.GetAxis("Mouse X") * 5);
    }
}