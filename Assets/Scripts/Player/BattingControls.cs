using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

public class BattingControls : MonoBehaviour
{
    //[SerializeField] private Text devHittingCheckText = null;
    [SerializeField] private Animator playerAnimator = null;
    [SerializeField] private float hitWindow = 0.5f;
    [SerializeField] private float hitCooldown = 0.5f;
    [SerializeField] private playerDash dashCont = null;
    [SerializeField] private fielderWhacked fielderWhackingScript = null;
    [SerializeField] private LayerMask fielderLayerMask = 0;
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
                    //sfxHolder.BattingSoundEffect();
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
                    //Set trigger for dash bat
                    //sfxHolder.BattingSoundEffect();
                    particleMaster.SetActive(true);
                    StartCoroutine(Cooldown());
                }
                break;
        }

        
    }

   /* public void DashBatting(CallbackContext context)
    {
        if(context.performed && isHitting == false && PauseMenu.isPaused == false)

        if (isHitting == false && PauseMenu.isPaused == false)
        {
            myCollider.enabled = true;
            isHitting = true;
            Debug.Log("DASH BAT!");
            //Do SetTrigger for Dash Batting animation
            particleMaster.SetActive(true);
            StartCoroutine(Cooldown());
        }
    }*/

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(hitWindow);
        myCollider.enabled = false;
        particleMaster.SetActive(false);
        yield return new WaitForSeconds(hitCooldown);
        dashBat = false;
        isHitting = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Fielder")
        {
            fielderWhackingScript.findFielder(other.transform);
        }
    }
}
