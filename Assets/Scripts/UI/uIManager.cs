using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class uIManager : MonoBehaviour
{
    [SerializeField] private GameObject rootMenu;
    [SerializeField] private GameObject newGameMenu;
    [SerializeField] private GameObject continueMenu;
    [SerializeField] private EventSystem rootSystem;
    [SerializeField] private EventSystem newGameSystem;
    [SerializeField] private EventSystem continueSystem;
    [SerializeField] private Button rootButton;
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueButton;

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

    IEnumerator RootSelectButton()
    {
        yield return null;
        rootSystem.SetSelectedGameObject(null);
        rootSystem.SetSelectedGameObject(continueButton.gameObject);
    }
}
