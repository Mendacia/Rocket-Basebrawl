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
    private scoreHolder myScoreHolder;
    private scoreUpdater myScoreUpdater;
    private arrowIconManager uIArrowManager;
    private ballIconManager uIBallManager;
    private fielderProgressionBasedAccuracyScript rangeAllocationScript;

    [Header("Determines how many balls the fielders will throw between this base and the next")]
    [SerializeField] private int ballsToThrowMinimum = 10;
    [SerializeField] private int ballsToThrowMaximum = 15;
    [SerializeField] private int higherTauntBallCountReductionFactor = 3;
    private int finalBallsToThrow;

    private List<Transform> fieldingTeam;
    private int fielderTauntLevel = 0;

    //Ball Variables, note that this is not everything, as most is referenced in an external script
    [System.Serializable] public struct ballStruct
    {
        public int myIndex;
        public int myTauntLevel;
        public GameObject uIIcon;
        public bool fired;
    }
    public List<ballStruct> upcomingBallList;

    //Cheats Below
    [System.NonSerialized] public bool Gilded = false;

    public bool canThrow = false; //Delete this

    /* -----------
     Code below 
     ------------*/

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        myScoreHolder = GameObject.Find("Scoreholder").GetComponent<scoreHolder>();
        myScoreUpdater = GameObject.Find("ScoreUpdater").GetComponent<scoreUpdater>();
        rangeAllocationScript = GameObject.Find("AccuracyTarget").GetComponent<fielderProgressionBasedAccuracyScript>();
        masterBallList = GameObject.Find("BallGod").GetComponent<BallList>();

        var uI = GameObject.Find("UI");
        uIArrowManager = uI.GetComponentInChildren<arrowIconManager>();
        uIBallManager = uI.GetComponentInChildren<ballIconManager>();

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
        //Makes the Fielders all look at the player at all times
        foreach(Transform fielder in fieldingTeam)
        {
            fielder.LookAt(player);
        }

        if (Input.GetKeyDown(KeyCode.N)) //TEMPORARY PLEASE CHANGE THIS RIGHTNOWMEDIATELY
        {
            ReadyThrow();
        }
    }

    public void fielderTauntLevelIncreaser()
    {
        fielderTauntLevel++;
    }

    public void startPeltingLoop()
    {
        if (Input.GetKeyDown(KeyCode.B)) //TEMPORARY PLEASE CHANGE THIS RIGHTNOWMEDIATELY
        {
            ballListPopulater(fielderTauntLevel);

            //This will make the fielders throw the first ball in their list. This will need to be called elsewhere later.
            ReadyThrow();
        }
    }

    private void ballListPopulater(int recievedTauntLevel)
    {
        int tempBallsToThrow = Random.Range(ballsToThrowMinimum, ballsToThrowMaximum);
        tempBallsToThrow += (Random.Range(5, 8) * recievedTauntLevel);
        finalBallsToThrow = tempBallsToThrow -= higherTauntBallCountReductionFactor * recievedTauntLevel;
        finalBallsToThrow = Mathf.Clamp(finalBallsToThrow, 0, 50);

        //Determines what taunt level the ball will be
        for (int i=0; i<finalBallsToThrow; i++)
        {
            ballStruct newBall = new ballStruct();

            int TauntLevelCalculator = Random.Range(0, recievedTauntLevel * 10);
            if (TauntLevelCalculator < 5)
            {
                newBall.myTauntLevel = 0;
            }
            else if (TauntLevelCalculator >= 5 && TauntLevelCalculator < 15)
            {
                newBall.myTauntLevel = 1;
            }
            else if (TauntLevelCalculator >= 15 && TauntLevelCalculator < 25)
            {
                newBall.myTauntLevel = 2;
            }
            else
            {
                newBall.myTauntLevel = 3;
            }

            //Use the uIBallManager to assign this ball it's image
            newBall.fired = false;

            //Stores the index on the struct so that other scripts can steal it
            newBall.myIndex = i;

            //Finally adding the ball to the list
            upcomingBallList.Add(newBall);
            var deliveredFielder = (fieldingTeam[Random.Range(0, fieldingTeam.Count)]);
            masterBallList.AddThisBallToTheList(newBall.myIndex, newBall.myTauntLevel, deliveredFielder);
        }

        masterBallList.AssignRemainingVariables();
    } //This should run after every successful "Batting Phase".

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
            var myBeamScript = Instantiate(targetingBeamPrefab, Vector3.zero, Quaternion.identity).GetComponent<fielderTargetingLineRenderer>();
            myBeamScript.originPosition = fielder.position;
            myBeamScript.direction = ((rangeAllocationScript.finalTargetPosition) - fielder.position).normalized;
            myBeamScript.playerTransform = player;
            myBeamScript.beamSizeDecreaseSpeed = ball.myThrowSpeed;
            rangeAllocationScript.GiveTheFielderATarget(true, fielder);

            //Cheating
            if (Gilded)
            {
                myBeamScript.sweetSpotActive = true;
            }
        }
    }
}
