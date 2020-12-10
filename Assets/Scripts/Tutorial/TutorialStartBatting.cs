using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStartBatting : MonoBehaviour
{
    [SerializeField] private TutorialGodScript tutorialReference = null;
    private bool hasActivated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasActivated)
        {
            hasActivated = true;
            tutorialReference.ShowBattingUI();
        }
    }
}
