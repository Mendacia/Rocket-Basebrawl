using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
using Cinemachine;

public class DeactiveateCamera : MonoBehaviour
{
    public GameObject startCam;

    private void Start()
    {
        WorldStateMachine.SetCurrentState(WorldState.GAMESTART);
    }

    public void skipDolly()
    {
        WorldStateMachine.SetCurrentState(WorldState.FIRSTPITCH);
        //dollyActive = false;
        startCam.SetActive(false);
    }
}
