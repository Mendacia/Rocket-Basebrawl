using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class runningPhaseMovement : MonoBehaviour
{
    [Header("Player Speed")]
    [SerializeField] private float speed = 1;
    [Header ("Plug the root camera object in here")]
    [SerializeField] private Transform cameraRotationReferenceY = null;
    private Rigidbody rb;

    //Animation
    private Animator anim;
    public GameObject animReference;

    //Inputs that I need to carry from Update to FixedUpdate
    private float movementX = 0;
    private float movementZ = 0;

    //Lock player movement at the start
    private int playerState = 1;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = animReference.gameObject.GetComponent<Animator>();
    }
    private void Update()
    {
        //Taking Inputs
        movementX = Input.GetAxis("Horizontal") * speed;
        movementZ = Input.GetAxis("Vertical") * speed;
        
        

        //Character unlock
        if (Input.GetButtonDown("Fire1")){
            playerState = 2;
        }
    }

    private void FixedUpdate()
    {
        switch (playerState)
        {
            case 1:

                break;

            case 2:
                //Temporary Animations
                if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
                {
                    anim.Play("Running");
                }
                else if (BattingOnClick.tempAnimCheck == false)
                {
                    anim.Play("Batting Idle");
                }

                transform.rotation = Quaternion.LookRotation(cameraRotationReferenceY.forward, Vector3.up);
                rb.velocity = transform.right * movementX + transform.forward * movementZ;
                break;
        }
    }
}
