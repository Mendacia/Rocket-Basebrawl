using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class playerDash : MonoBehaviour
{
    private Rigidbody playerRigidbody = null;
    [SerializeField] private playerControls playerCont;
    private bool canDash = true;
    [SerializeField] private float dashDuration = 0.1f;
    [SerializeField] private float dashCooldown = 0.5f;
    [SerializeField] private BattingControls battingCont = null;

    public Vector3 recievedVector(Vector3 myVector)
    {
        return myVector;
    }

    //Get input code
    private void Start()
    {
        playerRigidbody = gameObject.GetComponent<Rigidbody>();
    }
    
    public void PushThePlayerForwardRealHard(CallbackContext context)
    {
        if (context.performed)
        {
            if (canDash)
            {
                canDash = false;
                battingCont.DashBatting();
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
        canDash = true;
    }
}
