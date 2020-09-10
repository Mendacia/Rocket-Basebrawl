using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class baseCheckpointScript : MonoBehaviour
{
    [SerializeField] private currentBaseHolder GOD;
    [SerializeField] private Text uiBaseText;
    [SerializeField] private string PreviousBase = ("Previous Base");
    [SerializeField] private string thisBase = ("This Base");

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == ("Player")) 
        {
            if(GOD.currentBase == PreviousBase)
            {
                GOD.currentBase = thisBase;
                uiBaseText.text = GOD.currentBase;
            }
        }
    }
}
