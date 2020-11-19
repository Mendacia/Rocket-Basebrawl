using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraLookAt : MonoBehaviour
{
    [SerializeField] private Cinemachine.CinemachineVirtualCamera vcam = null;
    [SerializeField] private GameObject cameraRoot = null;

    private bool sequenceStarted = false;

    void Update()
    {
        if(GameObject.Find("Scoreholder").GetComponent<scoreHolder>().score == 1 && !sequenceStarted)
        {
            sequenceStarted = true;
            StartCoroutine(cameraSequence());
        }
    }

    IEnumerator cameraSequence()
    {
        cameraRoot.SetActive(true);
        vcam.LookAt = GameObject.Find("Baseball(Clone)").transform;
        yield return new WaitForSeconds(1f);
        cameraRoot.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
