using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseCheckpointScript : MonoBehaviour
{
    [SerializeField] private string PreviousBase = ("Previous Base");
    [SerializeField] private string thisBase = ("This Base");
    private string currentBase = ("Home");

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == ("Player"))
        {
            
        }
    }
}
