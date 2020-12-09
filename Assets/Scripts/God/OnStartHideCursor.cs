using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStartHideCursor : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
