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
        cameraRoot.SetActive(true);
        vcam.LookAt = GameObject.Find("Baseball(Clone)").transform;
        //vcam.Follow = GameObject.Find("Baseball(Clone)").transform;
        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 0.5f;
        firstBallHit = true;
        yield return StartCoroutine(WaitForRealSeconds(waitTime));
        if (PauseMenu.isPaused == false)
        {
            Time.timeScale = 1;
            cameraRoot.SetActive(false);
            this.gameObject.SetActive(false);
        }
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
