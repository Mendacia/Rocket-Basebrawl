using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fielderTargetingBallSpawner : MonoBehaviour
{
    [Header("Make sure this is actually set up on the prefab")]
    [SerializeField] private GameObject baseballPrefab = null;

    public void SpawnTheBaseballPrefabAndThrowItAtTheTarget(Vector3 oPos, Vector3 dir)
    {
        var myBaseballObject = Instantiate(baseballPrefab);
        myBaseballObject.transform.position = oPos;
        myBaseballObject.transform.LookAt(oPos + dir);
        myBaseballObject.transform.position = myBaseballObject.transform.position + new Vector3(0, 0.5f, 1);
    }
}
