using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class ActivatePlayer : MonoBehaviour
{
    private Rigidbody rb;
    //Score Reference
    private PlayerInputActions inputActions;

    [SerializeField] private bool isFrozen = false;

    [SerializeField] private runningPhaseMovement playerStateReference = null;
    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (isFrozen)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
    void Update()
    {
        //Yes this is fine to have in update, the script deactivates itself anyway

        if(playerStateReference.playerState == 2)
        {
            playerStateReference.playerState = 2;
            rb.constraints = ~RigidbodyConstraints.FreezePositionX & ~RigidbodyConstraints.FreezePositionY & ~RigidbodyConstraints.FreezePositionZ;
            //inputActions.Player.EnablePlayer.Disable();
            //fielderMain.startPeltingLoop();
            this.GetComponent<ActivatePlayer>().enabled = false;
        }
    }

    public void PlayerActivation(CallbackContext callbackContext)
    {
        //Accepts players first input, enabling the scene
        if(callbackContext.performed && playerStateReference.playerState == 0)
        {
            playerStateReference.playerState = 1;
        }

        //Call the coroutine to activate player only when you're NOT watching the dolly and when you're locked
        if (callbackContext.performed && playerStateReference.playerState == 1 && !DeactiveateCamera.dollyActive)
        {
            StartCoroutine(TimeSlowOnHit());
        }
    }

    IEnumerator TimeSlowOnHit()
    {
        if (PauseMenu.isPaused == false)
        {
            Time.timeScale = 0.3f;
            yield return new WaitForSeconds(0.25f);
            Time.timeScale = 1;
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
