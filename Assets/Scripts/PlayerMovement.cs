using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] CharacterController controller;
    [SerializeField] PlayerInput playerInput;
    public float sensitivityX;
    public float sensitivityY;
    //[SerializeField] Animator animator;
    private Vector3 playerVelocity;
    public bool groundedPlayer;

    public float playerSpeed = 2.0f;
    private float gravityValue = -9.81f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Move();
        Look();
    }

    private void Move()
    {

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 movementInput = playerInput.actions["Movement"].ReadValue<Vector2>();
        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
    }
    void Look()
    {
        float mouseX = Input.GetAxisRaw("MouseX") * Time.deltaTime * sensitivityX;
    }

}

