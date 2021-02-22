using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelRotation : MonoBehaviour
{
    [Header("This should be the model")]
    [SerializeField] private Transform playerModel = null;
    [Header("This should be the player")]
    [SerializeField] private Transform rotationReference = null;

    void Update()
    {
        transform.position = playerModel.position;
        //Rotates player model in direction of the camera
        transform.eulerAngles = new Vector3(0, rotationReference.eulerAngles.y, 0);
    }
}
