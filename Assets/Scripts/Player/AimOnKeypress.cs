using UnityEngine;
using System.Collections.Generic;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.InputSystem;

public class AimOnKeypress : MonoBehaviour
{
    public int PriorityBoostAmount = 10;
    public GameObject Reticle;

    [SerializeField] private bool usingDolly = false;
    private int camState;

    Cinemachine.CinemachineVirtualCameraBase vcam;
    public bool boosted = false;

    void Start()
    {
        if (usingDolly)
        {
            camState = 1;
        }
        else
        {
            camState = 2;
        }

        vcam = GetComponent<Cinemachine.CinemachineVirtualCameraBase>();
    }

    public void aimOn(CallbackContext context)
    {
        if(DeactiveateCamera.dollyActive == false) {
            camState = 2;
        }

        switch (camState)
        {
            case 1:

                break;

            case 2:
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
                    Reticle.SetActive(boosted);
                break;
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
