using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5.0f;     // Speed of the player
    public float jumpHeight = 2.0f;    // Height of the jump
    public float gravity = -9.81f;      // Gravity value

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded; // Check if player is grounded

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0; // Reset vertical velocity when grounded
        }

        // Get input for movement
        float moveX = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrow
        float moveZ = Input.GetAxis("Vertical");   // W/S or Up/Down Arrow

        // Create movement vector
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Move the player
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity); // Calculate jump velocity
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;

        // Move the player based on the vertical velocity
        controller.Move(velocity * Time.deltaTime);
    }
}
