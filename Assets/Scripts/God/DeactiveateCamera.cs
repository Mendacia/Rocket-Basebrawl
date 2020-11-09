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
    public GameObject pressToStartText;

    public static bool dollyActive = false;

    private void Awake()
    {
        dollyActive = true;
    }

    public void skipDolly()
    {
        StartCoroutine(dolSpeed());
    }
    
    //Setting speed instead of immediately turning off the camera to correctly orientate the player
    public IEnumerator dolSpeed()
    {
        dolCart.m_Speed = 10000;
        yield return new WaitForSeconds(0.5f);
        dollyActive = false;
        pressToStartText.SetActive(false);
        startCam.SetActive(false);
    }
}
