using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

public class TempBattingControls : MonoBehaviour
{
    //[SerializeField] private Text devHittingCheckText = null;
    [SerializeField] private Animator playerAnimator = null;
    private bool isHitting = false;
    private BoxCollider myCollider = null;

    private void Start()
    {
        myCollider = gameObject.GetComponent<BoxCollider>();
        myCollider.enabled = false;
        //devHittingCheckText.text = ("IDLE");
        isHitting = false;
    }
    public void Batting(CallbackContext context)
    {
        if (context.performed && isHitting == false)
        {
            myCollider.enabled = true;
            //devHittingCheckText.text = ("HITTING");
            isHitting = true;
            playerAnimator.SetTrigger("heHit");
            StartCoroutine(Cooldown());
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(0.5f);
        myCollider.enabled = false;
        //devHittingCheckText.text = ("IDLE");
        isHitting = false;
    }
}
