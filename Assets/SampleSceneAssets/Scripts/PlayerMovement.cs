#if ENABLE_INPUT_SYSTEM 
using UnityEngine.InputSystem;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public float speed = 6.0f;
    public float minSpeed = 6.0f; // Added min speed
    public float maxSpeed = 12.0f; // Added max speed
    public float speedIncreaseRate =2f; // Added speed increase rate
    public float speedDecreaseRate = 0.3f; // Added speed decrease rate
    public float shiftSpeedIncreaseRate = 2f; // Added shift speed increase rate
    public float turnSmoothTime = 0.1f;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float jumpHeight = 2.0f; // Added jump height
    public Animator animator; // Added animator

    float turnSmoothVelocity;
    Vector3 velocity;
    bool isGrounded;

    private void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // If grounded and falling, reset the downward velocity
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small downward force for better grounding
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            // Smoothly rotate the player to face the direction of movement
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // Increase speed based on how much time the player is moving
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                speed = Mathf.Min(speed + shiftSpeedIncreaseRate * Time.deltaTime, maxSpeed);
            }
            else
            {
                speed = Mathf.Min(speed + speedIncreaseRate * Time.deltaTime, maxSpeed);
            }

            // Move the player in the direction they are facing
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);

            // Lerp the animator float PlayerWalkVelocity from 0 to 12 based on the current speed
            animator.SetFloat("PlayerWalkVelocity", Mathf.Lerp(0, 12, speed / maxSpeed));
        }
        else
        {
            // Decrease speed to 0 when player stops pressing the axis
            speed = Mathf.Max(speed - speedDecreaseRate * Time.deltaTime, minSpeed);
            

            // Update the animator float PlayerWalkVelocity to 0 when player stops pressing the axis
            animator.SetFloat("PlayerWalkVelocity", 0);
        }

        // Jump functionality
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
