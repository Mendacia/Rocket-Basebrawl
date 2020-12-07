using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialGodScript : MonoBehaviour
{
    [Header("Put Player Controller here!")]
    [SerializeField] private GameObject player = null;
    [SerializeField] private playerControls playerStateReference = null;
    [Header("Put ScoreHolder here!")]
    [SerializeField] private scoreHolder scoreHold;
    [Header("Put the Opponent Team here!")]
    [SerializeField] private fielderPeltingScript fielderReference = null;

    [Header("UI Elements")]
    //Images
    [SerializeField] private Image pitchingPhase = null;
    [SerializeField] private Image generalControls = null;
    [SerializeField] private Image battingPhase = null;
    //Buttons
    [SerializeField] private GameObject pitchingButton = null;
    [SerializeField] private GameObject generalButton = null;
    [SerializeField] private GameObject battingButton = null;
    
    private bool isInBattingPhase = false;


    private void Awake()
    {
        scoreHold.canScore = false;
        Cursor.visible = true;
    }

    private void Update()
    {
        switch (isInBattingPhase)
        {
            case true:
                if(scoreHold.score >= 3)
                {
                    //Do next phase
                }
                break;
        }
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

    //Started from a UI button

    public void StartPitching()
    {
        pitchingPhase.enabled = false;
        pitchingButton.SetActive(false);
        Cursor.visible = false;
    }

    public void StartMovement()
    {
        generalControls.enabled = false;
        generalButton.SetActive(false);
        playerStateReference.playerState = 2;
        Cursor.visible = false;
    }

    public void StartBatting()
    {
        battingPhase.enabled = false;
        battingButton.SetActive(false);
        fielderReference.startPeltingLoop();
        playerStateReference.playerState = 2;
        Cursor.visible = false;
    }
}
