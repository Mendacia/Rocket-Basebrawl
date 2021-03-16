using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fielderWhacked : MonoBehaviour
{
    [SerializeField] private fielderPeltingScript peltingScript;
    [SerializeField] private GameObject ragdoll;
    [SerializeField] private float hitStrength = 1;

    private void Start()
    {
        peltingScript = gameObject.GetComponent<fielderPeltingScript>();
    }
}
