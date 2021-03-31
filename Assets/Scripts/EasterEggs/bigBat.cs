using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigBat : MonoBehaviour
{
    [SerializeField] private GameObject batCH = null;
    [SerializeField] private GameObject batRB = null;

    private bool isBigBat = false;

    private enum easterEggState
    {
        NA,
        B,
        I,
        G,
        B2,
        A,
        T
    }

    private easterEggState currentEggState = easterEggState.NA;

    private void Update()
    {
        switch (currentEggState)
        {
            case easterEggState.NA:
                if (Input.anyKeyDown)
                {
                    if (Input.GetKeyDown(KeyCode.B))
                    {
                        currentEggState = easterEggState.B;
                    }
                }
                break;
            case easterEggState.B:
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
                    if (Input.GetKeyDown(KeyCode.G))
                    {
                        currentEggState = easterEggState.G;
                    }
                    else
                    {
                        currentEggState = easterEggState.NA;
                    }
                }

                break;
            case easterEggState.G:
                if (Input.anyKeyDown)
                {
                    if (Input.GetKeyDown(KeyCode.B))
                    {
                        currentEggState = easterEggState.B2;
                    }
                    else
                    {
                        currentEggState = easterEggState.NA;
                    }
                }

                break;
            case easterEggState.B2:
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
                if (!isBigBat)
                {
                    isBigBat = true;
                    batCH.transform.localScale = new Vector3(3, 3, 3);
                    batRB.transform.localScale = new Vector3(3, 3, 3);
                }
                else
                {
                    isBigBat = false;
                    batCH.transform.localScale = new Vector3(1, 1, 1);
                    batRB.transform.localScale = new Vector3(1, 1, 1);
                }
                currentEggState = easterEggState.NA;
                break;
        }
    }
}
