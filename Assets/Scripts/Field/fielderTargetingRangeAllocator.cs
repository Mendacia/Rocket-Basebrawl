using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fielderTargetingRangeAllocator : MonoBehaviour
{
    [Header("Plug everything in please and thanks")]
    [SerializeField] private baseManager baseManagerScript;
    [SerializeField] private Transform playerPosition = null;
    private Transform recievedNextBase = null;

    public void RangeAllocatorNextBaseUpdater()
    {
        recievedNextBase = baseManagerScript.bases[baseManagerScript.nextBase];
    }

    public void GiveTheFielderATarget()
    {
        var nextBaseX = recievedNextBase.position.x;
        var nextBaseZ = recievedNextBase.position.z;
        var playerPosX = playerPosition.position.x;
        var playerPosZ = playerPosition.position.z;

        var finalX = Random.Range(nextBaseX, playerPosX);
        var finalY = Random.Range(0.2f,3);
        var finalZ = Random.Range(nextBaseZ, playerPosZ);

        var fielderTarget = new Vector3(finalX, finalY, finalZ);
    }
}
