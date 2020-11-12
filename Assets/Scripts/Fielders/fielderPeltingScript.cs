using System.Collections;
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
    [SerializeField] private playerControls playerStateReference = null;
    [Header("These Wait Times are in seconds")]
    [SerializeField] private float minWaitTime = 3f;
    [SerializeField] private float maxWaitTime = 6f;
    [Header("Variables to tell player when they can move")]
    [SerializeField] private GameObject goText = null;
    [Header("Make game hard")]
    public bool makeGameHard = false;

    //I set these automatically please don't try to manipulate these for anything other than visibility
    public List<Transform> fieldingTeam;
    [System.NonSerialized] public int battingBallCount;
    public bool canThrow = false;
    private bool hasReadiedAThrow = false;
    public bool hasStartedThrowingSequenceAlready = false;
    private bool hasStartedPitchingSequenceAlready = false;

    private int iterator = 0;
    public static bool gameStarted = false;

    private void Awake()
    {
        gameStarted = false;
    }

    private void Start()
    {
        
        //Populate fieldingTeam list with the children of this gameObject
        foreach (Transform child in gameObject.transform.Find("Team"))
        {
            fieldingTeam.Add(child);
        }
    }

    private void Update()
    {
        //Makes the Fielders all look at the player at all times
        foreach(Transform fielder in fieldingTeam)
        {
            fielder.LookAt(player);
        }

        if (canThrow && gameStarted)
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
        yield return new WaitForSeconds(2);
        iterator++;
        if (gameStarted == true)
        {
            //gameStarted is now being handled on the first hit under fielderTargetingSuccessfulHit to allow the pitching phase multiple times
            startPeltingLoop();
            iterator = 0;
            battingBallCount = 0;
            playerStateReference.playerState = 2;
            //Do some shit to tell the player they can go
            StartCoroutine(TellPlayerTheyCanGo());
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

    private IEnumerator TellPlayerTheyCanGo()
    {
        goText.SetActive(true);
        //Play audio clip of whistle or something
        yield return new WaitForSeconds(0.75f);
        goText.SetActive(false);
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
