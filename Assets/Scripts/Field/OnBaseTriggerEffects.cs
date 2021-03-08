using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBaseTriggerEffects : MonoBehaviour
{
    [Header("Put Camera under the base here!")]
    [SerializeField] private GameObject cineMachineBaseCam = null;
    [SerializeField] private GameObject pitchingCam = null;
    [Header("Put Player Controller here!")]
    [SerializeField] private GameObject player = null;
    [SerializeField] private playerControls playerStateReference = null;
    [Header("Put the Opponent Team here!")]
    [SerializeField] private fielderPeltingScript fielderReference = null;
    [Header("Put the Pitching Phase Target here!")]
    [SerializeField] private GameObject pitchingPhaseTarget = null;
    [Header("Put ScoreHolder here!")]
    [SerializeField] private scoreHolder scoreReference = null;
    [SerializeField] private scoreUpdater scoreUpdaterReference = null;
    [Header("Put the base number here!")]
    [SerializeField] private int pitchingNumber = 1;
    public static int currentPitchingNumber = 1;
    [Header("Put trigger box collider here!")]
    [SerializeField] private BoxCollider baseTrigger = null;
    [SerializeField] private GameObject baseBeacon = null;
    [Header("Put Canvas Here!")]
    [SerializeField] private GameObject baseCanvas = null;

    GameObject[] tempIcons;
    private bool pitchingStarted = false;

    private Vector3 basePosition;

    private void Awake()
    {
        currentPitchingNumber = 1;
    }

    void Start()
    {
        basePosition = this.transform.position;
    }

    private void Update()
    {
        if (fielderReference.activateBase == true && currentPitchingNumber == pitchingNumber)
        {
            baseTrigger.enabled = true;
            baseBeacon.SetActive(true);
        }
        else
        {
            baseTrigger.enabled = false;
            baseBeacon.SetActive(false);
        }
        if (fielderPeltingScript.pitchingLoopStarted)
        {
            pitchingCam.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && pitchingStarted == false && currentPitchingNumber == pitchingNumber && scoreReference.canScore == true)
        {
            pitchingStarted = true;
            fielderPeltingScript.pitchingLoopStarted = false;

            baseCanvas.SetActive(true);
            Time.timeScale = 0;
            Cursor.visible = true;

            fielderReference.upcomingBallList.Clear();

            tempIcons = GameObject.FindGameObjectsWithTag("BallIconTag");
            foreach(GameObject ball in tempIcons)
            {
                Destroy(ball);
            }
        }
    }

    public void Taunt()
    {
        //Taunt adds to taunt value and changes pithcing values accordingly
        fielderReference.fielderTauntLevelIncreaser();
        StartCoroutine(StartPitchingPhase());
        baseCanvas.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
    }
    public void Hold()
    {
        //Hold just doesn't affect the score...
        StartCoroutine(StartPitchingPhase());
        baseCanvas.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
    }
    public void Bank()
    {
        //Bank points, shouldn't require any weird stuff
        scoreUpdaterReference.BankScore();
        StartCoroutine(StartPitchingPhase());
        baseCanvas.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pitchingStarted = false;
        }
    }

    //This is a bunch of BS settings

    private IEnumerator StartPitchingPhase()
    {
        //Resets game variables back to beginning
        fielderReference.hasStartedThrowingSequenceAlready = false;
        fielderReference.activateBase = false;
        playerStateReference.playerState = 1;
        var rb = player.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
        cineMachineBaseCam.SetActive(true);
        pitchingCam.SetActive(true);
        player.GetComponent<ActivatePlayer>().enabled = true;
        //Wait a second so that the player can't see the players position and rotation being corrected

        //Add empty list obstructs!!!!!!
        yield return new WaitForSeconds(1);
        player.transform.position = basePosition + new Vector3(0, 1.1f, 0);
        player.transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
        pitchingPhaseTarget.transform.position = basePosition + new Vector3(0, 1.68f, 0.5f);
        //Reset camera back to the player, start pitching and increase the base variable
        yield return new WaitForSeconds(2);
        fielderReference.canThrow = false; //Set canThrow here so that it's guaranteed to not conflict with the coroutine before it stops
        cineMachineBaseCam.SetActive(false);
        StartCoroutine(fielderReference.BattingPhaseTimer());
        currentPitchingNumber++;
    }
}
