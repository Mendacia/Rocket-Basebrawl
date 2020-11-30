using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class uIManager : MonoBehaviour
{
    [SerializeField] private GameObject rootMenu = null;
    [SerializeField] private GameObject newGameMenu = null;
    [SerializeField] private GameObject continueMenu = null;
    [SerializeField] private Button rootButton = null;
    [SerializeField] private Button newGameButton = null;
    [SerializeField] private Button continueButton = null;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

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
}
