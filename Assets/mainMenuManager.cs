using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mainMenuManager : MonoBehaviour
{
    [SerializeField] private Animator homePanel, newGamePanel, continuePanel, settingsPanel, indexPanel, splashScreen;
    private bool initialized = false;

    public enum menuState
    {
        HOME,
        NEWGAME,
        CONTINUE,
        SETTINGS,
        SPLASH
    }
    public menuState currentMenu;

    private void Start()
    {
        currentMenu = menuState.SPLASH;
        indexPanel.Play("indexOffScreen");
        homePanel.Play("Stay off screen");
        newGamePanel.Play("Stay off screen");
        continuePanel.Play("Stay off screen");
        settingsPanel.Play("Stay off screen");
    }

    private void Update()
    {
        if (Input.anyKeyDown && !initialized)
        {
            OpenMenu();
            initialized = true;
        }
    }

    private void OpenMenu()
    {
        splashScreen.SetTrigger("Leave");
        indexPanel.SetBool("On", true);
        homePanel.SetBool("On", true);
        indexPanel.SetTrigger("IndexOn");
        currentMenu = menuState.HOME;
    }

    public void triggerAnimationTransition(Animator targetAnimator, int targetMenuState)
    {
        switch (currentMenu)
        {
            case menuState.HOME:
                {
                    homePanel.SetBool("On", false);
                    targetAnimator.SetBool("On", true);
                    currentMenu = (menuState)targetMenuState;
                    return;
                }
            case menuState.NEWGAME:
                {
                    newGamePanel.SetBool("On", false);
                    targetAnimator.SetBool("On", true);
                    currentMenu = (menuState)targetMenuState;
                    return;
                }
            case menuState.CONTINUE:
                {
                    continuePanel.SetBool("On", false);
                    targetAnimator.SetBool("On", true);
                    currentMenu = (menuState)targetMenuState;
                    return;
                }
            case menuState.SETTINGS:
                {
                    settingsPanel.SetBool("On", false);
                    targetAnimator.SetBool("On", true);
                    currentMenu = (menuState)targetMenuState;
                    return;
                }
        }
    }
}
