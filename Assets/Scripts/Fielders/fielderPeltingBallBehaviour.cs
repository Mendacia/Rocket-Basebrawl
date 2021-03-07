﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fielderPeltingBallBehaviour : MonoBehaviour
{
    public float ballSpeed, pingSpeed;
    public bool isHittable = true;
    public bool ballIsActive = true;

    private scoreUpdater myScoreUpdater;
    private AudioSource pitchChange;

    void Start()
    {
        myScoreUpdater = GameObject.Find("ScoreUpdater").GetComponent<scoreUpdater>();
        pitchChange = Camera.main.GetComponent<AudioSource>();

        var body = GetComponent<Rigidbody>();
        body.velocity = transform.forward * ballSpeed * 100 * Time.deltaTime;
        StartCoroutine(Expire());
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (myScoreUpdater.canScore)
        {
            case true:
                if (ballIsActive == true && fielderPeltingScript.pitchingLoopStarted == true && !TutorialGodScript.isTutorial)
                {
                    myScoreUpdater.SubtractFromScore();
                    pitchChange.pitch = pitchChange.pitch - 0.05f;
                    
                    ballIsActive = false;
                }
                break;

            case false:
                
                break;
        }
    }

    //Not being used - Delete?
    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BallHit")
        {
            var body = GetComponent<Rigidbody>();
            Vector3 pingDirection = new Vector3(Random.Range(0, -5), Random.Range(0, 2), Random.Range(-5, 5));
            body.velocity += pingDirection * (ballSpeed / 2) * pingSpeed * Time.deltaTime;
            isHittable = false;
            Detector.ballCols.Remove(this.gameObject.transform);
        }
    }*/

    IEnumerator Expire()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
