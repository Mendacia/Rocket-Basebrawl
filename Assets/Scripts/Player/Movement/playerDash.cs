using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class playerDash : MonoBehaviour
{
    [SerializeField] private playerControls playerCont = null;
    private bool canDash = true;
    public bool isDashing = false;
    [SerializeField] private float dashSpeed = 100;
    [SerializeField] private float dashDuration = 0.1f;
    [SerializeField] private float dashCooldown = 0.5f;
    [SerializeField] private Animator playerAnim = null;

    public Vector3 recievedVector(Vector3 myVector)
    {
        return myVector;
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
        isDashing = true;
        playerAnim.SetTrigger("heDashing");
        playerCont.speed = dashSpeed;
        yield return new WaitForSeconds(dashDuration);
        playerCont.speed = playerCont.topSpeed;
        yield return new WaitForSeconds(dashCooldown);
        isDashing = false;
        canDash = true;
    }
}
