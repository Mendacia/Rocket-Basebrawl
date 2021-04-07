using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelFollowPlayer : MonoBehaviour
{
    [Header("This should be the model")]
    [SerializeField] private Transform playerModel = null;
    [Header("This should be the player")]
    [SerializeField] private Transform rotationReference = null;

    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(h, 0f, v).normalized;

        transform.position = playerModel.position;

        if(WorldStateMachine.GetCurrentState() == WorldState.RUNNING)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + (Camera.main.transform.eulerAngles.y + 90);
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, rotationReference.eulerAngles.y + 90, 0);
        }
    }
}