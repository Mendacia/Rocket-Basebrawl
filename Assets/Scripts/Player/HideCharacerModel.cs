using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class HideCharacerModel : MonoBehaviour
{
    [Header("Put in CM 3rd Person Normal")]
    [SerializeField] private CinemachineVirtualCamera vcam = null;
    [Header("Put in the Child object with the Mesh Renderer")]
    [SerializeField] private Renderer playerModel;
    CinemachinePOV POVCam;

    void Start()
    {
        if (vcam != null)
        {
            POVCam = vcam.GetCinemachineComponent<CinemachinePOV>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (POVCam.m_VerticalAxis.Value <= -35)
        {
            playerModel.enabled = false;
        }
        else
        {
            playerModel.enabled = true;
        }
    }
}
