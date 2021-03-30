using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigHead : MonoBehaviour
{
    [SerializeField] private GameObject head = null;
    private bool isBigHead = false;

    private enum easterEggState
    {
        NA,
        B,
        I,
        G,
        H,
        E,
        A,
        D
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
                    if (Input.GetKeyDown(KeyCode.H))
                    {
                        currentEggState = easterEggState.H;
                    }
                    else
                    {
                        currentEggState = easterEggState.NA;
                    }
                }

                break;
            case easterEggState.H:
                if (Input.anyKeyDown)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        currentEggState = easterEggState.E;
                    }
                    else
                    {
                        currentEggState = easterEggState.NA;
                    }
                }

                break;
            case easterEggState.E:
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
                if (!isBigHead)
                {
                    isBigHead = true;
                    head.transform.localScale = new Vector3(4, 4, 4);
                }
                else
                {
                    isBigHead = false;
                    head.transform.localScale = new Vector3(1, 1, 1);
                }
                currentEggState = easterEggState.NA;
                break;
        }
    }
}
