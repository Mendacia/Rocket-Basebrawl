using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.InputSystem;

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

    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void Start()
    {
        anim = animReference.gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        ball = GetClosestBall(Detector.ballCols, this.transform);

        RotatePlane();
    }

    IEnumerator TempWaitFor()
    {
        yield return new WaitForSeconds(1);
        tempAnimCheck = false;
    }

    public void HitBall(CallbackContext context)
    {
        if (context.started)
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
                StartCoroutine(TempWaitFor());
            }
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