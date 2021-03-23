using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorialTextUpdater : MonoBehaviour
{
    private Text ObjectiveText;
    private TutorialScoreHolder scoreHold;

    private void Start()
    {
        ObjectiveText = this.gameObject.GetComponent<Text>();
        scoreHold = GameObject.Find("TutorialGod").GetComponent<TutorialScoreHolder>();
    }

    private void Update()
    {
        ObjectiveText.text = "Hit 3 balls (" + scoreHold.score.ToString() + "/3)";
    }
}
