using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseSensitivity : MonoBehaviour
{
    public POVCamControl mouseSens;

    public void ChangeSensitivty(float mouseValue)
    {
        switch (mouseValue)
        {
            case 1:
                mouseSens.sensitivity = 20;
                break;

            case 2:
                mouseSens.sensitivity = 40;
                break;

            case 3:
                mouseSens.sensitivity = 60;
                break;

            case 4:
                mouseSens.sensitivity = 80;
                break;

            case 5:
                mouseSens.sensitivity = 100;
                break;
        }
    }
}
