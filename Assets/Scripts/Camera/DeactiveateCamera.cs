using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
using Cinemachine;

public class DeactiveateCamera : MonoBehaviour
{
    [SerializeField] private GameObject startCam = null;
    private GameObject camMaster;
    [SerializeField] private GameObject clickStartText = null;
    bool hasSkipped = false;

    private void Start()
    {
        camMaster = this.gameObject;
        
    }

    public void skipDolly()
    {
        if(hasSkipped == false)
        {
            hasSkipped = true;
            clickStartText.SetActive(false);
            startCam.SetActive(false);
            StartCoroutine(delayFirstPitch());
        }
    }

    IEnumerator delayFirstPitch()
    {
        yield return new WaitForSeconds(0.15f);
        WorldStateMachine.SetCurrentState(WorldState.FIRSTPITCH);
        camMaster.SetActive(false);
    }
}
