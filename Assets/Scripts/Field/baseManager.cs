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
    [SerializeField] private HUDManager hUDScript = null;
    [SerializeField] private fielderPeltingScript fpScript= null;
    [SerializeField] private scoreUpdater scoreUpdaterScript = null;

    [Header("BaseEffects")]
    [SerializeField] private List<GameObject> pitchingCameras;
    [SerializeField] private GameObject playerBackCam = null;
    [SerializeField] private List<GameObject> SplitScreenLefts;
    [SerializeField] private List<GameObject> SplitScreenRights;
    [SerializeField] private GameObject baseCanvas = null;
    [SerializeField] private GameObject player = null;

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
                SwitchToBattingPhaseOnBaseTouch(nextBase.ToString());
            }
            else if (nextBase == bases.Count - 1)
            {
                ProgressBase(currentBase + 1, nextBase = 0);
                remainingDistanceToHomeBaseSansPlayerToNextBase = 0;
                currentBaseTarget = bases[currentBase].GetComponent<MyBaseTargetHolder>().myTarget;
                SwitchToBattingPhaseOnBaseTouch("HOME");
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

    private void SwitchToBattingPhaseOnBaseTouch(string nextBaseString)
    {
        hUDScript.SetTheBaseString(nextBaseString);
        WorldStateMachine.SetCurrentState(WorldState.FROZEN);

        Cursor.visible = true;
        baseCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    private void SwitchToBattingPhaseOnHomeBaseTouch()
    {
        hUDScript.SetTheBaseString("1");
        WorldStateMachine.SetCurrentState(WorldState.FROZEN);


        /*
            -Cinemachine shit for batting phase on bases
        */
    }

    private void ProgressBase(int requestedCurrentBase, int requestedNextBase)
    {
        currentBase = requestedCurrentBase;
        nextBase = requestedNextBase;
    }

    private IEnumerator BaseEffects()
    {
        Debug.Log(WorldStateMachine.GetCurrentState());
        /*var rb = playerPosition.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;*/
        playerBackCam.SetActive(true);
        pitchingCameras[currentBase - 1].SetActive(true);
        yield return new WaitForSeconds(1);
        player.transform.position = bases[currentBase].transform.position + new Vector3(0, 1.1f, 0);
        player.transform.eulerAngles = new Vector3(0, SplitScreenLefts[currentBase - 1].transform.eulerAngles.y, 0);
        yield return new WaitForSeconds(2);
        pitchingCameras[currentBase - 1].SetActive(false);
        yield return new WaitForSeconds(1);
        WorldStateMachine.SetCurrentState(WorldState.BATTING);
    }

    public void Taunt()
    {
        Cursor.visible = false;
        Time.timeScale = 1;
        player.transform.position = bases[currentBase].transform.position + new Vector3(0, 1.1f, 0);
        player.transform.eulerAngles = new Vector3(0, SplitScreenLefts[currentBase - 1].transform.eulerAngles.y, 0);
        fpScript.fielderTauntLevelIncreaser();
        SplitScreenLefts[currentBase - 1].SetActive(true);
        SplitScreenRights[currentBase - 1].SetActive(true);
        StartCoroutine(BaseEffects());
        baseCanvas.SetActive(false);
    }

    public void Bank()
    {
        Cursor.visible = false;
        Time.timeScale = 1;
        StartCoroutine(BaseEffects());
        scoreUpdaterScript.BankScore();
        baseCanvas.SetActive(false);
    }

    public void Hold()
    {
        Cursor.visible = false;
        Time.timeScale = 1;
        StartCoroutine(BaseEffects());
        baseCanvas.SetActive(false);
    }

    public List<Transform> GetBases() => bases;
}
