using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickChangeCamera : MonoBehaviour
{
    public GameObject cineCam, liamCam;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CineMachineCamera();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            LiamCamera();
        }
    }

    void CineMachineCamera()
    {
        cineCam.SetActive(true);
        liamCam.SetActive(false);
    }

    void LiamCamera()
    {
        liamCam.SetActive(true);
        cineCam.SetActive(false);
    }
}
