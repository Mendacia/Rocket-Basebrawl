using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fielderTargetingBallSpawner : MonoBehaviour
{
    [Header("Make sure this is actually set up on the prefab")]
    [SerializeField] private GameObject baseballPrefab = null;
    public void SpawnTheBaseballPrefabAndThrowItAtTheTarget()
    {
        var myLineRendererScript = gameObject.GetComponent<fielderTargetingLineRenderer>();
        var myBaseballObject = Instantiate(baseballPrefab, myLineRendererScript.fielderPosition);

        myBaseballObject.transform.LookAt(myLineRendererScript.fielderRaycastHitPosition);
    }
}
