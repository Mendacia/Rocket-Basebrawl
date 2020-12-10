using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class baseManager : MonoBehaviour
{
    [Header("You shouldn't need to touch these, they're visible for debug purposes")]
    [SerializeField] private List<Transform> bases = null;
    private int currentBase = 0;
    private int nextBase = 1;
    [Header("Set these to their respective objects please and thank you")]
    [SerializeField] private Transform playerPosition = null;
    [SerializeField] private Text uIBaseText = null;
    [SerializeField] private fielderProgressionBasedAccuracyScript testingAllocationScript = null;
    [Header("This is 5 by default")]
    [SerializeField] private float distanceFromBaseRequiredToProgress = 5;
    private bool hasLeftHome = false;
    [Header("This is theoretically how complete the run is")]
    private List<float> distanceBetweenBases = new List<float>();

    private float roundsCompleted = 0;

    private float totalDistanceBetweenAllBases = 0f;
    private float remainingDistanceToHomeBaseSansPlayerToNextBase = 0f;
    private float realRemainingDistanceToHomeBase = 0f;
    private float remainingDistanceToNextBase = 0f;
    public float percentageOfRunRemaining = 0f;


    private void Start()
    {
        //Populating list, if bases are wack order the prefab better, if bases break in build, this is probably the issue.
        foreach (Transform child in gameObject.transform)
        {
            bases.Add(child);
        }

        Debug.Log("I'm " + this + ". I have counted and there are " + bases.Count + " bases.");

        //Sets initial base to home
        currentBase = 0;
        nextBase = 1;
        testingAllocationScript.NewTargetingNextBaseUpdater(nextBase);

        //This populates the list for distance between bases
        for (int i = 0; i < bases.Count; i++)
        {
            if (i == bases.Count - 1)
            {
                distanceBetweenBases.Add(Vector3.Distance(bases[0].position, bases[i].position));
            }
            else
            {
                distanceBetweenBases.Add(Vector3.Distance(bases[i].position, bases[i + 1].position));
            }
        }

        totalDistanceBetweenAllBases = distanceBetweenBases.Sum();
        //Stores the total distance for accuracy calculations
        remainingDistanceToHomeBaseSansPlayerToNextBase = totalDistanceBetweenAllBases;

        //Removes the distance between base 0 and 1
        remainingDistanceToHomeBaseSansPlayerToNextBase = remainingDistanceToHomeBaseSansPlayerToNextBase - distanceBetweenBases[currentBase];
    }

    private void Update()
    {
        
        //distance between player and next base
        remainingDistanceToNextBase = Vector3.Distance(playerPosition.position, bases[nextBase].position);
        realRemainingDistanceToHomeBase = remainingDistanceToHomeBaseSansPlayerToNextBase + remainingDistanceToNextBase;


        //Updates the current base if the player is within the distanceFromBaseRequiredToProgress
        if (nextBase != (bases.Count - 1) && nextBase != 0)
        {
            if (Vector3.Distance(playerPosition.position, bases[(currentBase + 1)].position) < distanceFromBaseRequiredToProgress)
            {
                currentBase++;
                nextBase++;
                hasLeftHome = true;
                remainingDistanceToHomeBaseSansPlayerToNextBase = remainingDistanceToHomeBaseSansPlayerToNextBase - distanceBetweenBases[currentBase];
            }
        }
        //This is here because it covers an error where if this was an "or" in the above "if" then the next base would be higher than the index of bases, causing an error.
        else if (nextBase == (bases.Count - 1))
        {
            if (Vector3.Distance(playerPosition.position, bases[bases.Count - 1].position) < distanceFromBaseRequiredToProgress)
            {
                currentBase++;
                nextBase = 0;
                remainingDistanceToHomeBaseSansPlayerToNextBase = 0;
            }
        }
        else
        {
            if (Vector3.Distance(playerPosition.position, bases[0].position) < distanceFromBaseRequiredToProgress)
            {
                currentBase = 0;
                nextBase++;
            }
        }
        testingAllocationScript.NewTargetingNextBaseUpdater(nextBase);

        //Loads scene on return to home, replace this later
        if (currentBase == 0 && hasLeftHome)
        {
            //SceneManager.LoadScene("EndingBasebrawlTestingZone");
            roundsCompleted++;
        }

        uIBaseText.text = bases[nextBase].name;
        //This used to be in the if statements, but I got really tired of it not working, so it's here now. equations in "if" statements go through when you close the 'if'.

        //realRemainingDistanceToHomeBase totalDistanceBetweenAllBases
        percentageOfRunRemaining = realRemainingDistanceToHomeBase / totalDistanceBetweenAllBases;
    }

    public List<Transform> GetBases() => bases;
}
