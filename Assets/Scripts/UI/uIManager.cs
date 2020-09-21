using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uIManager : MonoBehaviour
{
    [SerializeField] private GameObject rootMenu;
    [SerializeField] private GameObject newGameMenu;
    [SerializeField] private GameObject continueMenu;

    public void ToggleRoot()
    {
        rootMenu.SetActive(!rootMenu.gameObject.activeSelf);
    }

    public void ToggleNewGame()
    {
        newGameMenu.SetActive(!rootMenu.gameObject.activeSelf);
    }

    public void ToggleContinue()
    {
        continueMenu.SetActive(!continueMenu.gameObject.activeSelf);
    }
}
