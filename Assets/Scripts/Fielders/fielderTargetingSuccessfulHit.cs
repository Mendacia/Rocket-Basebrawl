﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fielderTargetingSuccessfulHit : MonoBehaviour
{
    [Header("Make sure this is actually set up on the prefab")]
    [SerializeField] private GameObject baseballPrefab = null;

    private BaseballEffectHolder effectHolder;
    private scoreUpdater myScoreUpdater;
    private scoreHolder myScoreHolder;
    private AudioSource pitchChange;

    private void Start()
    {
        effectHolder = GameObject.Find("BaseballEffectHolder").GetComponent<BaseballEffectHolder>();
        myScoreUpdater = GameObject.Find("ScoreUpdater").GetComponent<scoreUpdater>();
        myScoreHolder = GameObject.Find("Scoreholder").GetComponent<scoreHolder>();
        pitchChange = Camera.main.GetComponent<AudioSource>();
    }

    public void SpawnTheBaseballPrefabAtThePlayerAndHitItRealHard(Vector3 midPointLocation, bool sweetSpot)
    {
        //Don't worry about these causing lag, it only runs when the player hits a ball, it shouldn't be that taxing.
        if (fielderPeltingScript.pitchingLoopStarted == false)
        {
            if (TutorialGodScript.isTutorial)
            {
                myScoreHolder.score = myScoreHolder.score + 1000;
            }

            if (sweetSpot && !TutorialGodScript.isTutorial)
            {
                myScoreUpdater.SweetAddToScore(true);
                fielderPeltingScript.pitchingLoopStarted = true;
            }
            else if(!TutorialGodScript.isTutorial)
            {
                myScoreUpdater.HitAddToScore(true);
                fielderPeltingScript.pitchingLoopStarted = true;
            }
            
        }
        else
        {
            if (TutorialGodScript.isTutorial)
            {
                myScoreHolder.score = myScoreHolder.score + 1000;
            }
            if (sweetSpot)
            {
                myScoreUpdater.SweetAddToScore(false);
            }
            else
            {
                myScoreUpdater.HitAddToScore(false);
            }

            if (!effectHolder.inPPTime)
            {
                effectHolder.OnHitTurnOnPP();
                //Enable this line to turn on OnHit effects
                effectHolder.inPPTime = true;
            }
            //effectHolder.vignetteValue = effectHolder.vignetteValue - 0.08f;
            if(pitchChange.pitch < 1)
            {
                //pitchChange.pitch = pitchChange.pitch + 0.1f;
            }
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