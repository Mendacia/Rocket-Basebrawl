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
        rootButton.Select();
    }


    public void ToggleNewGame()
    {
        newGameMenu.SetActive(!newGameMenu.gameObject.activeSelf);
        newGameButton.Select();
    }

    public void ToggleContinue()
    {
        continueMenu.SetActive(!continueMenu.gameObject.activeSelf);
        continueButton.Select();
    }

    IEnumerator RootSelectButton()
    {
        /*yield return null;
        rootSystem.SetSelectedGameObject(null);
        new WaitForSeconds(1f);
        rootSystem.SetSelectedGameObject(rootButton.gameObject);*/
        yield return new WaitForSeconds(1f);
        //rootButton.Select();
    }
    IEnumerator NewGameSelectButton()
    {
        /*yield return null;
        rootSystem.SetSelectedGameObject(null);
        new WaitForSeconds(1f);
        rootSystem.SetSelectedGameObject(newGameButton.gameObject);*/
        yield return new WaitForSeconds(1f);
        //newGameButton.Select();
    }
    IEnumerator ContinueSelectButton()
    {
        /*yield return null;
        rootSystem.SetSelectedGameObject(null);
        new WaitForSeconds(1f);
        rootSystem.SetSelectedGameObject(continueButton.gameObject);*/
        yield return new WaitForSeconds(1f);
        //continueButton.Select();
    }
}
