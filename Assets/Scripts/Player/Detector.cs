using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    public static bool isHittable = false;
    public static List<Transform> ballCols = new List<Transform>();

    private void OnTriggerStay(Collider other)
    {
        isHittable = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Add to list
        if (other.gameObject.CompareTag("Ball"))
        {
            Transform ballT = other.GetComponent<Transform>();
            ballCols.Add(ballT);
            Debug.Log("This is still running");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isHittable = false;
        //Remove from list
        if (other.gameObject.CompareTag("Ball"))
        {
            Transform ballT = other.GetComponent<Transform>();
            ballCols.Remove(ballT);
        }
    }
}
