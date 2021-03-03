using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat_Gold : MonoBehaviour
{
    [SerializeField] private fielderPeltingScript fielder = null;
    private bool isGilded = false;
    private enum easterEggState
    {
        G,
        O,
        L,
        D,
        NA
    }
    private easterEggState currentEggState = easterEggState.NA;

    private void Update()
    {
        switch (currentEggState)
        {
            case easterEggState.NA:
                if (Input.anyKeyDown)
                {
                    if (Input.GetKeyDown(KeyCode.G))
                    {
                        currentEggState = easterEggState.G;
                    }
                }
                break;
            case easterEggState.G:
                if (Input.anyKeyDown)
                {
                    if (Input.GetKeyDown(KeyCode.O))
                    {
                        currentEggState = easterEggState.O;
                    }
                    else
                    {
                        currentEggState = easterEggState.NA;
                    }
                }

                break;
            case easterEggState.O:
                if (Input.anyKeyDown)
                {
                    if (Input.GetKeyDown(KeyCode.L))
                    {
                        currentEggState = easterEggState.L;
                    }
                    else
                    {
                        currentEggState = easterEggState.NA;
                    }
                }

                break;
            case easterEggState.L:
                if (Input.anyKeyDown)
                {
                    if (Input.GetKeyDown(KeyCode.D))
                    {
                        currentEggState = easterEggState.D;
                    }
                    else
                    {
                        currentEggState = easterEggState.NA;
                    }
                }

                break;
            case easterEggState.D:
                if (!isGilded)
                {
                    Debug.Log("Gilded");
                    isGilded = true;
                    fielder.Gilded = true;
                }
                else
                {
                    Debug.Log("UnGilded");
                    isGilded = false;
                    fielder.Gilded = false;
                }
                currentEggState = easterEggState.NA;
                break;
        }
    }
}
