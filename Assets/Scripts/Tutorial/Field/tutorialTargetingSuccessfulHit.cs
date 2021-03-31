using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialTargetingSuccessfulHit : MonoBehaviour
{
    [Header("Make sure this is actually set up on the prefab")]
    [SerializeField] private GameObject baseballPrefab = null;
    private TutorialScoreHolder scoreHold;
    private TutorialUIPrompts tutorialUI;

    private void Start()
    {
        scoreHold = GameObject.Find("TutorialGod").GetComponent<TutorialScoreHolder>();
        tutorialUI = GameObject.Find("TutorialGod").GetComponent<TutorialUIPrompts>();
    }

    public void SpawnTheBaseballPrefabAndSendItInTheDirectionThePlayerIsFacing(bool gold, Vector3 mPos)
    {
        var myBaseballObject = Instantiate(baseballPrefab);
        myBaseballObject.transform.position = mPos;
        myBaseballObject.transform.rotation = Camera.main.transform.rotation;
        myBaseballObject.transform.position = myBaseballObject.transform.position + new Vector3(0, 0.5f, 1);

        if (TutorialStateMachine.GetCurrentState() == TutorialState.BATTING)
        {
            scoreHold.score++;
            if(scoreHold.score >= 3)
            {
                tutorialUI.MovementPhaseOn();
                TutorialStateMachine.SetCurrentState(TutorialState.FROZEN);
            }
        }
        if (TutorialStateMachine.GetCurrentState() == TutorialState.RUNNING)
        {
            scoreHold.score++;
            if (scoreHold.score >= 3)
            {
                tutorialUI.EndPhaseOn();
                TutorialStateMachine.SetCurrentState(TutorialState.FROZEN);
            }
        }
    }
}