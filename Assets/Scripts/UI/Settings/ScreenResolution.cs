using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenResolution : MonoBehaviour
{
    private FullscreenToggle fsTog;

    Resolution[] resolutions;
    Dropdown dropdownMenu;
    void Start()
    {
        fsTog = GameObject.Find("FullscreenToggle").GetComponent<FullscreenToggle>();
        dropdownMenu = gameObject.GetComponent<Dropdown>();
        resolutions = Screen.resolutions;
        dropdownMenu.onValueChanged.AddListener(delegate { Screen.SetResolution(resolutions[dropdownMenu.value].width, resolutions[dropdownMenu.value].height, false); });
        for (int i = 0; i < resolutions.Length; i++)
        {
            dropdownMenu.options[i].text = ResToString(resolutions[i]);
            dropdownMenu.value = i;
            dropdownMenu.options.Add(new Dropdown.OptionData(dropdownMenu.options[i].text));
        }
    }

    public void SetResolution(int DropdownInt)
    {
        Screen.SetResolution(resolutions[DropdownInt].width, resolutions[DropdownInt].height, fsTog.fullscreen, 60);
    }

    string ResToString(Resolution res)
    {
        return res.width + " x " + res.height;
    }
}