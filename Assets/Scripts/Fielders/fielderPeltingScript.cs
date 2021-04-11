using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fielderPeltingScript : MonoBehaviour
{
    [Header("Set this prefab up please")]
    [SerializeField] private GameObject targetingBeamPrefab;
    [SerializeField] private GameObject multiTargetingBeamPrefab;
    [SerializeField] private GameObject arcTargetingBeamPrefab;
    [SerializeField] private GameObject multiBeamHolder;
    [SerializeField] private HUDManager hUDScript;
    [SerializeField] private Transform ArrowHolderOnFloorCanvas = null;

    //Things I've already set up:
    private BallList ballGodScript; //This is holy
    private Transform player;
    private fielderProgressionBasedAccuracyScript rangeAllocationScript;
    private fielderScatterAccuracyScript scatterAllocationScript;

    [System.NonSerialized] public bool activateBase = false;

    [Header("Determines how many balls the fielders will throw between this base and the next")]
    [SerializeField] private int ballsToThrowMinimum = 10;
    [SerializeField] private int ballsToThrowMaximum = 15;
    [SerializeField] private int higherTauntBallCountReductionFactor = 3;
    private int finalBallsToThrow;

    [System.NonSerialized] public List<Transform> fieldingTeam;
    private int fielderTauntLevel = 0;
    private bool pleaseStop = false;

    //Cheats Below
    [System.NonSerialized] public bool Gilded = false;

    public bool canThrow = false; //Delete this

    /* -----------
     Code below 
     ------------*/

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rangeAllocationScript = GameObject.Find("AccuracyTarget").GetComponent<fielderProgressionBasedAccuracyScript>();
        scatterAllocationScript = GameObject.Find("AccuracyTarget").GetComponent<fielderScatterAccuracyScript>();
        ballGodScript = GameObject.Find("BallGod").GetComponent<BallList>();
        fieldingTeam = new List<Transform>();

        //Populate fieldingTeam list with the children of this gameObject
        foreach (Transform child in gameObject.transform.Find("Team"))
        {
            fieldingTeam.Add(child.transform);
        }
    }

    private void Update()
    {
        //Makes the Fielders all look at the player at all times
        foreach (Transform fielder in fieldingTeam)
        {
            fielder.LookAt(player);
        }
    }

    public void StopMe()
    {
        pleaseStop = true;
        StopCoroutine(ThrowDelay(0));
    }

    public void StartMe()
    {
        pleaseStop = false;
    }

    public void fielderTauntLevelIncreaser()
    {
        fielderTauntLevel++;
    }

    public void InitializeRunningPhase()
    {
        ballListPopulater(fielderTauntLevel);
        StartCoroutine(ThrowDelay(2f));
    }

    private void ballListPopulater(int recievedTauntLevel)
    {
        int tempBallsToThrow = Random.Range(ballsToThrowMinimum, ballsToThrowMaximum);
        int myTauntLevel;
        int myIndex;
        tempBallsToThrow += (Random.Range(5, 8) * recievedTauntLevel);
        finalBallsToThrow = tempBallsToThrow -= higherTauntBallCountReductionFactor * recievedTauntLevel;
        finalBallsToThrow = Mathf.Clamp(finalBallsToThrow, 0, 50);

        Debug.Log("Populating with " + finalBallsToThrow + " Balls");

        //Determines what taunt level the ball will be
        for (int i=0; i<finalBallsToThrow; i++)
        {

            int TauntLevelCalculator = Random.Range(0, recievedTauntLevel * 10);
            if (TauntLevelCalculator < 5)
            {
                myTauntLevel = 0;
            }
            else if (TauntLevelCalculator >= 5 && TauntLevelCalculator < 15)
            {
                myTauntLevel = 1;
            }
            else if (TauntLevelCalculator >= 15 && TauntLevelCalculator < 25)
            {
                myTauntLevel = 2;
            }
            else
            {
                myTauntLevel = 3;
            }
            
            myIndex = i;

            //Finally adding the ball to the list
            var deliveredFielder = (fieldingTeam[Random.Range(0, fieldingTeam.Count)]);
            ballGodScript.AddThisBallToTheList(myIndex, myTauntLevel, deliveredFielder);

            List<Transform> chosenFielders = new List<Transform>();
            for(int i2=ballGodScript.masterBallList[i].extraBallCount; i2>=0; i2--)
            {
                chosenFielders.Add(fieldingTeam[Random.Range(0, fieldingTeam.Count)]);
            }
            ballGodScript.AddFieldersToTheBall(chosenFielders, myIndex);
        }
    }

    public IEnumerator ThrowDelay(float requestedDelay)
    {
        yield return new WaitForSeconds(Random.Range(requestedDelay - 0.2f, requestedDelay + 0.2f));
        if (pleaseStop)
        {
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
        switch (ball.myType)
        {
            case ballType.SCATTER:
                ReadyThrowScatter(ball);
                break;
            case ballType.MULTI:
                ReadyThrowMulti(ball);
                break;
            case ballType.ARC:
                ReadyThrowArc(ball);
                break;
            default:
                ReadyThrowStandard(ball);
                break;
        }
        if (ball.myIndex != ballGodScript.masterBallList.Count - 1)
        {
            StartCoroutine(ThrowDelay(ballGodScript.masterBallList[ball.myIndex + 1].myReadySpeed));
        }
    }

    private void ReadyThrowScatter(masterBallStruct ball)
    {
        var folder = Instantiate(multiBeamHolder, Vector3.zero, Quaternion.identity);
        var i = 0;
        Vector3 initialTarget = Vector3.zero;
        foreach (Transform fielder in ball.myFielders)
        {
            var myTarget = scatterAllocationScript.GiveTheFielderATarget(i, initialTarget);
            var myBeamScript = Instantiate(multiTargetingBeamPrefab, folder.transform).GetComponent<fielderMultiTargetingLineRenderer>();
            myBeamScript.myParent = folder.GetComponent<fielderMultiBeamParent>();
            folder.GetComponent<fielderMultiBeamParent>().index = ball.myIndex;
            myBeamScript.SetUp(ball.myThrowSpeed, player.transform, ball.myFielders[0], (myTarget - fielder.position).normalized, ArrowHolderOnFloorCanvas);
            initialTarget = myTarget;
            i++;
        }
    }
    private void ReadyThrowMulti(masterBallStruct ball)
    {
        var folder = Instantiate(multiBeamHolder, Vector3.zero, Quaternion.identity);
        var i = 0;
        foreach (Transform fielder in ball.myFielders)
        {
            var target = rangeAllocationScript.GiveTheFielderATarget(true, ball.myFielders[i]);
            var myBeamScript = Instantiate(multiTargetingBeamPrefab, folder.transform).GetComponent<fielderMultiTargetingLineRenderer>();
            myBeamScript.myParent = folder.GetComponent<fielderMultiBeamParent>();
            folder.GetComponent<fielderMultiBeamParent>().index = ball.myIndex;
            myBeamScript.SetUp(ball.myThrowSpeed, player.transform, ball.myFielders[i], (target - fielder.position).normalized, ArrowHolderOnFloorCanvas);
            i++;

            //Cheating
            if (Gilded)
            {
                myBeamScript.GildMe();
            }
        }
    }
    private void ReadyThrowArc(masterBallStruct ball)
    {
        var target = rangeAllocationScript.GiveTheFielderATarget(true, ball.myFielders[0]);
        var myBeamScript = Instantiate(arcTargetingBeamPrefab, Vector3.zero, Quaternion.identity).GetComponent<fielderArcTargetingLineRenderer>();
        myBeamScript.SetUp(ball.myThrowSpeed, ball.myIndex, player.transform, ball.myFielders[0], (target - ball.myFielders[0].position).normalized, ArrowHolderOnFloorCanvas);

        //Cheating
        if (Gilded)
        {
           myBeamScript.GildMe();
        }
    }
    private void ReadyThrowStandard(masterBallStruct ball)
    {
        var target = rangeAllocationScript.GiveTheFielderATarget(true, ball.myFielders[0]);
        var myBeamScript = Instantiate(targetingBeamPrefab, Vector3.zero, Quaternion.identity).GetComponent<fielderTargetingLineRenderer>();
        myBeamScript.SetUp(ball.myThrowSpeed, ball.myIndex, player.transform, ball.myFielders[0], (target - ball.myFielders[0].position).normalized, ArrowHolderOnFloorCanvas);

        //Cheating
        if (Gilded)
        {
            myBeamScript.GildMe();
        }

    }



    public int GetFielderTauntLevel() => fielderTauntLevel;
}
