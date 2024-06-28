using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float groundDrag;

    [Header("Keybinds")]
    KeyCode sprintKey = KeyCode.LeftShift;
    KeyCode crouchKey = KeyCode.C;


    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;


    [Header("Crouching")]
    public float crouchSpeed;
    public float couchYScale;
    public float startYScale;
    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;
    [SerializeField] Rigidbody rigidBody;
    void Start()
    {
        rigidBody.freezeRotation = true;

        startYScale = transform.localScale.y;
    }
    public MovementState state;
    public enum MovementState
    {
        walking, sprinting, crouching
    }

    void StateHandler()
    {
        if (grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }
        else if (grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }
        if (Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
        }

    }


    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();
        SpeedControl();
        StateHandler();

        if (grounded)
        {
            rigidBody.drag = groundDrag;
        }
        else
        {
            rigidBody.drag = 0;
        }
    }
    void FixedUpdate()
    {
        MovePlayer();
    }
    void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, couchYScale, transform.localScale.z);
            rigidBody.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }
        if (Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }
    }
    void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rigidBody.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }
    void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);
        if (flatVelocity.magnitude > moveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
            rigidBody.velocity = new Vector3(limitedVelocity.x, rigidBody.velocity.y, limitedVelocity.z);
        }

    }
}
