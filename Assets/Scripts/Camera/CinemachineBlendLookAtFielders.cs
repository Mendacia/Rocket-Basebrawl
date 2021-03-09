using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineBlendLookAtFielders : MonoBehaviour
{
    [SerializeField] private AimOnKeypress aiming = null;
    private aimModeSnapping snapping = null;
    [SerializeField] private GameObject vcamMaster = null;
    [SerializeField] private CinemachineVirtualCamera vcam = null;
    private Transform fielderLocation;

    private void Start()
    {
        snapping = GameObject.Find("BunnyTeam").GetComponent<aimModeSnapping>();
    }

    void Update()
    {
        if(snapping != null)
        {
            fielderLocation = snapping.fielderPosition;

            if (aiming.boosted == true && fielderLocation != null)
            {
                vcamMaster.SetActive(true);
                vcam.LookAt = fielderLocation;
            }
            else
            {
                vcamMaster.SetActive(false);
            }
        }
    }
}