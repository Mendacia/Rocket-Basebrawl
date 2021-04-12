using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseSensitivity : MonoBehaviour
{
    public POVCamControl mouseSens;
    [SerializeField] Slider sensSlider;

    void Start()
    {
        switch (mouseSens.sensitivity)
        {
            case 5:
                sensSlider.value = 1;
                break;

            case 15:
                sensSlider.value = 2;
                break;

            case 30:
                sensSlider.value = 3;
                break;

            case 50:
                sensSlider.value = 4;
                break;

            case 65:
                sensSlider.value = 5;
                break;

            case 85:
                sensSlider.value = 6;
                break;

            case 100:
                sensSlider.value = 7;
                break;
        }
    }

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
