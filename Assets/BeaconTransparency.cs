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
        myMat.SetFloat("Vector1_EDB56D90", Vector3.Distance(transform.position, playerController.position) / 50);
        myMat.SetFloat("Vector1_DC2039FF", Vector3.Distance(transform.position, playerController.position) / 50);
    }
}
