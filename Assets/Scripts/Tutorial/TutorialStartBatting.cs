using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStartBatting : MonoBehaviour
{
    [SerializeField] private TutorialGodScript tutorialReference = null;
    [SerializeField] private playerControls player = null;
    private bool hasActivated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasActivated)
        {
            hasActivated = true;
            player.playerState = 0;
            tutorialReference.ShowBattingUI();
        }
    }
}
