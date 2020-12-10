using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
using Cinemachine;

public class DeactiveateCamera : MonoBehaviour
{
    public GameObject startCam;
    public CinemachineDollyCart dolCart;
    public Transform playerRotation;

    public static bool dollyActive = false;

    private void Awake()
    {
        dollyActive = true;
    }

    public void skipDolly()
    {
        dollyActive = false;
        startCam.SetActive(false);
    }
}
