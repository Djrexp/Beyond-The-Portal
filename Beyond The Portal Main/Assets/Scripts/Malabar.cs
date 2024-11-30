using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Malabar : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 3.0f;
    public float sprintMultiplier = 1.5f;
    public float jumpForce = 5.0f;
    public float wallJumpForce = 7.0f;
    public Transform groundCheck;
    public Transform wallCheck;
    public LayerMask groundLayer;
    public float checkRadius = 0.2f;

    private Rigidbody2D rb;
    private bool isFacingRight = true;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isWallSliding;
    private bool isGrabbing;

    [Header("Grabbing")]
    public Transform grabPoint;
    public float grabRadius = 0.5f;
    public LayerMask grabbableLayer;
    private GameObject grabbedObject;

    [Header("Throwing")]
    public float throwForce = 10f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckSurroundings();
    }

    // Check surroundings for jumping off grabbable objects
    private void CheckSurroundings()
    {
        // Check if the player is grounded on the ground layer
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        
        // Check if the player is touching a grabbable object
        isGrounded = isGrounded || Physics2D.OverlapCircle(groundCheck.position, checkRadius, grabbableLayer);
        
        // Check if the player is touching a static object for jumping
        isGrounded = isGrounded || Physics2D.OverlapCircle(groundCheck.position, checkRadius, LayerMask.GetMask("Static"));

        // Check if the player is touching a wall
        isTouchingWall = Physics2D.OverlapCircle(wallCheck.position, checkRadius, groundLayer);

        // Check for wall sliding
        if (isTouchingWall && !isGrounded && rb.velocity.y < 0)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }
    }

    // Movement
    public void Move(Vector3 movement, bool isSprinting)
    {
        float currentSpeed = isSprinting ? speed * sprintMultiplier : speed;
        rb.velocity = new Vector2(movement.x * currentSpeed, rb.velocity.y);

        // Flip the character based on movement direction
        if (movement.x > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (movement.x < 0 && isFacingRight)
        {
            Flip();
        }
    }

    // Jump function allowing jumping off grabbable objects
    public void Jump()
    {
        if (isGrounded) // Jump if grounded, including on grabbable objects
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        else if (isWallSliding)
        {
            float wallJumpDirection = isFacingRight ? -1 : 1;
            rb.velocity = new Vector2(wallJumpForce * wallJumpDirection, jumpForce);
            Flip();
        }
    }

    // Grabbing items
    public void HandleGrab()
    {
        if (isGrabbing)
        {
            ReleaseObject();
        }
        else
        {
            GrabObject();
        }
    }

    private void GrabObject()
    {
        // Check for nearby grabbable objects
        Collider2D[] objectsToGrab = Physics2D.OverlapCircleAll(transform.position, grabRadius, grabbableLayer);

        if (objectsToGrab.Length > 0)
        {
            grabbedObject = objectsToGrab[0].gameObject;

            // Get the colliders for both the player and the grabbed object
            Collider2D grabbedObjectCollider = grabbedObject.GetComponent<Collider2D>();
            Collider2D playerCollider = GetComponent<Collider2D>();

            // Ignore collision between the player and the grabbed object
            Physics2D.IgnoreCollision(grabbedObjectCollider, playerCollider, true);

            // Optionally calculate a reasonable offset based on the object size (or a fixed value for better control)
            float objectWidth = grabbedObject.GetComponent<Collider2D>().bounds.size.x;
            float adjustedOffset = Mathf.Clamp(objectWidth * 0.5f, 0.5f, 1.5f);

            // Attach the object to the grab point
            grabbedObject.transform.SetParent(grabPoint);
            grabbedObject.transform.localPosition = new Vector3(0f, 0f, 0f); // Adjust this for a closer grab point

            grabbedObject.GetComponent<Rigidbody2D>().isKinematic = true; // Disable physics while holding
            isGrabbing = true;
        }
    }

    private void ReleaseObject()
    {
        if (grabbedObject != null)
        {
            // Restore collision between the player and the grabbed object
            Collider2D grabbedObjectCollider = grabbedObject.GetComponent<Collider2D>();
            Collider2D playerCollider = GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(grabbedObjectCollider, playerCollider, false);

            grabbedObject.transform.SetParent(null); // Detach from player
            grabbedObject.GetComponent<Rigidbody2D>().isKinematic = false; // Re-enable physics
            grabbedObject = null;
            isGrabbing = false;
        }
    }

    // Throwing items
    public void ThrowObject()
    {
        if (isGrabbing && grabbedObject != null)
        {
            // Detach the object from the player
            grabbedObject.transform.SetParent(null);

            // Re-enable physics
            Rigidbody2D objectRb = grabbedObject.GetComponent<Rigidbody2D>();
            objectRb.isKinematic = false;

            // Restore collision between the player and the grabbed object
            Collider2D grabbedObjectCollider = grabbedObject.GetComponent<Collider2D>();
            Collider2D playerCollider = GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(grabbedObjectCollider, playerCollider, false);

            // Apply force to throw
            Vector2 throwDirection = isFacingRight ? Vector2.right : Vector2.left;
            objectRb.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);

            grabbedObject = null;
            isGrabbing = false;
        }
    }

    // Flip character direction
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    // Visualize grab and ground checks in the editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, grabRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
        Gizmos.DrawWireSphere(wallCheck.position, checkRadius);
    }
}
