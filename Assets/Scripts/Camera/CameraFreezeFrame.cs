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
        vcam.m_Lens.FieldOfView = 10;
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(0.3f);
        vcam.m_Lens.FieldOfView = 20;
        Time.timeScale = 0.3f;
        if (PauseMenu.isPaused == false)
        {
            Time.timeScale = Mathf.MoveTowards(Time.timeScale, 1, 25 * Time.unscaledDeltaTime);
            //cameraRoot.SetActive(false);
            //this.gameObject.SetActive(false);
        }
        yield return StartCoroutine(WaitForRealSeconds(waitTime));
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
