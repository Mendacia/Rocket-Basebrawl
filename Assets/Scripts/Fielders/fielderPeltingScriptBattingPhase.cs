using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fielderPeltingScriptBattingPhase : MonoBehaviour
{
    [Header("Set this stuff up please")]
    [SerializeField] private GameObject targetingBeamPrefab;
    [SerializeField] private Transform pitcher;
    [SerializeField] private Transform homeBaseTarget;

    //Things I've already set up:
    private BallList ballGodScript; //This is holy
    private Transform player;
    private Transform pitcherTarget;

    /* -----------
     Code below 
     ------------*/

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        ballGodScript = GameObject.Find("BallGod").GetComponent<BallList>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N)) //TEMPORARY PLEASE CHANGE THIS RIGHTNOWMEDIATELY
        {
            ReadyThrow();
        }
        if (Input.GetKeyDown(KeyCode.B)) //TEMPORARY PLEASE CHANGE THIS RIGHTNOWMEDIATELY
        {
            Debug.Log("B Pressed 1");
            InitializeBattingPhase(homeBaseTarget);
            Debug.Log("B Pressed 2");
        }
    }

    public void InitializeBattingPhase(Transform recievedTarget)
    {
        pitcherTarget = recievedTarget;
        ballListPopulater();
        StartCoroutine(ThrowDelay());
    }

    private void ballListPopulater()
    {
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

    public IEnumerator ThrowDelay()
    {
        yield return new WaitForSeconds(3);
        ReadyThrow();
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
        foreach (Transform fielder in ball.myFielders)
        {
            var myBeamScript = Instantiate(targetingBeamPrefab, Vector3.zero, Quaternion.identity).GetComponent<fielderTargetingLineRenderer>();
            myBeamScript.SetUp(ball.myThrowSpeed, ball.myIndex, player.transform, ball.myFielders[0], (pitcherTarget.position - fielder.position).normalized);

            StartCoroutine(ThrowDelay());
        }
    }
}

