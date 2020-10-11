using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.InputSystem;

public class TimeSlow : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool timeSlowed;
    public GameObject ballPlane;

    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    public void TimeScale(CallbackContext context)
    {
        if (context.performed)
        {
            Time.timeScale = 0.5f;
            timeSlowed = true;
            Camera.main.fieldOfView = 30;
            ballPlane.SetActive(true);
            Debug.Log(Time.timeScale);
        }

        else if (context.canceled)
        {
            Time.timeScale = 1;
        }    

        else
        {
            timeSlowed = false;
            Camera.main.fieldOfView = 60;
            ballPlane.SetActive(false);
        }
    }
}
