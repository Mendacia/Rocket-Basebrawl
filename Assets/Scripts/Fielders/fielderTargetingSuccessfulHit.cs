﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fielderTargetingSuccessfulHit : MonoBehaviour
{
    [Header("Make sure this is actually set up on the prefab")]
    [SerializeField] private GameObject baseballPrefab = null;

    public void SpawnTheBaseballPrefabAtThePlayerAndHitItRealHard(Vector3 midPointLocation)
    {
        var myLineRendererScript = gameObject.GetComponent<fielderTargetingLineRenderer>();
        var myBaseballObject = Instantiate(baseballPrefab);
        myBaseballObject.transform.position = midPointLocation;
        myBaseballObject.transform.LookAt(myLineRendererScript.originPosition - myLineRendererScript.direction);
    }
}