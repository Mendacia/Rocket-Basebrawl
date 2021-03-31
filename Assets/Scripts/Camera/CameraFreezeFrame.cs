using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFreezeFrame : MonoBehaviour
{
    [SerializeField] private Cinemachine.CinemachineVirtualCamera vcam = null;
    [SerializeField] private GameObject cameraRoot = null;
    [SerializeField] private float waitTime = 1;

    private bool sequenceStarted = false;
    private bool firstBallHit = false;

    private void Update()
    {
        if(!firstBallHit && WorldStateMachine.GetCurrentState() == WorldState.RUNNING)
        {
            if (!sequenceStarted)
            {
                firstBallHit = true;
                sequenceStarted = true;
                StartCoroutine(cameraSequence());
            }
        }
    }

    IEnumerator cameraSequence()
    {
        Transform ball = GameObject.Find("Baseball(Clone)").transform;
        cameraRoot.SetActive(true);
        vcam.LookAt = ball;
        //vcam.Follow = ball;
        //Rigidbody ballRB = GameObject.Find("Baseball(Clone)").GetComponent<Rigidbody>();
        yield return new WaitForSeconds(0.1f);
        //ballRB.velocity = Vector3.zero;
        //ballRB.constraints = RigidbodyConstraints.FreezeAll;
        vcam.m_Lens.FieldOfView = 10;
        Time.timeScale = 0.2f;
        //yield return new WaitForSecondsRealtime(0.3f);
        vcam.m_Lens.FieldOfView = 20;
        //Time.timeScale = 0.3f;
        if (PauseMenu.isPaused == false)
        {
            Time.timeScale = Mathf.MoveTowards(Time.timeScale, 1, 10 * Time.unscaledDeltaTime);
            //cameraRoot.SetActive(false);
            //this.gameObject.SetActive(false);
        }
        yield return StartCoroutine(WaitForRealSeconds(waitTime));
        //ballRB.constraints = ~RigidbodyConstraints.FreezeAll;
        //ballRB.velocity = transform.forward * 1069 * 100 * Time.deltaTime;
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }

    IEnumerator WaitForRealSeconds(float seconds)
    {
        float startTime = Time.realtimeSinceStartup;
        while(Time.realtimeSinceStartup-startTime < seconds)
        {
            yield return null;
        }
    }
}
