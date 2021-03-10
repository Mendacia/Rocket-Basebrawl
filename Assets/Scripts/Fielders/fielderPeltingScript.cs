using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fielderPeltingScript : MonoBehaviour
{
    [Header("Set this prefab up please")]
    [SerializeField] private GameObject targetingBeamPrefab;

    //Things I've already set up:
    private BallList masterBallList; //This is holy
    private Transform player;
    private fielderProgressionBasedAccuracyScript rangeAllocationScript;

    [Header("Determines how many balls the fielders will throw between this base and the next")]
    [SerializeField] private int ballsToThrowMinimum = 10;
    [SerializeField] private int ballsToThrowMaximum = 15;
    [SerializeField] private int higherTauntBallCountReductionFactor = 3;
    private int finalBallsToThrow;

    private List<Transform> fieldingTeam;
    private int fielderTauntLevel = 0;

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
        masterBallList = GameObject.Find("BallGod").GetComponent<BallList>();
        fieldingTeam = new List<Transform>();
    }

    private void Start()
    {
        //Populate fieldingTeam list with the children of this gameObject
        foreach (Transform child in gameObject.transform.Find("Team"))
        {
            fieldingTeam.Add(child.transform);
        }
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
            startPeltingLoop();
            Debug.Log("B Pressed 2");
        }

        //Makes the Fielders all look at the player at all times
        foreach (Transform fielder in fieldingTeam)
        {
            fielder.LookAt(player);
        }
    }

    public void fielderTauntLevelIncreaser()
    {
        fielderTauntLevel++;
    }

    public void startPeltingLoop()
    {
        bool holdThis = true;
        if (holdThis) //TEMPORARY PLEASE CHANGE THIS RIGHTNOWMEDIATELY
        {
            ballListPopulater(3);

            //This will make the fielders throw the first ball in their list. This will need to be called elsewhere later.
            ReadyThrow();
        }
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
            masterBallList.AddThisBallToTheList(myIndex, myTauntLevel, deliveredFielder);

            List<Transform> chosenFielders = new List<Transform>();
            for(int i2=masterBallList.masterBallList[i].extraBallCount; i2>=0; i2--)
            {
                chosenFielders.Add(fieldingTeam[Random.Range(0, fieldingTeam.Count)]);
            }
            masterBallList.AddFieldersToTheBall(chosenFielders, myIndex);
        }
    }

    public IEnumerator ThrowDelay()
    {
        yield return new WaitForSeconds(Random.Range(1, 1.5f));
    }

    private void ReadyThrow()
    {
        var ball = masterBallList.CallForBall();
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
    }
}
