using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire2"))
        {
            Time.timeScale = 0.4f;
            Camera.main.fieldOfView = 30;
        }
        else
        {
            Time.timeScale = 1;
            Camera.main.fieldOfView = 60;
        }
    }
}
