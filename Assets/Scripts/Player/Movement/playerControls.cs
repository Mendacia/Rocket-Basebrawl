using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.InputSystem;

public class playerControls : MonoBehaviour
{
    [Header("Speed Controls")]
    [SerializeField] private float baseSpeed = 30;
    [SerializeField] private float acceleration = 30;
    [SerializeField] private float topSpeed = 45;
    [SerializeField] private float stoppingSpeed = 10;
    [SerializeField] private float dashSpeed = 100;

    //Input System Movements
    private Vector2 movementInput;
    private Vector3 inputDirection;
    private Quaternion currentRotation;
    private float currentSpeed = 30;

    private Animator playerAnimator = null;
    private Rigidbody rb = null;

    [Header("Particle Variables whatever i don't care")]
    [SerializeField] private List<ParticleSystem> playerFeetParticlesForDashing;
    [SerializeField] private float MinimumSize, lerpDistance, MaximumSize;

    private void Awake()
    {
        playerAnimator = GetComponentInChildren<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        foreach(ParticleSystem p in playerFeetParticlesForDashing)
            {
                p.startLifetime = Mathf.Lerp(p.startLifetime, MinimumSize, lerpDistance * Time.deltaTime);
            }
    }

    private void Update()
    {
        if (currentSpeed <= topSpeed + 5)
        {
            foreach (ParticleSystem p in playerFeetParticlesForDashing)
            {
                p.startLifetime = Mathf.Lerp(p.startLifetime, MinimumSize, lerpDistance * Time.deltaTime);
            }
        }
    }

    private void FixedUpdate()
    {
        switch (WorldStateMachine.GetCurrentState())
        {
            //No input, aiming only
            case WorldState.BATTING:
                
                break;
            //Full movement
            case WorldState.RUNNING:
                float h = movementInput.x;
                float v = movementInput.y;

                Vector3 targetInput = new Vector3(h, 0, v);

                inputDirection = Vector3.Lerp(inputDirection, targetInput, Time.deltaTime * 10f);

                Vector3 camForward = Camera.main.transform.forward;
                Vector3 camRight = Camera.main.transform.right;
                camForward.y = 0f;
                camRight.y = 0f;

                Vector3 desiredDirection = camForward * inputDirection.z + camRight * inputDirection.x;

                if (desiredDirection.magnitude > 1)
                {
                    desiredDirection.Normalize();
                }

                Move(desiredDirection);
                Turn(desiredDirection);
                
                break;
        }

        if (rb.velocity.magnitude <= stoppingSpeed)
        {
            rb.velocity = Vector3.zero;
            currentSpeed = baseSpeed;
            playerAnimator.SetBool("heMoving", false);
        }
        else
        {
            playerAnimator.SetBool("heMoving", true);
        }
        if (currentSpeed <= topSpeed)
        {
            currentSpeed += acceleration * Time.deltaTime;
        }
    }

    public void SetMoveDirection(CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    void Move(Vector3 desiredDirection)
    {
        var moveVector = new Vector3(desiredDirection.x, 0, desiredDirection.z);
        rb.velocity = moveVector * currentSpeed;
    }

    void Turn(Vector3 desiredDirection)
    {
        if ((desiredDirection.x > 0.1 || desiredDirection.x < -0.1) || desiredDirection.z > 0.1 || desiredDirection.z < -0.1)
        {
            currentRotation = Quaternion.LookRotation(desiredDirection);
            transform.rotation = currentRotation;
        }
        else
            transform.rotation = currentRotation;
    }

    public void Dash(bool on)
    {
        if (on)
        {
            foreach (ParticleSystem p in playerFeetParticlesForDashing)
            {
                p.startLifetime = MaximumSize;
            }
            currentSpeed = dashSpeed;
            playerAnimator.SetTrigger("heDashing");
        }
        else
        {
            currentSpeed = topSpeed;
        }
    }
}
