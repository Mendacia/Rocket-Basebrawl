using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class fielderPeltingScript : MonoBehaviour
{
    [Header("These MUST be set in editor for game to work")]
    [SerializeField] private Transform player = null;
    [SerializeField] private GameObject targetingBeamPrefab;
    [SerializeField] private fielderTargetingRangeAllocator rangeAllocationScript;
    [Header("These Wait Times are in seconds")]
    [SerializeField] private float minWaitTime = 3f;
    [SerializeField] private float maxWaitTime = 6f;

    //I set these automatically please don't try to manipulate these for anything other than visibility
    [System.NonSerialized] public List<Transform> fieldingTeam;
    [System.NonSerialized] public int battingBallCount;
    private bool canThrow = false;
    private bool hasReadiedAThrow = false;
    private bool hasStartedThrowingSequenceAlready = false;

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

        if (canThrow == true)
        {
            ReadyThrow();
        }
    }
    
    public void Throw(CallbackContext context)
    {
        if (context.performed)
        {
            startPeltingLoop();
        }
    }

    public void Pelt(CallbackContext context)
    {
        if (context.performed)
        {
            minWaitTime = 0;
            maxWaitTime = 0;
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

    private void battingPhaseThrow()
    {
        battingBallCount++;
        if (battingBallCount < 4)
        {
            var thePitcher = fieldingTeam[0];
            var myBeamScript = Instantiate(targetingBeamPrefab, Vector3.zero, Quaternion.identity).GetComponent<fielderTargetingLineRenderer>();
            myBeamScript.direction = ((player.position + (Random.insideUnitSphere * 0.5f)) + new Vector3(0, 0, 1) - thePitcher.position).normalized;
            myBeamScript.playerTransform = player.transform;
        }
        else
        {
            //commit die.
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
