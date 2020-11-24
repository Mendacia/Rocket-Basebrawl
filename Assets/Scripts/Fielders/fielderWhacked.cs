using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fielderWhacked : MonoBehaviour
{
    private fielderPeltingScript peltingScript;
    [SerializeField] private List<GameObject> fieldingTeam;

    //This should run before start, Unity works how I think it does, which, it most certainly doesn't
    public void givetheFielderToFielderWhackedScript(GameObject receivedFielder)
    {
        fieldingTeam.Add(receivedFielder);
    }

    private void Start()
    {
        peltingScript = gameObject.GetComponent<fielderPeltingScript>();
    }

    public void findFielder(GameObject recievedFielder)
    {
        if (fieldingTeam.Contains(recievedFielder))
        {
            Debug.Log("Yeet");
            fieldingTeam.Remove(recievedFielder);
            peltingScript.fieldingTeam.Remove(recievedFielder.transform);
            recievedFielder.GetComponent<Rigidbody>().AddForce(0, 4000, 0);
        }
    }
}
