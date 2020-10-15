using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class currentBaseHolder : MonoBehaviour
{
    [System.NonSerialized]public string currentBase = "Home";
    private bool hasLeftHome = false;

    private void Update()
    {
        if (currentBase != "Home" && hasLeftHome == false)
        {
            hasLeftHome = true;
        }

        if (currentBase == "Home" && hasLeftHome)
        {
            hasLeftHome = true;
        }
    }
}
