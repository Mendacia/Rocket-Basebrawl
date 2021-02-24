﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fielderTargetingSuccessfulHit : MonoBehaviour
{
    [Header("Make sure this is actually set up on the prefab")]
    [SerializeField] private GameObject baseballPrefab = null;

    private BaseballEffectHolder effectHolder;
    private scoreHolder scoreHolderReference;

    private void Start()
    {
        effectHolder = GameObject.Find("BaseballEffectHolder").GetComponent<BaseballEffectHolder>();
        scoreHolderReference = GameObject.Find("Scoreholder").GetComponent<scoreHolder>();
    }

    public void SpawnTheBaseballPrefabAtThePlayerAndHitItRealHard(Vector3 midPointLocation)
    {
        //Don't worry about these causing lag, it only runs when the player hits a ball, it shouldn't be that taxing.
        if (fielderPeltingScript.pitchingLoopStarted == false)
        {
            scoreHolderReference.score++;
            
            fielderPeltingScript.pitchingLoopStarted = true;
        }
        else
        {
            scoreHolderReference.score++;
            
            if (!effectHolder.inPPTime)
            {
                effectHolder.OnHitTurnOnPP();
                //Enable this line to turn on OnHit effects
                effectHolder.inPPTime = true;
            }
            effectHolder.vignetteValue = effectHolder.vignetteValue - 0.08f;
        }
        
        var myLineRendererScript = gameObject.GetComponent<fielderTargetingLineRenderer>();

        //Following this is just making sure the ball goes in the right direction
        var myBaseballObject = Instantiate(baseballPrefab);
        myBaseballObject.GetComponent<fielderPeltingBallBehaviour>().ballIsActive = false;
        myBaseballObject.transform.position = midPointLocation;
        effectHolder.PlayHitEffect(midPointLocation);
        myBaseballObject.transform.LookAt(myLineRendererScript.originPosition - myLineRendererScript.direction); //You could change this LookAt to a position the player is looking at if you want
        myBaseballObject.transform.Rotate((Random.Range(-45, 45)), (Random.Range(-45, 45)), (Random.Range(-45, 45)), Space.Self); //If you do, remove this or change variation or whatever
        myBaseballObject.transform.position = myBaseballObject.transform.position + new Vector3(0, 0, 1);
    }
}