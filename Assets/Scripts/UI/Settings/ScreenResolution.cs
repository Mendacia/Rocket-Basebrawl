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
        dropdownMenu.GetComponent<Dropdown>().ClearOptions();
        resolutions = Screen.resolutions;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (!dropdownMenu.GetComponent<Dropdown>().options.Contains(new Dropdown.OptionData(ResToString(resolutions[i])))/* && resolutions[i].refreshRate == 60*/)
            {
                dropdownMenu.GetComponent<Dropdown>().options.Add(new Dropdown.OptionData(ResToString(resolutions[i])));
                dropdownMenu.GetComponent<Dropdown>().value = i;
            }
        }
    }
    public void SetResolution(int DropdownInt)
    {
        Screen.SetResolution(resolutions[DropdownInt].width, resolutions[DropdownInt].height, fsTog.fullscreen, resolutions[DropdownInt].refreshRate);
    }

    string ResToString(Resolution res)
    {
        return res.width + " x " + res.height;
    }
}