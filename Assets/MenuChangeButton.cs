using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuChangeButton : MonoBehaviour
{
    [SerializeField] private mainMenuManager menuManager = null;
    [SerializeField] private Animator myHolder = null;
    [Header("0 = Home, 1 = New Game, 2 = Continue, 3 = Settings")]
    [SerializeField] private int myMenu = 3;

    public void onButtonPressChangeMenu()
    {
        menuManager.triggerAnimationTransition(myHolder, myMenu);
    }
}
