using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class silhouetteMode : MonoBehaviour
{
    [SerializeField] private GameObject player = null;
    [SerializeField] private GameObject batCH = null;
    [SerializeField] private GameObject batR = null;
    [SerializeField] private GameObject batS = null;
    [SerializeField] private Material playerMat = null;
    [SerializeField] private Material batCHMat = null;
    [SerializeField] private Material batRMat = null;
    [SerializeField] private Material batSMat = null;
    [SerializeField] private Material silMat = null;
    private bool isSilhouetted = false;
    private enum easterEggState
    {
        NA,
        S,
        I,
        L,
        L2,
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
                    if (Input.GetKeyDown(KeyCode.S))
                    {
                        currentEggState = easterEggState.S;
                    }
                }
                break;
            case easterEggState.S:
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
                if (!isSilhouetted)
                {
                    isSilhouetted = true;
                    player.GetComponent<SkinnedMeshRenderer>().material = playerMat;
                    batCH.GetComponent<MeshRenderer>().material = batCHMat;
                    batR.GetComponent<MeshRenderer>().material = batRMat;
                    batS.GetComponentInChildren<MeshRenderer>().material = batSMat;
                }
                else
                {
                    isSilhouetted = false;
                    player.GetComponent<SkinnedMeshRenderer>().material = silMat;
                    batCH.GetComponent<MeshRenderer>().material = silMat;
                    batR.GetComponent<MeshRenderer>().material = silMat;
                    batS.GetComponentInChildren<MeshRenderer>().material = silMat;
                }
                currentEggState = easterEggState.NA;
                break;
        }
    }
}
