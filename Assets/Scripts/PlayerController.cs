using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;  // Adjust this to control the player's movement speed

    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Update the direction parameter based on the player's movement
        if (movement.magnitude > 0)
        {
            if (movement.y > 0) // Moving up
            {
                animator.SetInteger("Direction", 2);
            }
            else if (movement.y < 0) // Moving down
            {
                animator.SetInteger("Direction", 0);
            }
            else if (movement.x < 0) // Moving left
            {
                animator.SetInteger("Direction", 3);
            }
            else if (movement.x > 0) // Moving right
            {
                animator.SetInteger("Direction", 1);
            }
        }
        else // No movement input
        {
            animator.SetInteger("Direction", -1);
        }
    }



    private void FixedUpdate()
    {
        // Calculate the movement vector
        Vector2 movementVector = movement * moveSpeed * Time.fixedDeltaTime;

        // Apply the movement to the player's Rigidbody
        rb.MovePosition(rb.position + movementVector);
    }
}
