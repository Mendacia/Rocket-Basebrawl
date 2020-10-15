using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.InputSystem;

public class ActivateOnKeypress : MonoBehaviour
{
    public int PriorityBoostAmount = 10;
    public GameObject Reticle;

    Cinemachine.CinemachineVirtualCameraBase vcam;
    bool boosted = false;

    void Start()
    {
        vcam = GetComponent<Cinemachine.CinemachineVirtualCameraBase>();
    }

    public void aimOn(CallbackContext context)
    {
        if (context.performed)
        {
            if (!boosted)
            {
                vcam.Priority += PriorityBoostAmount;
                boosted = true;
            }
        }
        else if (vcam != null)
        {
            if (boosted)
            {
                vcam.Priority -= PriorityBoostAmount;
                boosted = false;
            }
        }
        if (Reticle != null)
            Reticle.SetActive(boosted);
    }
}
