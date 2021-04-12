using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSwap : MonoBehaviour
{
    [SerializeField] private GameObject Bat1 = null;
    [SerializeField] private GameObject Bat2 = null;
    [SerializeField] private GameObject Swing1 = null;
    [SerializeField] private GameObject Swing2 = null;
    [SerializeField] private GameObject Swing1B = null;
    [SerializeField] private GameObject Swing2B = null;
    private bool isSwapped = false;

    private enum easterEggState
    {
        NA,
        S,
        W,
        A,
        P
    }

    private easterEggState currentEggState = easterEggState.NA;

    private void Update()
    {
        switch (currentEggState)
        {
            case easterEggState.NA:
                if (Input.anyKeyDown)
                {
                    if (Input.GetKeyDown(KeyCode.S))
                    {
                        currentEggState = easterEggState.S;
                    }
                }
                break;
            case easterEggState.S:
                if (Input.anyKeyDown)
                {
                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        currentEggState = easterEggState.W;
                    }
                    else
                    {
                        currentEggState = easterEggState.NA;
                    }
                }

                break;
            case easterEggState.W:
                if (Input.anyKeyDown)
                {
                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        currentEggState = easterEggState.A;
                    }
                    else
                    {
                        currentEggState = easterEggState.NA;
                    }
                }

                break;
            case easterEggState.A:
                if (Input.anyKeyDown)
                {
                    if (Input.GetKeyDown(KeyCode.P))
                    {
                        currentEggState = easterEggState.P;
                    }
                    else
                    {
                        currentEggState = easterEggState.NA;
                    }
                }
                break;

            case easterEggState.P:
                if (!isSwapped)
                {
                    isSwapped = true;
                    Bat1.SetActive (false);
                    Swing1.SetActive (false);
                    Swing1B.SetActive (false);
                    Bat2.SetActive (true);
                    Swing2.SetActive (true);
                }
                else
                {
                    isSwapped = false;
                    Bat1.SetActive (true);
                    Swing1.SetActive (true);
                    Bat2.SetActive (false);
                    Swing2B.SetActive (false);
                    Swing2.SetActive (false);
                }
                currentEggState = easterEggState.NA;
                break;
        }
    }
}
