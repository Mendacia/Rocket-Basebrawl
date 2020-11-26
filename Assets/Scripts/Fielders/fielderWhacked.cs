using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fielderWhacked : MonoBehaviour
{
    private fielderPeltingScript peltingScript;

    private void Start()
    {
        peltingScript = gameObject.GetComponent<fielderPeltingScript>();
    }
    public void findFielder(Transform recievedFielder)
    {
        if (peltingScript.fieldingTeam.Contains(recievedFielder))
        {
            Debug.Log("Yeet");
            peltingScript.fieldingTeam.Remove(recievedFielder);
            recievedFielder.GetComponent<Rigidbody>().AddForce(Random.Range(-4000, 4000), 4000, Random.Range(-4000, 4000));
        }
    }
}
