using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineBlendLookAtFielders : MonoBehaviour
{
    [SerializeField] private AimOnKeypress aiming = null;
    [SerializeField] private aimModeSnapping snapping = null;
    [SerializeField] private GameObject vcamMaster = null;
    [SerializeField] private CinemachineVirtualCamera vcam = null;
    private Transform fielderLocation;

    void Update()
    {
        fielderLocation = snapping.fielderPosition;

        if(aiming.boosted == true && fielderLocation != null)
        {
            vcamMaster.SetActive(true);
            vcam.LookAt = fielderLocation;
            Debug.Log("Hey I'm doing a thing!");
        }
        else
        {
            vcamMaster.SetActive(false);
        }
    }
    /*
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
    }*/
}
