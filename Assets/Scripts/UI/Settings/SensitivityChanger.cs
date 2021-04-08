using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensitivityChanger : MonoBehaviour
{
    public SensitivityHolder mouseSens;

    public void ChangeSensitivty(float mouseValue)
    {
        switch (mouseValue)
        {
            case 1:
                mouseSens.sensitivity = 5;
                break;

            case 2:
                mouseSens.sensitivity = 15;
                break;

            case 3:
                mouseSens.sensitivity = 30;
                break;

            case 4:
                mouseSens.sensitivity = 50;
                break;

            case 5:
                mouseSens.sensitivity = 65;
                break;

            case 6:
                mouseSens.sensitivity = 85;
                break;

            case 7:
                mouseSens.sensitivity = 100;
                break;
        }
    }
}
