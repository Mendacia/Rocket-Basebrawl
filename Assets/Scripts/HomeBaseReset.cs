using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class HomeBaseReset : MonoBehaviour
{
    [Header("Put Camera under the base here!")]
    [SerializeField] private GameObject homeCam = null;
    [SerializeField] private CinemachineDollyCart cart;
    [SerializeField] private CinemachineVirtualCamera playerVCAM;
    [Header("Put the UI here")]
    [SerializeField] private GameObject screenUI = null;
    [Header("Put Player Controller here!")]
    [SerializeField] private GameObject player = null;
    [SerializeField] private playerControls playerStateReference = null;
    [Header("Put the Pitching Phase Target here!")]
    [SerializeField] private GameObject pitchingPhaseTarget = null;
    [Header("Put the Opponent Team here!")]
    [SerializeField] private fielderPeltingScript fielderReference = null;
    [Header("Put ScoreHolder here!")]
    [SerializeField] private scoreHolder scoreReference = null;
    [Header("Put the base number here!")]
    [SerializeField] private int pitchingNumber = 4;

    private bool pitchingStarted = false;

    [System.NonSerialized] public int roundsRun = 0;

    private Vector3 basePosition;

    void Start()
    {
        roundsRun = 0;
        basePosition = this.transform.position;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && pitchingStarted == false && CinemachineLookAtPitcher.currentPitchingNumber == 4 && CinemachineLookAtPitcher.currentPitchingNumber == pitchingNumber && scoreReference.canScore == true)
        {
            pitchingStarted = true;
            fielderPeltingScript.pitchingLoopStarted = false;

            StartCoroutine(ResetHomeBase());
        }
    }

    private IEnumerator ResetHomeBase()
    {
        homeCam.SetActive(true);
        //Resets game variables back to beginning
        fielderPeltingScript.pitchingLoopStarted = false;
        fielderReference.hasStartedThrowingSequenceAlready = false;
        playerStateReference.playerState = 1;
        var rb = player.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
        player.GetComponent<ActivatePlayer>().enabled = true;
        //Wait a second so that the player can't see the players position and rotation being corrected
        yield return new WaitForSeconds(1);
        player.transform.position = basePosition + new Vector3(0, 1.1f, 0);
        pitchingPhaseTarget.transform.position = basePosition + new Vector3(0, 1.68f, 0.5f);
        player.transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
        //pitchingPhaseTarget.transform.position = basePosition + new Vector3(0, 1.68f, 0.5f);
        //Reset camera back to the player, start pitching and increase the base variable
        yield return new WaitForSeconds(4);
        fielderReference.canThrow = false; //Set canThrow here so that it's guaranteed to not conflict with the coroutine before it stops
        CinemachineLookAtPitcher.currentPitchingNumber = 1;
        screenUI.SetActive(true);
        Cursor.visible = true;
        roundsRun++;
        playerVCAM.m_Transitions.m_InheritPosition = false;
    }
    public void RoundRestart()
    {
        if (roundsRun == 3)
        {
            SceneManager.LoadScene("TempHub");
        }
        else
        {
            screenUI.SetActive(false);
            homeCam.SetActive(false);
            StartCoroutine(fielderReference.BattingPhaseTimer());
            pitchingStarted = false;
            Cursor.visible = false;
            //cart.m_PositionUnits = 0;
            cart.m_Position = 0;
        }
    }
}

