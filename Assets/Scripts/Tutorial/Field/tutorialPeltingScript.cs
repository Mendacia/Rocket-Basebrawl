using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialPeltingScript : MonoBehaviour
{
    [Header("Set this prefab up please")]
    [SerializeField] private GameObject targetingBeamPrefab;
    [SerializeField] private HUDManager hUDScript;

    //Things I've already set up:
    private BallList ballGodScript; //This is holy
    private Transform player;
    private fielderProgressionBasedAccuracyScript rangeAllocationScript;

    [System.NonSerialized] public bool activateBase = false;

    [Header("Determines how many balls the fielders will throw between this base and the next")]
    [SerializeField] private int ballsToThrowMinimum = 10;
    [SerializeField] private int ballsToThrowMaximum = 15;
    [SerializeField] private int higherTauntBallCountReductionFactor = 3;
    private int finalBallsToThrow;

    private List<Transform> fieldingTeam;
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
        int tempBallsToThrow = 3;
        int myTauntLevel = 0;
        int myIndex;
        finalBallsToThrow = tempBallsToThrow;
        finalBallsToThrow = Mathf.Clamp(finalBallsToThrow, 0, 50);

        Debug.Log("Populating with " + finalBallsToThrow + " Balls");

        //Determines what taunt level the ball will be
        for (int i=0; i<finalBallsToThrow; i++)
        {            
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
        Debug.Log("ReadyThrow was called");
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
            var target = rangeAllocationScript.GiveTheFielderATarget(true, ball.myFielders[0]);
            var myBeamScript = Instantiate(targetingBeamPrefab, Vector3.zero, Quaternion.identity).GetComponent<fielderTargetingLineRenderer>();
            myBeamScript.SetUp(ball.myThrowSpeed, ball.myIndex, player.transform, ball.myFielders[0], (target - fielder.position).normalized);

            //Cheating
            if (Gilded)
            {
                myBeamScript.GildMe();
            }
        }
        if (ball.myIndex != ballGodScript.masterBallList.Count - 1)
        {
            StartCoroutine(ThrowDelay(ballGodScript.masterBallList[ball.myIndex + 1].myReadySpeed));
        }
    }

    public int GetFielderTauntLevel() => fielderTauntLevel;
}
