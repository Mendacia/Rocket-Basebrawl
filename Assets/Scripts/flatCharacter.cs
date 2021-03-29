using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flatCharacter : MonoBehaviour
{
    [SerializeField] private GameObject model = null;
    private bool isFlat = false;

    private enum easterEggState
    {
        NA,
        F,
        L,
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
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        currentEggState = easterEggState.F;
                    }
                }
                break;
            case easterEggState.F:
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
                if (!isFlat)
                {
                    isFlat = true;
                    model.transform.localScale = new Vector3(1, 1, 0.05f);
                }
                else
                {
                    isFlat = false;
                    model.transform.localScale = new Vector3(1, 1, 1);
                }
                currentEggState = easterEggState.NA;
                break;
        }
    }
}
