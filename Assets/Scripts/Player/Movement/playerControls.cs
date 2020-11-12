using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.InputSystem;

public class playerControls : MonoBehaviour
{
    [Header("Player Speed")]
    [SerializeField] private float speed = 30;
    [SerializeField] private float baseSpeed = 30;
    [SerializeField] private float topSpeed = 45;
    //Lock player movement at the start
    [Header("Player State")]
    public int playerState = 2;

    [Header("Please put the animator from CheetahIdle here")]
    [SerializeField] private Animator playerAnimator;

    
    //Input System Movements
    private PlayerInputActions inputActions;
    private Vector2 movementInput;
    private Vector3 inputDirection, moveVector;
    private Quaternion currentRotation;

    [Header("Rigidbody Variables")]
    [SerializeField] private Rigidbody rb = null;
    [SerializeField] private float magnitudeStopFloat = 10;
    [Header("Standalone Player Gravity")]
    [SerializeField] private float gravityMultiplier = 100;
    [Header("Tick this if the player needs to be locked in place on Start")]
    public bool isFrozen = false;

    private void Awake()
    {
        //On awake grabs the InputSystem and assigns the variable movementInput to the Move field
        inputActions = new PlayerInputActions();
        inputActions.Player.Move.performed += context => movementInput = context.ReadValue<Vector2>();
    }

    private void Start()
    {
        speed = baseSpeed;
        //Gamepad.current.SetMotorSpeeds(0.25f, 0.75f);
        if (isFrozen == true)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    private void Update()
    {
        if(rb.velocity.magnitude <= magnitudeStopFloat)
        {
            rb.velocity = Vector3.zero;
            speed = baseSpeed;
            playerAnimator.SetBool("heMoving", false);
        }
        else
        {
            playerAnimator.SetBool("heMoving", true);
        }
        if(rb.velocity.magnitude >= magnitudeStopFloat && speed <= topSpeed)
        {
            speed = speed + 0.05f;
        }
    }

    private void FixedUpdate()
    {
        switch (playerState)
        {
            //No input, aiming only
            case 1:
                
                break;
            //Full movement
            case 2:
                float h = movementInput.x;
                float v = movementInput.y;

                //Enhances the Gravity for the player alone
                rb.AddForce(Physics.gravity * gravityMultiplier, ForceMode.Acceleration);

                Vector3 targetInput = new Vector3(h, 0, v);

                inputDirection = Vector3.Lerp(inputDirection, targetInput, Time.deltaTime * 10f);

                Vector3 camForward = Camera.main.transform.forward;
                Vector3 camRight = Camera.main.transform.right;
                camForward.y = 0f;
                camRight.y = 0f;

                Vector3 desiredDirection = camForward * inputDirection.z + camRight * inputDirection.x;

                Move(desiredDirection);
                Turn(desiredDirection);

                break;
        }
    }

    void Move(Vector3 desiredDirection)
    {
        moveVector.Set(desiredDirection.x, 0, desiredDirection.z);
        moveVector += moveVector * speed / 5 * Time.deltaTime;
        rb.velocity = moveVector * speed * 50 * Time.deltaTime;
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
