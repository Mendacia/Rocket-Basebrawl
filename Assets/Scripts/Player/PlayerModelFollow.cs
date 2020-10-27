using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModelFollow : MonoBehaviour
{
    public Transform player;
    public Transform camRot;

    void Update()
    {
        transform.position = player.position;
        //Rotates player model in direction of the camera
        transform.eulerAngles = new Vector3(0, camRot.eulerAngles.y, 0);
    }
}
