using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationsForPlayer : MonoBehaviour
{
    [Header("This should be the Main Camera")]
    [SerializeField] private Transform cameraRotation = null;

    void Update()
    {
        transform.eulerAngles = new Vector3(0, cameraRotation.eulerAngles.y, 0);
    }
}
