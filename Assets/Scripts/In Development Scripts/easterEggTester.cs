using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class easterEggTester : MonoBehaviour
{
    [SerializeField] private GameObject field = null;
    [SerializeField] private Material woodyMat = null;
    [SerializeField] private BaseballEffectHolder baseEffect = null;
    private enum easterEggState
    {
        NA,
        W,
        O,
        O2,
        D,
        Y
    }
    private easterEggState currentEggState = easterEggState.NA;

    private void Update()
    {
        switch (currentEggState)
        {
            case easterEggState.NA:
                if (Input.anyKeyDown)
                {
                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        currentEggState = easterEggState.W;
                    }
                }
                break;
            case easterEggState.W:
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
                    if (Input.GetKeyDown(KeyCode.O))
                    {
                        currentEggState = easterEggState.O2;
                    }
                    else
                    {
                        currentEggState = easterEggState.NA;
                    }
                }

                break;
            case easterEggState.O2:
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
                field.GetComponent<MeshRenderer>().material = woodyMat;
                baseEffect.ChangeDirtTexture();
                currentEggState = easterEggState.NA;
                break;
        }
    }
}
