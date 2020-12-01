using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fielderWhacked : MonoBehaviour
{
    private fielderPeltingScript peltingScript;
    [SerializeField] private GameObject ragdoll;
    [SerializeField] private float hitStrength = 1;

    private void Start()
    {
        peltingScript = gameObject.GetComponent<fielderPeltingScript>();
    }
    public void findFielder(Transform recievedFielder)
    {
        if (peltingScript.fieldingTeam.Contains(recievedFielder))
        {
            var realHitStrength = hitStrength * 10000;
            Debug.Log("Yeet");
            peltingScript.fieldingTeam.Remove(recievedFielder);
            var myRagdoll = Instantiate(ragdoll, recievedFielder.position, Quaternion.identity);
            Destroy(recievedFielder.gameObject);
            myRagdoll.GetComponentInChildren<Rigidbody>().AddForce(Random.Range(-realHitStrength, realHitStrength), realHitStrength, Random.Range(-realHitStrength, realHitStrength));
        }
    }
}
