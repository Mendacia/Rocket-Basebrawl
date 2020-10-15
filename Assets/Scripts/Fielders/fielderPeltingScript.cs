using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class fielderPeltingScript : MonoBehaviour
{
    [Header("These MUST be set in editor for game to work")]
    [SerializeField] private Transform player = null;
    [SerializeField] private GameObject targetingBeamPrefab;
    [Header("Dev Controls")]
    [SerializeField] private KeyCode devkeyToStartPelting = KeyCode.P;
    [SerializeField] private KeyCode comedy = KeyCode.L;
    [Header("These Wait Times are in seconds")]
    [SerializeField] private float minWaitTime = 3f;
    [SerializeField] private float maxWaitTime = 6f;

    //I set these automatically please don't try to manipulate these for anything other than visibility
    public List<Transform> fieldingTeam;
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
        startPeltingLoop();
    }

    public void Pelt(CallbackContext context)
    {
        minWaitTime = 0;
        maxWaitTime = 0;
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

    private void ReadyThrow()
    {
        int numberOfBallsToThrow;
        List<Transform> chosenFielders = new List<Transform>();
        if (hasReadiedAThrow == false)
        {

            //Block to choose how many balls to throw
            var throwCountValue = Random.Range(0, 9);
            if(throwCountValue == 8)
            {
                numberOfBallsToThrow = 2;
            }
            else if(throwCountValue == 9)
            {
                numberOfBallsToThrow = 3;
            }
            else
            {
                numberOfBallsToThrow = 1;
            }

            //The variable "numberOfBallsToThrow" is now holding how many balls the fielders will throw, we now need to find who will throw them
            while (numberOfBallsToThrow > 0)
            {
                chosenFielders.Add(fieldingTeam[Random.Range(0, fieldingTeam.Count)]);
                numberOfBallsToThrow--;
            }

            //Cool, we now have a list populated with the fielders that will throw the ball. Now all we need to do is, get them to do that...
            foreach (Transform fielder in chosenFielders)
            {
                var myBeamScript = Instantiate(targetingBeamPrefab, Vector3.zero, Quaternion.identity).GetComponent<fielderTargetingLineRenderer>();
                myBeamScript.originPosition = fielder.position;
                myBeamScript.direction = ((player.position + Random.insideUnitSphere * 1.5f) - fielder.position).normalized;
                myBeamScript.playerTransform = player.transform;
            }
            canThrow = false;
        }
    }
}
