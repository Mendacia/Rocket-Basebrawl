using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.InputSystem;

public class TimeSlow : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool timeSlowed;
    public GameObject aimingBox;

    private PlayerInputActions inputActions;
    Vector2 lookInput;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.Look.performed += context => lookInput = context.ReadValue<Vector2>();
    }

    void Start()
    {
        Time.timeScale = 1;
    }

    private void Update()
    {
        RotatePlane();
    }

    public void TimeScale(CallbackContext context)
    {
        if (context.performed && !PauseMenu.isPaused)
        {
            Time.timeScale = 0.5f;
            timeSlowed = true;
            aimingBox.SetActive(true);
        }

        else if (context.canceled && !PauseMenu.isPaused)
        {
            Time.timeScale = 1;
            timeSlowed = false;
            aimingBox.SetActive(false);
        }    

        else
        {
            timeSlowed = false;
            aimingBox.SetActive(false);
        }
    }

    void RotatePlane()
    {
        if (!PauseMenu.isPaused)
        {
            aimingBox.transform.eulerAngles += new Vector3(0, 0, -lookInput.x);
        }
    }

    //Enables NewInputSystem Inputs
    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }
}
