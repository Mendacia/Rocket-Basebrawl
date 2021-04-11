using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FielderWhacker : MonoBehaviour
{
    [SerializeField] private fielderWhacked fielderWhackingScript;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Fielder")
        {
            Debug.Log("I have found a fuckler");
            fielderWhackingScript.FindAndDestroyFielder(other.transform);
        }
    }
}
