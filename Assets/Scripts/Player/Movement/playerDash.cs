using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDash : MonoBehaviour
{
    private Rigidbody playerRigidbody = null;

    public Vector3 recievedVector(Vector3 myVector)
    {
        return myVector;
    }

    //Get input code
    private void Start()
    {
        playerRigidbody = gameObject.GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            PushThePlayerForwardRealHard();
        }
    }
    public void PushThePlayerForwardRealHard()
    {
        playerRigidbody.AddForce(Vector3.forward * 100000);
    }
}
