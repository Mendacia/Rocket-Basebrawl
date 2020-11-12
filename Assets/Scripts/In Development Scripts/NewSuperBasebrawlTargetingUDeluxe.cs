﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSuperBasebrawlTargetingUDeluxe : MonoBehaviour
{
    [SerializeField] private Transform playerPosition = null;
    [SerializeField] private baseManager baseManagerScript = null;
    [SerializeField] private float targetingSphereScale = 1f;
    [SerializeField] private float targetingDistanceMaximum = 20f;
    private Transform recievedNextBase = null;
    private Vector3 finalTargetPosition = Vector3.zero;
    private int nextBaseInt = 0;

    public void NewTargetingNextBaseUpdater(int sentNextBase)
    {
        recievedNextBase = baseManagerScript.GetBases()[sentNextBase];
        nextBaseInt = sentNextBase;
    }

    void Update()
    {
        /*var multiplierHolder = (recievedNextBase.position - playerPosition.position);
        var fielderTarget = (playerPosition.position + 0.3f * multiplierHolder);
        fielderTarget.y = 1;
        finalTargetPosition = fielderTarget;

        transform.position = finalTargetPosition;*/

        var targetingDistanceUpdated = (targetingDistanceMaximum * baseManagerScript.percentageOfRunRemaining);
        gameObject.transform.position = playerPosition.position;
        transform.LookAt(recievedNextBase);
        gameObject.transform.Translate(0, 0, targetingDistanceUpdated);
        var extraDistance = Vector3.Distance(recievedNextBase.position, gameObject.transform.position);
        if (Vector3.Distance(playerPosition.position, recievedNextBase.position) < Vector3.Distance(playerPosition.position, gameObject.transform.position) && nextBaseInt != 0 && nextBaseInt != baseManagerScript.GetBases().Count - 1)
        {
            gameObject.transform.position = recievedNextBase.position;
            transform.LookAt(baseManagerScript.GetBases()[nextBaseInt + 1]);
            gameObject.transform.Translate(0, 0, extraDistance);
        }
        else if (Vector3.Distance(playerPosition.position, recievedNextBase.position) < Vector3.Distance(playerPosition.position, gameObject.transform.position) && nextBaseInt == baseManagerScript.GetBases().Count - 1)
        {
            gameObject.transform.position = recievedNextBase.position;
            transform.LookAt(baseManagerScript.GetBases()[0]);
            gameObject.transform.Translate(0, 0, extraDistance);
        }
        gameObject.transform.position = new Vector3 (gameObject.transform.position.x, 1, gameObject.transform.position.z);

        //Updates the size of the targeting sphere
        gameObject.transform.localScale = new Vector3 (baseManagerScript.percentageOfRunRemaining * targetingSphereScale, baseManagerScript.percentageOfRunRemaining * targetingSphereScale, baseManagerScript.percentageOfRunRemaining * targetingSphereScale);
    }
}