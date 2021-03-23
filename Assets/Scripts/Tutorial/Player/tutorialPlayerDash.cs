﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class tutorialPlayerDash : MonoBehaviour
{
    private tutorialPlayerControls playerCont;
    private bool canDash = true;
    [SerializeField] private float dashDuration = 0.1f;
    [SerializeField] private float dashCooldown = 0.5f;
    [System.NonSerialized] public bool isDashing = false;

    public Vector3 recievedVector(Vector3 myVector)
    {
        return myVector;
    }

    //Get input code
    private void Awake()
    {
        playerCont = gameObject.GetComponent<tutorialPlayerControls>();
    }
    
    public void PushThePlayerForwardRealHard(CallbackContext context)
    {
        if (context.performed && TutorialStateMachine.GetCurrentState() == TutorialState.RUNNING)
        {
            if (canDash)
            {
                canDash = false;
                StartCoroutine(Dash());
            }
        }
        if (context.performed && TutorialStateMachine.GetCurrentState() == TutorialState.WALKING)
        {
            if (canDash)
            {
                canDash = false;
                StartCoroutine(Dash());
            }
        }
    }

    IEnumerator Dash()
    {
        playerCont.Dash(true);
        isDashing = true;
        yield return new WaitForSeconds(dashDuration);
        playerCont.Dash(false);
        yield return new WaitForSeconds(dashCooldown);
        isDashing = false;
        canDash = true;
    }
}
