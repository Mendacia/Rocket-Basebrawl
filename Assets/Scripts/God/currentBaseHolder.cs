using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class currentBaseHolder : MonoBehaviour
{
    [System.NonSerialized]public string currentBase = "Home";
    private bool hasLeftHome = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (currentBase != "Home" && hasLeftHome == false)
        {
            hasLeftHome = true;
        }

        if (currentBase == "Home" && hasLeftHome)
        {
            SceneManager.LoadScene("EndingBasebrawlTestingZone");
        }
    }
}
