using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class playerDash : MonoBehaviour
{
    private Rigidbody playerRigidbody = null;
    [SerializeField] private playerControls playerCont;
    private bool canDash = true;
    [SerializeField] private float dashSpeed = 100;
    [SerializeField] private float dashDuration = 0.1f;
    [SerializeField] private float dashCooldown = 0.5f;

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
            //playerRigidbody.AddForce(Vector3.forward * 100000);
            if (canDash)
            {
                canDash = false;
                StartCoroutine(Dash());
            }
        }
    }

    IEnumerator Dash()
    {
        playerCont.speed = dashSpeed;
        yield return new WaitForSeconds(dashDuration);
        playerCont.speed = playerCont.topSpeed;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
