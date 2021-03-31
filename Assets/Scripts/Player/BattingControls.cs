using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

public class BattingControls : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator = null;
    [SerializeField] private float hitWindow = 0.5f;
    [SerializeField] private float hitCooldown = 0.5f;
    [SerializeField] private playerDash dashCont = null;
    private bool isHitting = false;
    public bool dashBat = false;
    private BoxCollider myCollider = null;
    [SerializeField] private GameObject particleMaster = null;
    [SerializeField] private GameObject ventParticles = null;

    private void Start()
    {
        myCollider = gameObject.GetComponent<BoxCollider>();
        myCollider.enabled = false;
        isHitting = false;
    }
    public void Batting(CallbackContext context)
    {
        switch (dashCont.isDashing)
        {
            case false:
                if (context.performed && isHitting == false && PauseMenu.isPaused == false && WorldStateMachine.GetCurrentState() != WorldState.FROZEN && WorldStateMachine.GetCurrentState() != WorldState.GAMESTART)
                {
                    myCollider.enabled = true;
                    dashBat = true;
                    isHitting = true;
                    playerAnimator.SetTrigger("heHit");
                    StartCoroutine(Cooldown());
                }
                break;

            case true:
                if (context.performed && isHitting == false && PauseMenu.isPaused == false && WorldStateMachine.GetCurrentState() != WorldState.FROZEN && WorldStateMachine.GetCurrentState() != WorldState.GAMESTART)
                {
                    myCollider.enabled = true;
                    dashBat = true;
                    isHitting = true;
                    Debug.Log("DAAAAASH BAAAAT!!!!!!!!!!!!!!!");
                    playerAnimator.SetTrigger("heDashHit");
                    StartCoroutine(Cooldown());
                }
                break;
        }
    }

    IEnumerator Cooldown()
    {
        ventParticles.SetActive(true);
        particleMaster.SetActive(true);
        yield return new WaitForSeconds(hitWindow);
        myCollider.enabled = false;
        yield return new WaitForSeconds(hitCooldown);
        ventParticles.SetActive(false);
        particleMaster.SetActive(false);
        dashBat = false;
        isHitting = false;
    }
}
