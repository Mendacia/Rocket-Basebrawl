using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGodScript : MonoBehaviour
{
    [Header("Put Player Controller here!")]
    [SerializeField] private GameObject player = null;
    [SerializeField] private playerControls playerStateReference = null;
    [Header("Put ScoreHolder here!")]
    [SerializeField] private scoreHolder scoreHold;
    [Header("Put the Opponent Team here!")]
    [SerializeField] private fielderPeltingScript fielderReference = null;

    private void Awake()
    {
        scoreHold.canScore = false;
    }

    //This is a bunch of BS settings

    private IEnumerator StartPitchingPhase()
    {
        //Resets game variables back to beginning
        fielderReference.hasStartedThrowingSequenceAlready = false;
        playerStateReference.playerState = 1;
        var rb = player.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
        player.GetComponent<ActivatePlayer>().enabled = true;
        //Reset camera back to the player, start pitching and increase the base variable
        yield return new WaitForSeconds(2);
        fielderReference.canThrow = false; //Set canThrow here so that it's guaranteed to not conflict with the coroutine before it stops
        StartCoroutine(fielderReference.BattingPhaseTimer());
    }
}
