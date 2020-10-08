using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fielderPeltingScript : MonoBehaviour
{
    public List<Transform> fieldingTeam;
    public GameObject ball;
    [Header("These MUST be set in editor for game to work")]
    [SerializeField] private LayerMask fielderLayerMask = 0;
    [SerializeField] private Transform player = null;
    [SerializeField] private GameObject targetingBeamPrefab;
    [Header("Dev Controls")]
    [SerializeField] private KeyCode devkeyToStartPelting = KeyCode.P;
    [SerializeField] private KeyCode comedy = KeyCode.L;
    [Header("These Wait Times are in seconds")]
    [SerializeField] private float minWaitTime = 3f;
    [SerializeField] private float maxWaitTime = 6f;
    private bool canThrow = false;
    private bool hasReadiedAThrow = false;

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

        if (Input.GetKeyDown(devkeyToStartPelting))
        {
            startPeltingLoop();
        }

        if (Input.GetKeyDown(comedy))
        {
            minWaitTime = 0;
            maxWaitTime = 0;
        }
    }

    public void startPeltingLoop()
    {
        StartCoroutine(ThrowDelay());
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
                RaycastHit fielderRaycastHit;
                var chosenFieldersRaycast = Physics.Raycast(fielder.position, ((player.position + Random.insideUnitSphere * 1.5f) - fielder.position).normalized, out fielderRaycastHit, 1000, fielderLayerMask);
                //Hee hoo beam time
                var myBeam = Instantiate(targetingBeamPrefab, Vector3.zero, Quaternion.identity);
                var myBeamScript = myBeam.GetComponent<fielderTargetingLineRenderer>();
                myBeamScript.fielderPosition = fielder;
                myBeamScript.fielderRaycastHitPosition = fielderRaycastHit.point;
                myBeamScript.ArmTheLineRenderer();

                //GameObject myBall = Instantiate(ball, fielder.position + fielder.transform.forward * 1, fielder.rotation);
                //myBall.GetComponent<fielderPeltingBallBehaviour>().fielder = fielder;
            }
            canThrow = false;
        }
    }
}
