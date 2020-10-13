using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.InputSystem.InputAction;
using Cinemachine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pause;
    public static bool isPaused = false;

    public CinemachineBrain brain;

    public void PauseGame(CallbackContext context)
    {
        if (context.performed)
        {
            if (isPaused == true)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pause.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
        Cursor.visible = false;
        brain.enabled = true;
    }

    public void Pause()
    {
        pause.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
        Cursor.visible = true;
        brain.enabled = false;
    }

    public void ReturnToTitle()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
