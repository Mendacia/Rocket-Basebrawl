using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullscreenToggle : MonoBehaviour
{
    public bool fullscreen = true;
    private Toggle fsToggle;

    private void Start()
    {
        fsToggle = gameObject.GetComponent<Toggle>();
        Screen.fullScreen = true;
    }
    public void FullscreenSwitch()
    {
        if (fsToggle.isOn == true)
        {
            Screen.fullScreen = true;
            fullscreen = true;
        }
        if(fsToggle.isOn == false)
        {
            Screen.fullScreen = false;
            fullscreen = false;
        }
    }
}