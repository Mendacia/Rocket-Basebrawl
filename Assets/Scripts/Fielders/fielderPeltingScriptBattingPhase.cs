using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fielderPeltingScriptBattingPhase : MonoBehaviour
{
    [Header("Set this stuff up please")]
    [SerializeField] private GameObject targetingBeamPrefab;
    [SerializeField] private Transform pitcher;
    //Yes I hate this public too
    public Transform homeBaseTarget;

    //Things I've already set up:
    private BallList ballGodScript; //This is holy
    private Transform player;
    private Transform pitcherTarget;
    private bool pleaseStopRightNowPlease = false;

    /* -----------
     Code below 
     ------------*/

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        ballGodScript = GameObject.Find("BallGod").GetComponent<BallList>();
        //InitializeBattingPhase(homeBaseTarget);
        //Hey this is being initialised under the FIRSTPITCH state in WorldStateMachine!
    }


    public void InitializeBattingPhase(Transform recievedTarget)
    {
        pitcherTarget = recievedTarget;
        ballListPopulater();
        StartCoroutine(ThrowDelay());
    }

    private void ballListPopulater()
    {
        Debug.Log("Why the fuck is this triggering twice????");
        int ballsToThrow = 3;
        int myTauntLevel;
        int myIndex;

        Debug.Log("Populating with " + ballsToThrow + " Balls");

        //Determines what taunt level the ball will be
        for (int i = 0; i < ballsToThrow; i++)
        {
            myTauntLevel = 0;
            myIndex = i;

            ballGodScript.AddThisBallToTheList(myIndex, myTauntLevel, pitcher);
        }
    }

    public void StopMe()
    {
        pleaseStopRightNowPlease = true;
        StopCoroutine(ThrowDelay());
    }

    public void StartMe()
    {
        pleaseStopRightNowPlease = false;
    }

    public IEnumerator ThrowDelay()
    {
        yield return new WaitForSeconds(3);
        if (pleaseStopRightNowPlease)
        {
            //Fucking... Stop...
        }
        else
        {
            ReadyThrow();
        }
    }

    private void ReadyThrow()
    {
        var ball = ballGodScript.CallForBall();
        if (ball.myIndex != -1)
        {
            ReadyThrow2(ball);
        }
    }

    private void ReadyThrow2(masterBallStruct ball)
    {
        var myBeamScript = Instantiate(targetingBeamPrefab, Vector3.zero, Quaternion.identity).GetComponent<fielderTargetingLineRenderer>();
        myBeamScript.SetUp(ball.myThrowSpeed, ball.myIndex, player.transform, pitcher, (pitcherTarget.position - pitcher.position).normalized);

        StartCoroutine(ThrowDelay());
        
    }
}

