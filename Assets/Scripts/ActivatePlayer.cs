using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class ActivatePlayer : MonoBehaviour
{
    private Rigidbody rb;
    //Score Reference
    private PlayerInputActions inputActions;

    [SerializeField] private fielderPeltingScript fielderMain = null;
    [SerializeField] private scoreHolder scoreHolderObject;
    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    void Update()
    {
        //Yes this is fine to have in update, the script deactivates itself anyway

        if(scoreHolderObject.score >= 1)
        {
            runningPhaseMovement.playerState = 2;
            rb.constraints = ~RigidbodyConstraints.FreezePositionX & ~RigidbodyConstraints.FreezePositionZ;
            //inputActions.Player.EnablePlayer.Disable();
            fielderMain.startPeltingLoop();
            this.GetComponent<ActivatePlayer>().enabled = false;
        }
    }

    public void PlayerActivation(CallbackContext callbackContext)
    {
        //Accepts players first input, enabling the scene
        if(callbackContext.performed && runningPhaseMovement.playerState == 0)
        {
            runningPhaseMovement.playerState = 1;
        }

        //Call the coroutine to activate player only when you're NOT watching the dolly and when you're locked
        if (callbackContext.performed && runningPhaseMovement.playerState == 1)
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
