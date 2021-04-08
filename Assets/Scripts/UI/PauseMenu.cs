using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pause;
    public static bool isPaused = false;
    private bool inLeave = false;
    [SerializeField] private Image CurrentTaunt;
    [SerializeField] private Text currentScore, currentUnstableScore;
    [SerializeField] private Animator LeaveMenu;

    [SerializeField] private fielderPeltingScript fielderPelting;
    [SerializeField] private scoreUpdater updaterScript;

    [SerializeField] private Button quit, settings, cont;

    public void PauseGame(CallbackContext context)
    {
        if (context.performed)
        {
            CurrentTaunt.sprite = BallIconHolder.GetIcon(BallResult.UNTHROWN, fielderPelting.GetFielderTauntLevel());
            currentScore.text = scoreHolder.scoreStatic.score.ToString();
            currentUnstableScore.text = "+" + updaterScript.GetTheScoreUpdater().ToString();

            if (inLeave)
            {
                inLeave = false;
                LeaveMenu.SetTrigger("Close");
                LeaveMenu.gameObject.SetActive(false);
                quit.enabled = true;
                settings.enabled = true;
                cont.enabled = true;
            }
            else if (isPaused == true)
            {
                Resume();
            }
            else if(/*Time.timeScale != 0*/ Time.timeScale == 1 && WorldStateMachine.GetCurrentState() != WorldState.FROZEN)
            {
                Pause();
            }
        }
    }

    public void LeaveMenuOpen()
    {
        quit.enabled = false;
        settings.enabled = false;
        cont.enabled = false;
        LeaveMenu.gameObject.SetActive(true);
        LeaveMenu.SetTrigger("Open");
        inLeave = true;
    }

    public void Resume()
    {
        pause.GetComponent<Animator>().SetTrigger("Close");
        pause.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void Pause()
    {
        pause.SetActive(true);
        pause.GetComponent<Animator>().SetTrigger("Open");
        Time.timeScale = 0;
        isPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ReturnToTitle()
    {
        isPaused = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
