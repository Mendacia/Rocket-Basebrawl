using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullscreenToggle : MonoBehaviour
{
    bool fullscreen = true;

    private void Start()
    {
        Screen.fullScreen = true;
    }
    public void FullscreenSwitch()
    {
        if (fullscreen == true)
        {
            Screen.fullScreen = false;
            fullscreen = false;
        }
        if(fullscreen == false)
        {
            Screen.fullScreen = true;
            fullscreen = true;
        }
    }
}