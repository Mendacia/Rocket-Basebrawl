using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    public static bool isHittable = false;
    public static List<Collider> ballCols = new List<Collider>();

    private void OnTriggerStay(Collider other)
    {
        isHittable = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Add to list
        if (!ballCols.Contains(other))
        {
            ballCols.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isHittable = false;
        //Remove from list
        ballCols.Remove(other);
    }
}
