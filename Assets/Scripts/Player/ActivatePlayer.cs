using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class ActivatePlayer : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerInputActions inputActions;
    private playerControls player;


    private void Awake()
    {
        player = GetComponent<playerControls>();
        inputActions = new PlayerInputActions();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        //Yes this is fine to have in update, the script deactivates itself anyway

        if(WorldStateMachine.GetCurrentState() == WorldState.RUNNING)
        {
            rb.constraints = ~RigidbodyConstraints.FreezePositionX & ~RigidbodyConstraints.FreezePositionY & ~RigidbodyConstraints.FreezePositionZ;
            //inputActions.Player.EnablePlayer.Disable();
            this.GetComponent<ActivatePlayer>().enabled = false;
        }
    }

    public void PlayerActivation(CallbackContext callbackContext)
    {
        //Accepts players first input, enabling the scene
        if(callbackContext.performed && WorldStateMachine.GetCurrentState() == WorldState.FROZEN)
        {
            //WorldStateMachine.SetCurrentState(WorldState.BATTING);
            this.GetComponent<ActivatePlayer>().enabled = false;
        }

        //Call the coroutine to activate player only when you're NOT watching the dolly and when you're locked
        if (callbackContext.performed && WorldStateMachine.GetCurrentState() == WorldState.BATTING && !DeactiveateCamera.dollyActive)
        {
            //StartCoroutine(TimeSlowOnHit());
        }
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }
}
