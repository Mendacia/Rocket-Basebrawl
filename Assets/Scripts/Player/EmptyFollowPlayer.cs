using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyFollowPlayer : MonoBehaviour
{
    [Header("This should be the Player Controller")]
    [SerializeField] private Transform player;
    [Header("This should be the Main Camera")]
    [SerializeField] private Transform cameraRotation;

    void Update()
    {
        transform.position = player.position;
        //Rotates player model in direction of the camera
        transform.eulerAngles = new Vector3(0, cameraRotation.eulerAngles.y, 0);
    }
}
