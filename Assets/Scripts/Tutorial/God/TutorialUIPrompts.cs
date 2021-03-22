using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialUIPrompts : MonoBehaviour
{
    [SerializeField] private List<GameObject> UIList;
    
    //Activate this on start
    public void PitchingPhaseOn()
    {
        Time.timeScale = 0;
        UIList[0].SetActive(true);
    }
    public void PitchingPhaseOff()
    {

    }

    //Activate this after pitching
    public void MovementPhaseOn()
    {
        Time.timeScale = 0;
        UIList[1].SetActive(true);
    }
    public void MovementPhaseOff()
    {

    }

    //Activate this when reaching the first base
    public void BattingPhaseOn()
    {
        Time.timeScale = 0;
        UIList[2].SetActive(true);
    }
    public void BattingPhaseOff()
    {
        Time.timeScale = 0;
        UIList[2].SetActive(true);
    }

    //Activate this to end the tutorial
    public void EndPhaseOn()
    {
        Time.timeScale = 0;
        UIList[3].SetActive(true);
    }
    public void EndPhaseOff()
    {

    }

    //Put this on the button to load the main game
    public void LoadMainScene()
    {
        SceneManager.LoadScene(1);
    }
}
