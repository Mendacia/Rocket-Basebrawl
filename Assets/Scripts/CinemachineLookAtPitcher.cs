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

    // Update is called once per frame
    void Update()
    {
        
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

    private IEnumerator StartPitchingPhase()
    {
        playerStateReference.playerState = 1;
        StopCoroutine(fielderReference.ThrowDelay());
        cineMachineBaseCam.SetActive(true);
        player.GetComponent<ActivatePlayer>().enabled = true;
        yield return new WaitForSeconds(1);
        player.transform.position = basePosition;
        pitchingPhaseTarget.transform.position = basePosition + new Vector3(0, 1.68f, 0.5f);
        yield return new WaitForSeconds(2);
        cineMachineBaseCam.SetActive(false);
        StartCoroutine(fielderReference.BattingPhaseTimer());
        currentPitchingNumber++;
    }
}
