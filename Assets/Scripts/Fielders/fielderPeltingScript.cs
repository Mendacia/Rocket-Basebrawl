using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class fielderPeltingScript : MonoBehaviour
{
    [Header("These MUST be set in editor for game to work")]
    [SerializeField] private Transform player = null;
    [SerializeField] private GameObject targetingBeamPrefab;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowFolderOnCanvas;
    [SerializeField] private fielderProgressionBasedAccuracyScript rangeAllocationScript;
    [SerializeField] private Transform pitchingPhaseTarget = null;
    [SerializeField] private playerControls playerStateReference = null;
    [SerializeField] private CinemachineVirtualCamera playerVCAM;
    [SerializeField] private scoreHolder scoreHolderReference;

    [Header("These Wait Times are in seconds")]
    [SerializeField] private float minWaitTime = 1f;
    [SerializeField] private float maxWaitTime = 3f;

    [Header("Variables to tell player when they can move")]
    //[SerializeField] private GameObject goText = null;

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

    private void Awake()
    {
        pitchingLoopStarted = false;
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

        if (canThrow && pitchingLoopStarted)
        {
            ReadyThrow();
        }

        if (playerStateReference.playerState == 1 && !hasStartedPitchingSequenceAlready)
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
                if (scoreHolderReference.score >= 3)
                {
                    Time.timeScale = 1;
                    battingBallCount = 0;
                    StopCoroutine(BattingPhaseTimer());
                    scoreHolderReference.score = 0;
                    playerVCAM.m_Transitions.m_InheritPosition = false;
                    tutorialButton.SetActive(true);
                    tutorialPopup.SetActive(true);
                    Cursor.visible = true;
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
            }
            canThrow = false;
        }
    }
}
