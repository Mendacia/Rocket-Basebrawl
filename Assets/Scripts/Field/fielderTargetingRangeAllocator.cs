using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fielderTargetingRangeAllocator : MonoBehaviour
{
    [Header("Plug everything in please and thanks")]
    [SerializeField] private baseManager baseManagerScript;
    [SerializeField] private Transform playerPosition = null;
    [Header("Minimum and Maximum 'Y' Values for targeting")]
    [SerializeField] private float targetingMinimumY;
    [SerializeField] private float targetingMaximumY;
    [Header("This is the distance the extra ball will be from the first")]
    [SerializeField] private float extraBallSphereRadius;
    [System.NonSerialized] public Vector3 finalTargetPosition;
    [System.NonSerialized] public bool firstFielder = true;
    private Transform recievedNextBase = null;

    public void RangeAllocatorNextBaseUpdater(int sentNextBase)
    {
        recievedNextBase = baseManagerScript.GetBases()[sentNextBase];
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.B))
        {
            GiveTheFielderATarget();
        }
    }

    public void GiveTheFielderATarget()
    {
        if (firstFielder == true)
        {
            var multiplierHolder = (recievedNextBase.position - playerPosition.position);
            var fielderTarget = (playerPosition.position + (Random.value / 3) * multiplierHolder);
            fielderTarget.y = (Random.Range(targetingMinimumY, targetingMaximumY));
            finalTargetPosition = fielderTarget;
        }
        else
        {
            finalTargetPosition = ((finalTargetPosition + Random.insideUnitSphere * extraBallSphereRadius));
        }
    }
}
