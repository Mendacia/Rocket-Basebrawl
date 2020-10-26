using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class POVCamControl : MonoBehaviour
{
    CinemachinePOV POVCam;
    public InputActionReference actions;
    private PlayerInputActions inputActions;
    Vector2 camInput;
    public float sensitivity;

    [SerializeField] private bool useX = true;

    // Start is called before the first frame update

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.Look.performed += context => camInput = context.ReadValue<Vector2>();
    }

    void Start()
    {
        var vcam = GetComponent<CinemachineVirtualCamera>();
        if (vcam != null)
        {
            POVCam = vcam.GetCinemachineComponent<CinemachinePOV>();
        }
    }

    void Update()
    {
        if (useX == true)
        {
            POVCam.m_HorizontalAxis.Value += camInput.x * sensitivity * Time.deltaTime;
        }
        
        POVCam.m_VerticalAxis.Value -= camInput.y * sensitivity / 3 * Time.deltaTime;
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
