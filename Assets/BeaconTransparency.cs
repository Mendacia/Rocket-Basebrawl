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
        myMat.SetFloat("RangeOffset", Vector3.Distance(transform.position, playerController.position) / 50);
        myMat.SetFloat("RangeOFF", Vector3.Distance(transform.position, playerController.position) / 50);
    }
}
