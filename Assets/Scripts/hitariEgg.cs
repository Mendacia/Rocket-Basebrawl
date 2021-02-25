using GameAnalyticsSDK.Setup;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitariEgg : MonoBehaviour
{
    [SerializeField] private GameObject cheetah;
    [SerializeField] private GameObject hitari;

    private bool isHitari = false;

    private enum easterEggState
    {
        NA,
        H,
        I,
        T,
        A,
        R,
        I2
    }
    private easterEggState currentEggState = easterEggState.NA;

    private void Update()
    {
        switch (currentEggState)
        {
            case easterEggState.NA:
                if (Input.anyKeyDown)
                {
                    if (Input.GetKeyDown(KeyCode.H))
                    {
                        currentEggState = easterEggState.H;
                    }
                }
                break;
            case easterEggState.H:
                if (Input.anyKeyDown)
                {
                    if (Input.GetKeyDown(KeyCode.I))
                    {
                        currentEggState = easterEggState.I;
                    }
                    else
                    {
                        currentEggState = easterEggState.NA;
                    }
                }

                break;
            case easterEggState.I:
                if (Input.anyKeyDown)
                {
                    if (Input.GetKeyDown(KeyCode.T))
                    {
                        currentEggState = easterEggState.T;
                    }
                    else
                    {
                        currentEggState = easterEggState.NA;
                    }
                }

                break;
            case easterEggState.T:
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
                    if (Input.GetKeyDown(KeyCode.R))
                    {
                        currentEggState = easterEggState.R;
                    }
                    else
                    {
                        currentEggState = easterEggState.NA;
                    }
                }
                break;
            case easterEggState.R:
                if (Input.anyKeyDown)
                {
                    if (Input.GetKeyDown(KeyCode.I))
                    {
                        currentEggState = easterEggState.I2;
                    }
                    else
                    {
                        currentEggState = easterEggState.NA;
                    }
                }

                break;
            case easterEggState.I2:
                if (!isHitari)
                {
                    isHitari = true;
                    hitari.SetActive(true);
                    cheetah.SetActive(false);
                }
                else
                {
                    isHitari = false;
                    hitari.SetActive(false);
                    cheetah.SetActive(true);
                }
                currentEggState = easterEggState.NA;
                break;
        }
    }
}