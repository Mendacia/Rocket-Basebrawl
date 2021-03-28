using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenuManager : MonoBehaviour
{
    [SerializeField] private Animator homePanel, newGamePanel, continuePanel, settingsPanel;

    public enum menuState
    {
        HOME,
        NEWGAME,
        CONTINUE,
        SETTINGS
    }
    public menuState currentMenu;

    private void Start()
    {
        currentMenu = menuState.HOME;
        newGamePanel.Play("Stay off screen");
        continuePanel.Play("Stay off screen");
        settingsPanel.Play("Stay off screen");
    }

    public void triggerAnimationTransition(Animator targetAnimator, int targetMenuState)
    {
        switch (currentMenu)
        {
            case menuState.HOME:
                {
                    homePanel.SetTrigger("turnOff");
                    targetAnimator.SetTrigger("turnOn");
                    currentMenu = (menuState)targetMenuState;
                    return;
                }
            case menuState.NEWGAME:
                {
                    newGamePanel.SetTrigger("turnOff");
                    targetAnimator.SetTrigger("turnOn");
                    currentMenu = (menuState)targetMenuState;
                    return;
                }
            case menuState.CONTINUE:
                {
                    continuePanel.SetTrigger("turnOff");
                    targetAnimator.SetTrigger("turnOn");
                    currentMenu = (menuState)targetMenuState;
                    return;
                }
            case menuState.SETTINGS:
                {
                    settingsPanel.SetTrigger("turnOff");
                    targetAnimator.SetTrigger("turnOn");
                    currentMenu = (menuState)targetMenuState;
                    return;
                }
        }
    }
}
