using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardFacing : MonoBehaviour
{
    //Bro this boy facing the camera
    void Update()
    {
        transform.LookAt(Camera.main.transform.position, -Vector3.up);
    }
}
