using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconTransparency : MonoBehaviour
{
    [SerializeField] private Transform playerController;
    private float transparency;
    [SerializeField] private Material myMat;
    void Update()
    {
        myMat.SetFloat("Vector1_Offset", (Vector3.Distance(transform.position, playerController.position) *-1  / 50) + 1);
        myMat.SetFloat("Vector1_Off", (Vector3.Distance(transform.position, playerController.position) * -1 / 50) + 1);
    }
}
