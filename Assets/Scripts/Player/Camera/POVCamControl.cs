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

    [SerializeField] private bool useX = true;
    [SerializeField] private playerControls playerStateReference = null;

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
        vcam.m_Transitions.m_InheritPosition = false;
    }

    void FixedUpdate()
    {
        if (playerStateReference.playerState > 0)
        {
            if (useX == true)
            {
                POVCam.m_HorizontalAxis.Value += camInput.x * (sensitivity / 2) * Time.deltaTime;
            }

            POVCam.m_VerticalAxis.Value -= camInput.y * (sensitivity / 2) / 2.5f * Time.deltaTime;
            camInput = Vector2.zero;
        }
        if(playerStateReference.playerState == 2)
        {
            StartCoroutine(WaitToInheritPosition());
        }
    }

    IEnumerator WaitToInheritPosition()
    {
        yield return new WaitForSeconds(4f);
        var vcam = GetComponent<CinemachineVirtualCamera>();
        vcam.m_Transitions.m_InheritPosition = true;
    }

    private void GetValue(CallbackContext context)
    {
        var value = context.ReadValue<Vector2>();
        if (value != Vector2.zero)
        {
            camInput.x = value.x;
            camInput.y = value.y;
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
