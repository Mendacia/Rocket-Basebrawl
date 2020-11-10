﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.SceneManagement;

public class fielderPeltingScript : MonoBehaviour
{
    [Header("These MUST be set in editor for game to work")]
    [SerializeField] private Transform player = null;
    [SerializeField] private GameObject targetingBeamPrefab;
    [SerializeField] private fielderTargetingRangeAllocator rangeAllocationScript;
    [SerializeField] private Transform pitchingPhaseTarget = null;
    [SerializeField] private scoreHolder scoreHolderObject;
    [SerializeField] private runningPhaseMovement playerStateReference = null;
    [Header("These Wait Times are in seconds")]
    [SerializeField] private float minWaitTime = 3f;
    [SerializeField] private float maxWaitTime = 6f;

    //I set these automatically please don't try to manipulate these for anything other than visibility
    public List<Transform> fieldingTeam;
    [System.NonSerialized] public int battingBallCount;
    private bool canThrow = false;
    private bool hasReadiedAThrow = false;
    private bool hasStartedThrowingSequenceAlready = false;
    private bool hasStartedPitchingSequenceAlready = false;

    private int iterator = 0;
    public static bool gameStarted = false;

    private void Start()
    {
        //Populate fieldingTeam list with the children of this gameObject
        foreach (Transform child in gameObject.transform.Find("Team"))
        {
            fieldingTeam.Add(child);
        }
        //StartCoroutine(BattingPhaseTimer());
    }

    private void Update()
    {
        //Makes the Fielders all look at the player at all times
        foreach(Transform fielder in fieldingTeam)
        {
            fielder.LookAt(player);
        }

        if (canThrow == true)
        {
            ReadyThrow();
        }

        if(scoreHolderObject.score >= 1 && !gameStarted)
        {
            playerStateReference.playerState = 2;
            gameStarted = true;
        }

        if(playerStateReference.playerState == 1 && !hasStartedPitchingSequenceAlready)
        {
            hasStartedPitchingSequenceAlready = true;
            StartCoroutine(BattingPhaseTimer());
        }
    }
    public void startPeltingLoop()
    {
        if (hasStartedThrowingSequenceAlready == false)
        {
            hasStartedThrowingSequenceAlready = true;
            StartCoroutine(ThrowDelay());
        }
    }

    IEnumerator ThrowDelay()
    {
        yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
        canThrow = true;
        StartCoroutine(ThrowDelay());
    }

    public void battingPhaseThrow()
    {
        battingBallCount++;
        if (battingBallCount < 4)
        {
            var thePitcher = fieldingTeam[0];
            var myBeamScript = Instantiate(targetingBeamPrefab, Vector3.zero, Quaternion.identity).GetComponent<fielderTargetingLineRenderer>();
            myBeamScript.originPosition = thePitcher.position;
            myBeamScript.playerTransform = player.transform;
            myBeamScript.direction = ((pitchingPhaseTarget.position + (Random.insideUnitSphere)) - thePitcher.position).normalized;
           
        }
        else
        {
            //commit die.
        }
    }

    public IEnumerator BattingPhaseTimer()
    {
        yield return new WaitForSeconds(3);
        iterator++;
        if (scoreHolderObject.score >= 1)
        {
            //Start the main game and stop this coroutine
            startPeltingLoop();
            StopCoroutine(BattingPhaseTimer());
        }
        else if (iterator >= 4)
        {
            //if they havent hit the ball, then kill them
            SceneManager.LoadScene(0);
        }
        
        else
        {
            battingPhaseThrow();
            StartCoroutine(BattingPhaseTimer());
        }
    }

    private void ReadyThrow()
    {
        int numberOfBallsToThrow;
        List<Transform> chosenFielders = new List<Transform>();
        if (hasReadiedAThrow == false)
        {

            //Block to choose how many balls to throw
            var throwCountValue = Random.Range(0, 9);
            
            switch (throwCountValue)
            {
                case 9:
                    numberOfBallsToThrow = 3;
                    break;
                case 8:
                    numberOfBallsToThrow = 2;
                    break;
                default:
                    numberOfBallsToThrow = 1;
                    break;
            }

            //The variable "numberOfBallsToThrow" is now holding how many balls the fielders will throw, we now need to find who will throw them
            while (numberOfBallsToThrow > 0)
            {
                chosenFielders.Add(fieldingTeam[Random.Range(0, fieldingTeam.Count)]);
                numberOfBallsToThrow--;
                rangeAllocationScript.firstFielder = true;
            }

            //Cool, we now have a list populated with the fielders that will throw the ball. Now all we need to do is, get them to do that...
            foreach (Transform fielder in chosenFielders)
            {
                var myBeamScript = Instantiate(targetingBeamPrefab, Vector3.zero, Quaternion.identity).GetComponent<fielderTargetingLineRenderer>();
                myBeamScript.originPosition = fielder.position;
                rangeAllocationScript.GiveTheFielderATarget();
                myBeamScript.direction = ((rangeAllocationScript.finalTargetPosition) - fielder.position).normalized;
                rangeAllocationScript.firstFielder = false;
                myBeamScript.playerTransform = player.transform;
            }
            canThrow = false;
        }
    }
}
