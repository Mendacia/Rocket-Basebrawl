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
    private fielderScatterAccuracyScript scatterAccuracyObject = null;
    [SerializeField] private HUDManager hUDScript = null;
    [SerializeField] private fielderPeltingScript fpScript= null;
    [SerializeField] private scoreUpdater scoreUpdaterScript = null;
    [SerializeField] private Button bankButton = null;

    [Header("BaseEffects")]
    [SerializeField] private Transform nextBaseIndicator;
    [SerializeField] private List<GameObject> pitchingCameras;
    [SerializeField] private GameObject playerBackCam = null;
    [SerializeField] private List<GameObject> SplitScreenLefts;
    [SerializeField] private List<GameObject> SplitScreenRights;
    [SerializeField] private GameObject player = null;
    [SerializeField] private GameObject endScreenCam = null;
    [SerializeField] private Animator playerAnim = null;
    [SerializeField] private Animator pitcherAnim = null;

    [Header("Visible for debug")]
    [SerializeField] private List<Transform> bases = null;
    [SerializeField] private float percentageOfRunRemaining = 0f;

    [Header("Tweak this")]
    [SerializeField] private float distanceFromBaseRequiredToProgress = 5;
    [SerializeField] private string sceneToLoad = "EndingBasebrawlTestingZone";

    private Transform playerPosition = null;
    [System.NonSerialized] public Transform currentBaseTarget = null;
    private int currentBase = 0;
    private int nextBase = 1;
    private List<float> distanceBetweenBases = new List<float>();
    private float totalDistanceBetweenAllBases = 0f;
    private float remainingDistanceToHomeBaseSansPlayerToNextBase = 0f;
    private float realRemainingDistanceToHomeBase = 0f;
    private float remainingDistanceToNextBase = 0f;
    private bool bankAnimation;

    private void Awake()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        scatterAccuracyObject = fielderAccuracyObject.GetComponent<fielderScatterAccuracyScript>();
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
        scatterAccuracyObject.NewTargetingNextBaseUpdater(GetBases(), currentBase);

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
                SwitchToBattingPhaseOnBaseTouch("H");
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
            scatterAccuracyObject.NewTargetingNextBaseUpdater(GetBases(), currentBase);
            nextBaseIndicator.position = bases[nextBase].position;
        }

        realRemainingDistanceToHomeBase = remainingDistanceToHomeBaseSansPlayerToNextBase + remainingDistanceToNextBase;
        percentageOfRunRemaining = realRemainingDistanceToHomeBase / totalDistanceBetweenAllBases;
        fielderAccuracyObject.updateAccuracysPercentage(percentageOfRunRemaining);
        scatterAccuracyObject.updateAccuracysPercentage(percentageOfRunRemaining);

    }

    private void SwitchToBattingPhaseOnBaseTouch(string nextBaseString)
    {
        hUDScript.SetTheBaseString(nextBaseString);
        playerBackCam.SetActive(true);
        player.transform.position = bases[currentBase].transform.position + new Vector3(0, 1.1f, 0);
        //player.transform.eulerAngles = new Vector3(0, playerBackCam.transform.eulerAngles.y, 0);
        WorldStateMachine.SetCurrentState(WorldState.FROZEN);
        Cursor.visible = true;
        hUDScript.runTheBaseUI(true, fpScript.GetFielderTauntLevel());
        bankButton.enabled = true;
        //This line only works if the pitcher is centered
        player.transform.eulerAngles = new Vector3(0, SplitScreenLefts[currentBase - 1].transform.eulerAngles.y, 0);
        //This line works if the pitcher isn't centered, but doesn't if it is


        //Time.timeScale = 0;
    }

    private void SwitchToBattingPhaseOnHomeBaseTouch()
    {
        hUDScript.SetTheBaseString("1");
        WorldStateMachine.SetCurrentState(WorldState.FROZEN);

        StartCoroutine(HomeBaseEffects());
    }

    private void ProgressBase(int requestedCurrentBase, int requestedNextBase)
    {
        currentBase = requestedCurrentBase;
        nextBase = requestedNextBase;
    }

    private IEnumerator BaseEffects()
    {
        Debug.Log("Yeah this is running");
        Debug.Log(WorldStateMachine.GetCurrentState());
        /*var rb = playerPosition.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;*/
        playerBackCam.SetActive(true);
        pitchingCameras[currentBase - 1].SetActive(true);
        yield return new WaitForSeconds(1);
        player.transform.position = bases[currentBase].transform.position + new Vector3(0, 1.1f, 0);
        player.transform.eulerAngles = new Vector3(0, playerBackCam.transform.eulerAngles.y, 0);
        yield return new WaitForSeconds(2);
        pitchingCameras[currentBase - 1].SetActive(false);
        yield return new WaitForSeconds(1);
        WorldStateMachine.SetCurrentState(WorldState.BATTING);
    }

    private IEnumerator HomeBaseEffects()
    {
        yield return new WaitForSeconds(0.1f);
        scoreUpdaterScript.BankScore();
        SceneManager.LoadScene(sceneToLoad);
        /*player.transform.position = bases[currentBase].transform.position + new Vector3(0, 1.1f, 0);
        player.transform.eulerAngles = new Vector3(0, 90, 0);
        playerBackCam.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        endScreenCam.SetActive(true);*/
    }

    public void Taunt()
    {
        Cursor.visible = false;
        Time.timeScale = 1;
        StartCoroutine(waitForAnimation(1));
    }

    public void Bank()
    {
        Cursor.visible = false;
        bankButton.enabled = false;
        Time.timeScale = 1;
        StartCoroutine(waitForAnimation(3));
    }

    public void Hold()
    {
        Cursor.visible = false;
        Time.timeScale = 1;
        StartCoroutine(waitForAnimation(2));
    }

    IEnumerator waitForAnimation(int state)
    {
        switch (state)
        {
            case 1:
                hUDScript.baseUITaunt(fpScript.GetFielderTauntLevel());
                yield return new WaitForSeconds(4.33333f);
                player.transform.position = bases[currentBase].transform.position + new Vector3(0, 1.1f, 0);
                player.transform.eulerAngles = new Vector3(0, playerBackCam.transform.eulerAngles.y, 0);
                fpScript.fielderTauntLevelIncreaser();
                SplitScreenLefts[currentBase - 1].SetActive(true);
                SplitScreenRights[currentBase - 1].SetActive(true);
                playerAnim.SetTrigger("heTaunt");
                pitcherAnim.SetTrigger("heTaunt");
                StartCoroutine(BaseEffects());
                hUDScript.runTheBaseUI(false, fpScript.GetFielderTauntLevel());
                break;
            case 2:
                hUDScript.baseUIHold();
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(BaseEffects());
                hUDScript.runTheBaseUI(false, fpScript.GetFielderTauntLevel());
                break;
            case 3:
                yield return new WaitForSeconds(0);
                hUDScript.baseUIBank();
                break;
            default:
                yield return new WaitForSeconds(0);
                break;
        }
    }

    public void EndBankAnimation()
    {
        StartCoroutine(BaseEffects());
        scoreUpdaterScript.BankScore();
        hUDScript.runTheBaseUI(false, fpScript.GetFielderTauntLevel());
    }

    public List<Transform> GetBases() => bases;
}
