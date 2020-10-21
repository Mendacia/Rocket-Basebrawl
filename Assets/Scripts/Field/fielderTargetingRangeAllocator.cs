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
    [System.NonSerialized] public int recievedChosenFielderCount;
    private Transform recievedNextBase = null;

    public void RangeAllocatorNextBaseUpdater(int sentNextBase)
    {
        recievedNextBase = baseManagerScript.bases[sentNextBase];
        Debug.Log(sentNextBase);
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
        //Alright so I know this one line looks intimidating, but it's just randomizing the position of:
        //X and Z between the player position and the next base
        //Y between the maximum and minimum "Y" values set in inspector.
        var fielderTarget = new Vector3(Random.Range(recievedNextBase.position.x, playerPosition.position.x), Random.Range(targetingMinimumY, targetingMaximumY), Random.Range(recievedNextBase.position.z, playerPosition.position.z));
        GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = fielderTarget;
    }
}
