using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineLookAtPitcher : MonoBehaviour
{
    [Header("Put Camera under the base here!")]
    [SerializeField] private GameObject cineMachineBaseCam = null;
    [Header("Put Player Controller here!")]
    [SerializeField] private GameObject player = null;
    [SerializeField] private playerControls playerStateReference = null;
    [Header("Put the Opponent Team here!")]
    [SerializeField] private fielderPeltingScript fielderReference = null;
    [Header("Put the Pitching Phase Target here!")]
    [SerializeField] private GameObject pitchingPhaseTarget = null;
    [Header("Put the base number here!")]
    [SerializeField] private int pitchingNumber = 1;
    public static int currentPitchingNumber = 1;

    private bool pitchingStarted = false;

    private Vector3 basePosition;

    void Start()
    {
        basePosition = this.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && pitchingStarted == false && currentPitchingNumber == pitchingNumber)
        {
            pitchingStarted = true;
            fielderPeltingScript.gameStarted = false;

            StartCoroutine(StartPitchingPhase());
        }
    }

    //This is a bunch of BS settings

    private IEnumerator StartPitchingPhase()
    {
        //Resets game variables back to beginning
        if (fielderReference.makeGameHard)
        {
            //Setting these lines to false makes the game get harder as you clear each base!
            fielderReference.hasStartedThrowingSequenceAlready = false;
            fielderReference.canThrow = false;
        }
        playerStateReference.playerState = 1;
        var rb = player.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
        StopCoroutine(fielderReference.ThrowDelay());
        cineMachineBaseCam.SetActive(true);
        player.GetComponent<ActivatePlayer>().enabled = true;
        //Wait a second so that the player can't see the players position and rotation being corrected
        yield return new WaitForSeconds(1);
        player.transform.position = basePosition + new Vector3(0, 1.1f, 0);
        player.transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
        pitchingPhaseTarget.transform.position = basePosition + new Vector3(0, 1.68f, 0.5f);
        //Reset camera back to the player, start pitching and increase the base variable
        yield return new WaitForSeconds(2);
        cineMachineBaseCam.SetActive(false);
        StartCoroutine(fielderReference.BattingPhaseTimer());
        currentPitchingNumber++;
    }
}
