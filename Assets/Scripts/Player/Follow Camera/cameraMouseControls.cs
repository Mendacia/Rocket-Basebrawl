using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMouseControls : MonoBehaviour
{
    [SerializeField]
    private GameObject followTarget, camRotationY;
    [SerializeField]
    private float sensitivityX, sensitivityY, camMinX, camMaxX;
    float camCurrentY;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        var cameraAngle = camRotationY.transform.rotation.eulerAngles;
        camCurrentY += Input.GetAxis("Mouse Y") * sensitivityY;
        camCurrentY = Mathf.Clamp(camCurrentY, camMinX, camMaxX);
        cameraAngle.x = camCurrentY;
        camRotationY.transform.rotation = Quaternion.Euler(cameraAngle);

        transform.Rotate(new Vector3(0, Input.GetAxisRaw("Mouse X") * sensitivityX, 0));
        transform.position = followTarget.transform.position;

    }
}
