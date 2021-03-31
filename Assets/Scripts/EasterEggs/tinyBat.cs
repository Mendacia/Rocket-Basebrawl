using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tinyBat : MonoBehaviour
{
    [SerializeField] private GameObject batCH = null;
    [SerializeField] private GameObject batRB = null;

    private bool isTinyBat = false;

    private enum easterEggState
    {
        NA,
        T,
        I,
        N,
        Y,
        B,
        A,
        T2
    }

    private easterEggState currentEggState = easterEggState.NA;

    private void Update()
    {
        switch (currentEggState)
        {
            case easterEggState.NA:
                if (Input.anyKeyDown)
                {
                    if (Input.GetKeyDown(KeyCode.T))
                    {
                        currentEggState = easterEggState.T;
                    }
                }
                break;
            case easterEggState.T:
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
                    if (Input.GetKeyDown(KeyCode.N))
                    {
                        currentEggState = easterEggState.N;
                    }
                    else
                    {
                        currentEggState = easterEggState.NA;
                    }
                }

                break;
            case easterEggState.N:
                if (Input.anyKeyDown)
                {
                    if (Input.GetKeyDown(KeyCode.Y))
                    {
                        currentEggState = easterEggState.Y;
                    }
                    else
                    {
                        currentEggState = easterEggState.NA;
                    }
                }

                break;
            case easterEggState.Y:
                if (Input.anyKeyDown)
                {
                    if (Input.GetKeyDown(KeyCode.B))
                    {
                        currentEggState = easterEggState.B;
                    }
                    else
                    {
                        currentEggState = easterEggState.NA;
                    }
                }

                break;
            case easterEggState.B:
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
                        currentEggState = easterEggState.T2;
                    }
                    else
                    {
                        currentEggState = easterEggState.NA;
                    }
                }

                break;

            case easterEggState.T2:
                if (!isTinyBat)
                {
                    isTinyBat = true;
                    batCH.transform.localScale = new Vector3(.5f, .5f, .5f);
                    batRB.transform.localScale = new Vector3(.5f, .5f, .5f);
                }
                else
                {
                    isTinyBat = false;
                    batCH.transform.localScale = new Vector3(1, 1, 1);
                    batRB.transform.localScale = new Vector3(1, 1, 1);
                }
                currentEggState = easterEggState.NA;
                break;
        }
    }
}
