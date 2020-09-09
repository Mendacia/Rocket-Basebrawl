using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class runningPhaseMovement : MonoBehaviour
{
    [Header("Player Speed")]
    [SerializeField] private float speed = 1;
    [Header ("Plug the root camera object in here")]
    [SerializeField] private Transform cameraRotationReferenceY = null;
    private Rigidbody rb;
    
    //Inputs that I need to carry from Update to FixedUpdate
    private float movementX = 0;
    private float movementZ = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        //Taking Inputs
        movementX = Input.GetAxis("Horizontal") * speed;
        movementZ = Input.GetAxis("Vertical") * speed;
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.LookRotation(cameraRotationReferenceY.forward, Vector3.up);
        rb.velocity = transform.right * movementX + transform.forward * movementZ;
    }
}
