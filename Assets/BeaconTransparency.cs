using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconTransparency : MonoBehaviour
{
    [SerializeField] private Transform playerController;
    private float transparency;
    void Update()
    {
        transparency = (Vector3.Distance(transform.position, playerController.position) / 50);
    }
}
