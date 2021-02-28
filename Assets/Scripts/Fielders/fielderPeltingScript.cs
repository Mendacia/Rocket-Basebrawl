using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cinemachine;

public class fielderPeltingScript : MonoBehaviour
{
    [Header("These MUST be set in editor for game to work")]
    [SerializeField] private Transform player = null;
    [SerializeField] private GameObject targetingBeamPrefab;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private GameObject ballIconPrefab;
    [SerializeField] private Transform ballIconHolderOnCanvas;
    [SerializeField] private Transform arrowFolderOnCanvas;
    [SerializeField] private fielderProgressionBasedAccuracyScript rangeAllocationScript;
    [SerializeField] private Transform pitchingPhaseTarget = null;
    [SerializeField] private playerControls playerStateReference = null;
    [SerializeField] private CinemachineVirtualCamera playerVCAM = null;
    [SerializeField] private scoreHolder scoreHolderReference;
    [SerializeField] private scoreUpdater scoreUpdaterReference;

    [Header("These Wait Times are in seconds")]
    [SerializeField] private float minWaitTime = 1f;
    [SerializeField] private float maxWaitTime = 3f;

    [Header("Determines how many balls the fielders will throw between this base and the next")]
    [SerializeField] private int ballsToThrowMinimum = 10;
    [SerializeField] private int ballsToThrowMaximum = 15;
    [SerializeField] private int higherTauntBallCountReductionFactor = 3;
    private int finalBallsToThrow;


    public enum ballType
    {
        STANDARD,
        ARC,
        SCATTER,
        MIXUP
    }
    [System.Serializable] public struct ballsOrSomethingIDK
    {
        public ballType myType;
        public int tauntLevel;
        public float speedMultiplier;
        public GameObject uIIcon;
        public Sprite myTexture;
        public bool fired;
    }
    private bool actuallyHasBallsReady;

    public List<ballsOrSomethingIDK> upcomingBallList;

    [Header("Tutorial Variables")]
    [SerializeField] private GameObject tutorialPopup = null;
    [SerializeField] private GameObject tutorialButton = null;

    private bool firstFielder = true;

    //I set these automatically please don't try to manipulate these for anything other than visibility
    public List<Transform> fieldingTeam;
    [System.NonSerialized] public int battingBallCount;
    public bool canThrow = false;
    private bool hasReadiedAThrow = false;
    public bool hasStartedThrowingSequenceAlready = false;
    private bool hasStartedPitchingSequenceAlready = false;

    private int iterator = 0;
    public static bool pitchingLoopStarted = false;

    public int fielderTauntLevel = 0;

    private void Awake()
    {
        pitchingLoopStarted = false;
        scoreHolderReference = GameObject.Find("Scoreholder").GetComponent<scoreHolder>();
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

        if (canThrow && pitchingLoopStarted && actuallyHasBallsReady)
        {
            ReadyThrow();
        }

        if (playerStateReference.playerState == 1 && !hasStartedPitchingSequenceAlready)
        {
            hasStartedPitchingSequenceAlready = true;
            StartCoroutine(BattingPhaseTimer());
        }
    }

    public void fielderTauntLevelIncreaser()
    {
        fielderTauntLevel++;
    }

    public void startPeltingLoop()
    {
        if (hasStartedThrowingSequenceAlready == false)
        {
            hasStartedThrowingSequenceAlready = true;


            //THIS IS WHERE THE NEW PELTING STARTS.
            ballListPopulater(fielderTauntLevel);
            actuallyHasBallsReady = true;

            StartCoroutine(ThrowDelay());
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
            ballsOrSomethingIDK newBall = new ballsOrSomethingIDK();

            int holdThis = Random.Range(0, recievedTauntLevel * 10);
            if (holdThis < 5)
            {
                newBall.tauntLevel = 0;
            }
            else if (holdThis >= 5 && holdThis < 15)
            {
                newBall.tauntLevel = 1;
            }
            else if (holdThis >= 15 && holdThis < 25)
            {
                newBall.tauntLevel = 2;
            }
            else
            {
                newBall.tauntLevel = 3;
            }

            //Assigns the ball's icon for the UI
            newBall.myTexture = BallIconHolder.GetIcon(BallResult.UNTHROWN, newBall.tauntLevel);

            //Instantiating the icon to the UI
            newBall.uIIcon = Instantiate(ballIconPrefab);
            newBall.uIIcon.transform.SetParent(ballIconHolderOnCanvas);
            newBall.uIIcon.GetComponent<Image>().sprite = newBall.myTexture;
            newBall.fired = false;
            Debug.Log("Fired has been set to false");

            //Finally adding the ball to the list
            upcomingBallList.Add(newBall);
        }
    }

    public IEnumerator ThrowDelay()
    {
        yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
        canThrow = true;
        if (pitchingLoopStarted)
        {
            StartCoroutine(ThrowDelay());
        }
    }

    public void battingPhaseThrow()
    {
        battingBallCount++;
        if (battingBallCount < 4)
        {
            var thePitcher = fieldingTeam[0];
            var myBeamScript = Instantiate(targetingBeamPrefab, Vector3.zero, Quaternion.identity).GetComponent<fielderTargetingLineRenderer>();
            var myArrow = Instantiate(arrowPrefab);
            myArrow.transform.SetParent(arrowFolderOnCanvas);
            myBeamScript.myArrow = myArrow;
            myBeamScript.originPosition = thePitcher.position;
            myBeamScript.playerTransform = player.transform;
            myBeamScript.direction = ((pitchingPhaseTarget.position + (Random.insideUnitSphere)) - thePitcher.position).normalized;
           
        }
        else
        {
            //commit die.
        }
    }

    //Start of the game/Pitching phase
    public IEnumerator BattingPhaseTimer()
    {
        switch (scoreHolderReference.canScore)
        {
            //Case for the MAIN GAME
            case true:
                yield return new WaitForSeconds(2);
                iterator++;
                if (pitchingLoopStarted == true)
                {
                    //pitchingLoopStarted is now being handled on the first hit under fielderTargetingSuccessfulHit to allow the pitching phase multiple times
                    startPeltingLoop();
                    Time.timeScale = 1;
                    iterator = 0;
                    battingBallCount = 0;
                    playerVCAM.m_Transitions.m_InheritPosition = true;
                    playerStateReference.playerState = 2;
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
                break;


            //Case for the TUTORIAL
            case false:
                yield return new WaitForSeconds(2);
                if (scoreHolderReference.score >= 3000)
                {
                    Time.timeScale = 1;
                    battingBallCount = 0;
                    StopCoroutine(BattingPhaseTimer());
                    scoreHolderReference.score = 0;
                    playerVCAM.m_Transitions.m_InheritPosition = false;
                    playerStateReference.playerState = 0;
                    tutorialButton.SetActive(true);
                    tutorialPopup.SetActive(true);
                    Cursor.visible = true;
                    Time.timeScale = 0;
                }
                else
                {
                    battingBallCount = 0;
                    battingPhaseThrow();
                    StartCoroutine(BattingPhaseTimer());
                }
                break;
        }
    }
    
    /*  Removed code about the Go Text. Unnecessary until we get assets in that actually make use of this.
        That means we are waiting on:
            - Actual Go Text
            - Accompanying Sound Effect
            - Fielder Animations
        Along with those we should probably move that function to an entirely new script but we'll cross that bridge later*/

    private void ReadyThrow()
    {
        ballsOrSomethingIDK finalBall;
        if (hasReadiedAThrow == false)
        {
            for (int i =0; i < upcomingBallList.Count; i++)
            {
                Debug.Log("Successfully polled a ball");
                var testedBall = upcomingBallList[i];
                if (!testedBall.fired)
                {
                    Debug.Log("Fired a ball with itterator at " + i);
                    finalBall = testedBall;
                    finalBall.fired = true;
                    upcomingBallList[i] = finalBall;
                    scoreUpdaterReference.gameObject.GetComponent<scoreUpdater>().ballIndex = i;

                    ReadyThrowPartTwo(finalBall);
                    break;
                }
                else if ( i+1 == upcomingBallList.Count)
                {
                    actuallyHasBallsReady = false;
                }
            }
        }
    }

    private void ReadyThrowPartTwo(ballsOrSomethingIDK myBall)
    {
        List<Transform> chosenFielders = new List<Transform>();
        int numberOfBallsToThrow;

        //Block to choose how many balls to throw
        numberOfBallsToThrow = 1;

        //The variable "numberOfBallsToThrow" is now holding how many balls the fielders will throw, we now need to find who will throw them
        while (numberOfBallsToThrow > 0)
        {
            chosenFielders.Add(fieldingTeam[Random.Range(0, fieldingTeam.Count)]);
            numberOfBallsToThrow--;
            firstFielder = true;
        }

        //Cool, we now have a list populated with the fielders that will throw the ball. Now all we need to do is, get them to do that...
        foreach (Transform fielder in chosenFielders)
        {
            var myBeamScript = Instantiate(targetingBeamPrefab, Vector3.zero, Quaternion.identity).GetComponent<fielderTargetingLineRenderer>();
            var myArrow = Instantiate(arrowPrefab);
            myArrow.transform.SetParent(arrowFolderOnCanvas);
            myBeamScript.myArrow = myArrow;
            myBeamScript.originPosition = fielder.position;
            rangeAllocationScript.GiveTheFielderATarget(firstFielder, fielder);
            myBeamScript.direction = ((rangeAllocationScript.finalTargetPosition) - fielder.position).normalized;
            firstFielder = false;
            myBeamScript.playerTransform = player.transform;
            myBeamScript.beamSizeDecreaseSpeed = 1 + (myBall.tauntLevel / 2);
        }
        canThrow = false;
        Debug.Log("This is the end?");
    }
}
