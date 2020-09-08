using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class runningPhaseMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Rigidbody rb;
    [SerializeField]
    private Transform cameraRotationReference;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        var rotationY = cameraRotationReference.rotation.y;
        transform.rotation = new Quaternion(0, rotationY, 0, 0);


        //Big Temp Movement
        float movementZ = Input.GetAxis("Horizontal") * speed;
        float movementX = Input.GetAxis("Vertical") * -speed;
        rb.AddForce(new Vector3(movementX, 0, movementZ), ForceMode.VelocityChange);
    }
}
