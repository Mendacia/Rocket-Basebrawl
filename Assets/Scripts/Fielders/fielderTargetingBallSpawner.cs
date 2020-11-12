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
        var myBaseballObject = Instantiate(baseballPrefab);
        myBaseballObject.transform.position = myLineRendererScript.originPosition;
        myBaseballObject.transform.LookAt(myLineRendererScript.originPosition + myLineRendererScript.direction);
        myBaseballObject.transform.position = myBaseballObject.transform.position + new Vector3(0, 0.5f, 1);
    }
}
