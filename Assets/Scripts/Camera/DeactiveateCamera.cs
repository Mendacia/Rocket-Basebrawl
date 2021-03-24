using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
using Cinemachine;

public class DeactiveateCamera : MonoBehaviour
{
    private GameObject startCam;
    [SerializeField] private GameObject clickStartText = null;
    bool hasSkipped = false;

    private void Start()
    {
        startCam = this.gameObject;
        
    }

    public void skipDolly()
    {
        if(hasSkipped == false)
        {
            hasSkipped = true;
            WorldStateMachine.SetCurrentState(WorldState.FIRSTPITCH);
            clickStartText.SetActive(false);
            startCam.SetActive(false);
        }
    }
}
