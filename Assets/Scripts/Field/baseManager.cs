using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class baseManager : MonoBehaviour
{
    [Header("This must be manually set up until I can figure out a solution")]
    [SerializeField] private fielderProgressionBasedAccuracyScript fielderAccuracyObject = null;

    [Header("Visible for debug")]
    [SerializeField] private List<Transform> bases = null;
    [SerializeField] private float percentageOfRunRemaining = 0f;

    [Header("Tweak this")]
    [SerializeField] private float distanceFromBaseRequiredToProgress = 5;

    private Transform playerPosition = null;
    [System.NonSerialized] public Transform currentBaseTarget = null;
    private int currentBase = 0;
    private int nextBase = 1;
    private List<float> distanceBetweenBases = new List<float>();
    private float totalDistanceBetweenAllBases = 0f;
    private float remainingDistanceToHomeBaseSansPlayerToNextBase = 0f;
    private float realRemainingDistanceToHomeBase = 0f;
    private float remainingDistanceToNextBase = 0f;

    private void Awake()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }
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
        fielderAccuracyObject.NewTargetingNextBaseUpdater(GetBases(), currentBase);

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
       
        //Storing this for percentage calculations
        remainingDistanceToHomeBaseSansPlayerToNextBase = totalDistanceBetweenAllBases - distanceBetweenBases[currentBase];
    }

    private void Update()
    {
        //distance between player and next base
        remainingDistanceToNextBase = Vector3.Distance(playerPosition.position, bases[nextBase].position);

        //Updates the current and next bases when the player enters their range
        if (Vector3.Distance(playerPosition.position, bases[nextBase].position) < distanceFromBaseRequiredToProgress)
        {
            if (nextBase != bases.Count - 1 && nextBase != 0)
            {
                ProgressBase(currentBase + 1, nextBase + 1);
                remainingDistanceToHomeBaseSansPlayerToNextBase -= distanceBetweenBases[currentBase];
                currentBaseTarget = bases[currentBase].GetComponent<MyBaseTargetHolder>().myTarget;
                SwitchToBattingPhaseOnBaseTouch();
            }
            else if (nextBase == bases.Count - 1)
            {
                ProgressBase(currentBase + 1, nextBase = 0);
                remainingDistanceToHomeBaseSansPlayerToNextBase = 0;
                currentBaseTarget = bases[currentBase].GetComponent<MyBaseTargetHolder>().myTarget;
                SwitchToBattingPhaseOnBaseTouch();
            }
            else if (nextBase == 0)
            {
                ProgressBase(currentBase = 0, nextBase + 1);
                remainingDistanceToHomeBaseSansPlayerToNextBase = totalDistanceBetweenAllBases;
                remainingDistanceToHomeBaseSansPlayerToNextBase -= distanceBetweenBases[currentBase];
                currentBaseTarget = bases[currentBase].GetComponent<MyBaseTargetHolder>().myTarget;
                SwitchToBattingPhaseOnHomeBaseTouch();
            }
            fielderAccuracyObject.NewTargetingNextBaseUpdater(GetBases(), currentBase);
        }

        realRemainingDistanceToHomeBase = remainingDistanceToHomeBaseSansPlayerToNextBase + remainingDistanceToNextBase;
        percentageOfRunRemaining = realRemainingDistanceToHomeBase / totalDistanceBetweenAllBases;
        fielderAccuracyObject.updateAccuracysPercentage(percentageOfRunRemaining);
    }

    private void SwitchToBattingPhaseOnBaseTouch()
    {
        
        WorldStateMachine.SetCurrentState(WorldState.BATTING);
        //TRIGGER UI MANAGER'S TAUNT/BANK/HOLD MENU HERE


        /* 
            -Cinemachine shit for batting phase on bases
        */
    }

    private void SwitchToBattingPhaseOnHomeBaseTouch()
    {
        WorldStateMachine.SetCurrentState(WorldState.BATTING);


        /*
            -Cinemachine shit for batting phase on bases
        */
    }

    private void ProgressBase(int requestedCurrentBase, int requestedNextBase)
    {
        currentBase = requestedCurrentBase;
        nextBase = requestedNextBase;
    }

    public List<Transform> GetBases() => bases;
}
