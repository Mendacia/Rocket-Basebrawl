using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class ActivatePlayer : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerInputActions inputActions;

    [Header("Put the Player Controller here")]
    [SerializeField] private playerControls playerStateReference = null;

    [Header("Cinemachine Variables")]
    [SerializeField] private CinemachineCameraShake camShake = null;
    [SerializeField] private CinemachineCameraShake camShakeAim = null;
    [SerializeField] private float frequency = 0.8f, amplitude = 3f, waitTime = 0.1f;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            //StartCoroutine(TimeSlowOnHit());
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
    IEnumerator TurnShakeOnAndOff()
    {
        camShake.Noise(frequency, amplitude);
        camShakeAim.Noise(frequency, amplitude);
        yield return new WaitForSeconds(waitTime);
        camShake.Noise(0, 0);
        camShakeAim.Noise(0, 0);
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
