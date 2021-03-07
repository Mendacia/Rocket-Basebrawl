using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fielderTargetingSuccessfulHit : MonoBehaviour
{
    [Header("Make sure this is actually set up on the prefab")]
    [SerializeField] private GameObject baseballPrefab = null;
    [SerializeField] private GameObject onHitEffect = null;
    private scoreUpdater myScoreUpdater;
    private scoreHolder myScoreHolder;
    private AudioSource pitchChange;
    private soundEffectHolder sfxHolder;

    private void Start()
    {
        myScoreUpdater = GameObject.Find("ScoreUpdater").GetComponent<scoreUpdater>();
        myScoreHolder = GameObject.Find("Scoreholder").GetComponent<scoreHolder>();
        pitchChange = Camera.main.GetComponent<AudioSource>();
        sfxHolder = GameObject.Find("SoundEffectHolder").GetComponent<soundEffectHolder>();
    }

    public void SpawnTheBaseballPrefabAtThePlayerAndHitItRealHard(Vector3 midPointLocation, bool sweetSpot)
    {
        //Don't worry about these causing lag, it only runs when the player hits a ball, it shouldn't be that taxing.
        if (fielderPeltingScript.pitchingLoopStarted == false)
        {
            if (TutorialGodScript.isTutorial)
            {
                sfxHolder.SilverSoundEffect();
                myScoreHolder.score = myScoreHolder.score + 1000;
            }

            if (sweetSpot && !TutorialGodScript.isTutorial)
            {
                myScoreUpdater.SweetAddToScore(true);
                sfxHolder.GoldSoundEffect();
                fielderPeltingScript.pitchingLoopStarted = true;
            }
            else if(!TutorialGodScript.isTutorial)
            {
                myScoreUpdater.HitAddToScore(true);
                sfxHolder.SilverSoundEffect();
                fielderPeltingScript.pitchingLoopStarted = true;
            }
            
        }
        else
        {
            if (TutorialGodScript.isTutorial)
            {
                sfxHolder.SilverSoundEffect();
                myScoreHolder.score = myScoreHolder.score + 1000;
            }
            if (sweetSpot)
            {
                myScoreUpdater.SweetAddToScore(false);
                sfxHolder.GoldSoundEffect();
                ChangePitchOnHit();
            }
            else
            {
                myScoreUpdater.HitAddToScore(false);
                sfxHolder.SilverSoundEffect();
                ChangePitchOnHit();
            }
        }
        
        var myLineRendererScript = gameObject.GetComponent<fielderTargetingLineRenderer>();

        //Following this is just making sure the ball goes in the right direction
        var myBaseballObject = Instantiate(baseballPrefab);
        myBaseballObject.GetComponent<fielderPeltingBallBehaviour>().ballIsActive = false;
        myBaseballObject.transform.position = midPointLocation;
        PlayHitEffect(midPointLocation);
        myBaseballObject.transform.LookAt(myLineRendererScript.originPosition - myLineRendererScript.direction); //You could change this LookAt to a position the player is looking at if you want
        myBaseballObject.transform.Rotate((Random.Range(-45, 45)), (Random.Range(-45, 45)), (Random.Range(-45, 45)), Space.Self); //If you do, remove this or change variation or whatever
        myBaseballObject.transform.position = myBaseballObject.transform.position + new Vector3(0, 0, 1);
    }

    void PlayHitEffect(Vector3 ballTransform)
    {
        Instantiate(onHitEffect, ballTransform, Quaternion.identity);
    }

    void ChangePitchOnHit()
    {
        if (pitchChange.pitch < 1)
        {
            pitchChange.pitch = pitchChange.pitch + 0.1f;
            if(pitchChange.pitch > 1)
            {
                pitchChange.pitch = 1;
            }
        }
    }
}