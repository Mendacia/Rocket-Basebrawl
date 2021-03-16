using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class playerDash : MonoBehaviour
{
    private Rigidbody playerRigidbody = null;
    private playerControls playerCont;
    private bool canDash = true;
    [SerializeField] private float dashDuration = 0.1f;
    [SerializeField] private float dashCooldown = 0.5f;
    [SerializeField] private Animator playerAnim = null;

    public Vector3 recievedVector(Vector3 myVector)
    {
        return myVector;
    }

    //Get input code
    private void Awake()
    {
        playerRigidbody = gameObject.GetComponent<Rigidbody>();
        playerCont = gameObject.GetComponent<playerControls>();
    }
    
    public void PushThePlayerForwardRealHard(CallbackContext context)
    {
        if (context.performed && playerCont.playerState == 2)
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
        yield return new WaitForSeconds(dashDuration);
        playerCont.Dash(false);
        yield return new WaitForSeconds(dashCooldown);
        isDashing = false;
        canDash = true;
    }
}
