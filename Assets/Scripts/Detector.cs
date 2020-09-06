using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    public static bool isHittable = false;

    private void OnTriggerStay(Collider other)
    {
        isHittable = true;
        Debug.Log("This shouldn't be going off");
    }

    private void OnTriggerExit(Collider other)
    {
        isHittable = false;
    }
}
