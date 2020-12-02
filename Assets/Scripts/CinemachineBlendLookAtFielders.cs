using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineBlendLookAtFielders : MonoBehaviour
{
    [SerializeField] private AimOnKeypress aiming = null;
    [SerializeField] private fielderPeltingScript fielderPelting = null;
    [SerializeField] private CinemachineVirtualCamera vcam = null;
    [SerializeField] private GameObject player = null;
    private Transform fielderLocation;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fielderLocation = GetClosestFielder(fielderPelting.fieldingTeam, player.transform);

        if(aiming.boosted == true)
        {
            vcam.LookAt = fielderLocation;
            Debug.Log("Hey I'm doing a thing!");
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
