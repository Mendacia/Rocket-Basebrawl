using UnityEngine;
using System.Collections.Generic;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.InputSystem;

public class AimOnKeypress : MonoBehaviour
{
    public int PriorityBoostAmount = 5;
    public GameObject Reticle;

    Cinemachine.CinemachineVirtualCameraBase vcam;
    public bool boosted = false;

    void Start()
    {
        vcam = GetComponent<Cinemachine.CinemachineVirtualCameraBase>();
    }

    public void aimOn(CallbackContext context)
    {

        if (context.performed && !PauseMenu.isPaused)
        {
            if (!boosted)
            {
                vcam.Priority += PriorityBoostAmount;
                boosted = true;
            }
        }
        else if (vcam != null && context.canceled)
        {
            if (boosted)
            {
                vcam.Priority -= PriorityBoostAmount;
                boosted = false;
            }
        }
        if (Reticle != null)
        {
            Reticle.SetActive(boosted);
        }
    }

    Transform GetClosestFielder(List<Transform> fielders, Transform fromThis)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = fromThis.position;
        foreach (Transform potentialTarget in fielders)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
        return bestTarget;
    }
}
