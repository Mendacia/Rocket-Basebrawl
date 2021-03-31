using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smallHead : MonoBehaviour
{
    [SerializeField] private GameObject head = null;
    private bool isSmallHead = false;

    private enum easterEggState
    {
        NA,
        S,
        M,
        A,
        L,
        L2,
        H,
        E,
        A2,
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
                    if (Input.GetKeyDown(KeyCode.S))
                    {
                        currentEggState = easterEggState.S;
                    }
                }
                break;
            case easterEggState.S:
                if (Input.anyKeyDown)
                {
                    if (Input.GetKeyDown(KeyCode.M))
                    {
                        currentEggState = easterEggState.M;
                    }
                    else
                    {
                        currentEggState = easterEggState.NA;
                    }
                }

                break;
            case easterEggState.M:
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
                    if (Input.GetKeyDown(KeyCode.L))
                    {
                        currentEggState = easterEggState.L2;
                    }
                    else
                    {
                        currentEggState = easterEggState.NA;
                    }
                }

                break;
            case easterEggState.L2:
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
                        currentEggState = easterEggState.A2;
                    }
                    else
                    {
                        currentEggState = easterEggState.NA;
                    }
                }
                break;
            case easterEggState.A2:
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
                if (!isSmallHead)
                {
                    isSmallHead = true;
                    head.transform.localScale = new Vector3(.6f, .6f, .6f);
                }
                else
                {
                    isSmallHead = false;
                    head.transform.localScale = new Vector3(1, 1, 1);
                }
                currentEggState = easterEggState.NA;
                break;
        }
    }
}
