using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class POVCamControl : MonoBehaviour
{
    CinemachinePOV POVCam;
    public InputActionReference actions;
    private PlayerInputActions inputActions;
    Vector2 camInput;
    [Range(0, 100)]
    public float sensitivity = 50;

    [SerializeField] private bool shouldInheritAtStart = false;

    // Start is called before the first frame update

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.Look.performed += context => GetValue(context);
    }

    void Start()
    {
        var vcam = GetComponent<CinemachineVirtualCamera>();
        if (vcam != null)
        {
            POVCam = vcam.GetCinemachineComponent<CinemachinePOV>();
        }
        if (shouldInheritAtStart)
        {
            vcam.m_Transitions.m_InheritPosition = true;
        }
        StartCoroutine(InheritAfterTime());
    }

    void FixedUpdate()
    {
        if (WorldStateMachine.GetCurrentState() != WorldState.FROZEN)
        {
            POVCam.m_HorizontalAxis.Value += camInput.x * (sensitivity / 3) * Time.deltaTime;
            POVCam.m_VerticalAxis.Value -= camInput.y * (sensitivity / 3) / 2.5f * Time.deltaTime;
            camInput = Vector2.zero;
        }
    }

    IEnumerator InheritAfterTime()
    {
        var vcam = GetComponent<CinemachineVirtualCamera>();
        yield return new WaitForSeconds(5);
        vcam.m_Transitions.m_InheritPosition = true;
    }

    private void GetValue(CallbackContext context)
    {
        var value = context.ReadValue<Vector2>();
        if (value != Vector2.zero)
        {
            camInput.x = Vector2.Lerp(camInput, value, Mathf.SmoothStep(0f, 1f, 60 * Time.deltaTime)).x;
            camInput.y = Vector2.Lerp(camInput, value, Mathf.SmoothStep(0f, 1f, 60 * Time.deltaTime)).y;
        }
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }
}
