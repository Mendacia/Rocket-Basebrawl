using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.InputSystem;

public class playerControls : MonoBehaviour
{
    [Header("Player Speed")]
    [System.NonSerialized] public float speed = 30;
    [SerializeField] private float baseSpeed = 30;
    public float topSpeed = 45;
    //Lock player movement at the start
    [Header("Player State")]
    public int playerState = 2;

    [Header("Please put the animator from CheetahIdle here")]
    [SerializeField] private Animator playerAnimator = null;
    //[SerializeField] private Animator hitariAnimator = null;

    
    //Input System Movements
    private PlayerInputActions inputActions;
    private Vector2 movementInput;
    private Vector3 inputDirection, moveVector;
    private Quaternion currentRotation;

    [Header("Rigidbody Variables")]
    [SerializeField] private Rigidbody rb = null;
    [SerializeField] private float magnitudeStopFloat = 10;

    [Header("Jump Variables")]
    [SerializeField] private float jumpForce = 50;
    [SerializeField] private float hangTime = 0.2f;

    [Header("Running Effects")]
    [SerializeField] private GameObject runningEffects = null;

    [Header("Tick this if the player needs to be locked in place on Start")]
    public bool isFrozen = false;

    private bool isGrounded = true;

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
        if (rb.velocity.magnitude <= magnitudeStopFloat)
        {
            rb.velocity = Vector3.zero;
            speed = baseSpeed;
            playerAnimator.SetBool("heMoving", false);
            if (runningEffects.activeSelf == true)
            {
                runningEffects.SetActive(false);
            }
        }
        else
        {
            playerAnimator.SetBool("heMoving", true);
            if (runningEffects.activeSelf == false)
            {
                runningEffects.SetActive(true);
            }
        }
        if(rb.velocity.magnitude >= magnitudeStopFloat && speed <= topSpeed)
        {
            speed = speed + 0.1f;
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

    public void Jump(CallbackContext context)
    {
        if(context.performed && isGrounded && playerState == 2)
        {
            isGrounded = false;
            StartCoroutine(Jump());
        }
    }

    IEnumerator Jump()
    {
        /*Vector3 initialPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 newPos = new Vector3(transform.position.x, transform.position.y + jumpForce, transform.position.z);
        Vector3 jump = Vector3.Lerp(initialPos, newPos, 1.0f);*/
        Vector3 jump = new Vector3(0.0f, 2.0f, 0.0f);
        //rb.AddForce(jump * jumpForce, ForceMode.Force);
        rb.useGravity = false;
        for (int t = 0; t < 20; t++)
        {
            rb.AddRelativeForce(jump * jumpForce, ForceMode.Force);
            yield return new WaitForSeconds(0.005f);
        }
            //transform.position = jump;
            
        yield return new WaitForSeconds(hangTime);
        rb.useGravity = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 14 /*Ground*/)
        {
            isGrounded = true;
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
