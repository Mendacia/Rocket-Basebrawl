using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialUIPrompts : MonoBehaviour
{
    [SerializeField] private List<GameObject> UIList;
    [SerializeField] private List<GameObject> ObjectiveList;
    [SerializeField] private TutorialScoreHolder scoreHold = null;

    private void Start()
    {
        PitchingPhaseOn();
    }

    //Activate this on start
    public void PitchingPhaseOn()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;
        UIList[0].SetActive(true);
    }
    public void PitchingPhaseOff()
    {
        Cursor.visible = false;
        Time.timeScale = 1;
        UIList[0].SetActive(false);
        ObjectiveList[0].SetActive(true);
    }

    //Activate this after pitching
    public void MovementPhaseOn()
    {
        Cursor.visible = true;
        Time.timeScale = 0;
        UIList[1].SetActive(true);
        ObjectiveList[0].SetActive(false);
    }
    public void MovementPhaseOff()
    {
        Cursor.visible = false;
        Time.timeScale = 1;
        TutorialStateMachine.SetCurrentState(TutorialState.WALKING);
        UIList[1].SetActive(false);
        ObjectiveList[1].SetActive(true);
    }

    //Activate this when reaching the first base
    public void BattingPhaseOn()
    {
        Cursor.visible = true;
        Time.timeScale = 0;
        UIList[2].SetActive(true);
        ObjectiveList[1].SetActive(false);
    }
    public void BattingPhaseOff()
    {
        Cursor.visible = false;
        Time.timeScale = 1;
        scoreHold.score = 0;
        TutorialStateMachine.SetCurrentState(TutorialState.RUNNING);
        UIList[2].SetActive(false);
        ObjectiveList[2].SetActive(true);
    }

    //Activate this to end the tutorial
    public void EndPhaseOn()
    {
        Cursor.visible = true;
        Time.timeScale = 0;
        UIList[3].SetActive(true);
        ObjectiveList[2].SetActive(false);
    }
    public void EndPhaseOff()
    {
        Cursor.visible = false;
        Time.timeScale = 1;
        UIList[3].SetActive(false);
    }

    //Put this on the button to load the main game
    public void LoadMainScene()
    {
        SceneManager.LoadScene(1);
    }
}
