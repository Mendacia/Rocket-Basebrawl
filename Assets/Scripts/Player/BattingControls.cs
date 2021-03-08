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
    [SerializeField] private soundEffectHolder sfxHolder = null;

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
                if (context.performed && isHitting == false && PauseMenu.isPaused == false)
                {
                    myCollider.enabled = true;
                    dashBat = true;
                    isHitting = true;
                    playerAnimator.SetTrigger("heHit");
                    particleMaster.SetActive(true);
                    StartCoroutine(Cooldown());
                }
                break;

            case true:
                if (context.performed && isHitting == false && PauseMenu.isPaused == false)
                {
                    myCollider.enabled = true;
                    dashBat = true;
                    isHitting = true;
                    Debug.Log("DAAAAASH BAAAAT!!!!!!!!!!!!!!!");
                    playerAnimator.SetTrigger("heDashHit");
                    particleMaster.SetActive(true);
                    StartCoroutine(Cooldown());
                }
                break;
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(hitWindow);
        myCollider.enabled = false;
        yield return new WaitForSeconds(hitCooldown);
        particleMaster.SetActive(false);
        dashBat = false;
        isHitting = false;
    }
}
