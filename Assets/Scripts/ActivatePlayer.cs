using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class ActivatePlayer : MonoBehaviour
{
    private Rigidbody rb;
    //Score Reference
    GameObject scoreHolder;
    private PlayerInputActions inputActions;
    private void Awake()
    {
        inputActions = new PlayerInputActions();
        scoreHolder = GameObject.Find("Scoreholder");
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerActivation(CallbackContext callbackContext)
    {
        //Call the coroutine to activate player only when you're NOT watching the dolly and when you're locked
        if (callbackContext.performed && DeactiveateCamera.dollyActive == false && runningPhaseMovement.playerState == 1)
        {
            StartCoroutine(StateActivation());
        }
    }

    IEnumerator StateActivation()
    {
        Time.timeScale = 0.3f;
        yield return new WaitForSeconds(0.25f);
        if (scoreHolder.GetComponent<scoreHolder>().score >= 1)
        {
            //Sets playerState to movement and unlocks the rigidbodies
            runningPhaseMovement.playerState = 2;
            rb.constraints = ~RigidbodyConstraints.FreezePositionX & ~RigidbodyConstraints.FreezePositionZ;
            inputActions.Player.EnablePlayer.Disable();
        }
        Time.timeScale = 1;
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
