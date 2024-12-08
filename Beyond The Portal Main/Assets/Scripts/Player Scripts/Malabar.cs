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

    [Header("Attacking")]
    [SerializeField] float attackDamage = 34f;
    [SerializeField] LayerMask enemyLayer;
    public float attackPushDistance = 0.5f;
    private PolygonCollider2D attackCollider;
    private SpriteRenderer spriteRenderer;
    private bool isRed = false;
  
    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
        attackCollider = GetComponentInChildren<PolygonCollider2D>();

        if (attackCollider == null){
        }else{
            attackCollider.enabled = false;
        }
    }

    private void Update(){
        CheckSurroundings();
    }

    private void Start(){
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Checks surroundings for jumping off grabbable objects
    private void CheckSurroundings(){
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        isGrounded = isGrounded || Physics2D.OverlapCircle(groundCheck.position, checkRadius, grabbableLayer);
        isGrounded = isGrounded || Physics2D.OverlapCircle(groundCheck.position, checkRadius, LayerMask.GetMask("Static"));
        isTouchingWall = Physics2D.OverlapCircle(wallCheck.position, checkRadius, groundLayer);

        if (isTouchingWall && !isGrounded && rb.velocity.y < 0){
            isWallSliding = true;
        }else{
            isWallSliding = false;
        }
    }

    // Movement 
    public void Move(Vector3 movement, bool isSprinting)
    {
        float currentSpeed = isSprinting ? speed * sprintMultiplier : speed;
        rb.velocity = new Vector2(movement.x * currentSpeed, rb.velocity.y);

        if (movement.x > 0 && !isFacingRight){
            Flip();
        }else if (movement.x < 0 && isFacingRight){
            Flip();
        }
    }

    // Handles jumping including off of grabbable objects
    public void Jump(){
        float adjustedJumpForce = jumpForce;

        GravitySwitch gravitySwitch = FindObjectOfType<GravitySwitch>();
        if (gravitySwitch != null){
            adjustedJumpForce = gravitySwitch.isGravityPositive ? jumpForce : -jumpForce;
        }

        if (isGrounded){
            rb.velocity = new Vector2(rb.velocity.x, adjustedJumpForce);
        }else if (isWallSliding){
            float wallJumpDirection = isFacingRight ? -1 : 1;
            rb.velocity = new Vector2(wallJumpForce * wallJumpDirection, adjustedJumpForce);
            Flip();
        }
    }

    // Grabbing items
    public void HandleGrab(){
        if (isGrabbing){
            ReleaseObject();
        }else{
            GrabObject();
        }
    }

    // Deems if an object is grabbable
    private void GrabObject(){
        Collider2D[] objectsToGrab = Physics2D.OverlapCircleAll(transform.position, grabRadius, grabbableLayer);

        if (objectsToGrab.Length > 0){
            grabbedObject = objectsToGrab[0].gameObject;

            Collider2D grabbedObjectCollider = grabbedObject.GetComponent<Collider2D>();
            Collider2D playerCollider = GetComponent<Collider2D>();

            Physics2D.IgnoreCollision(grabbedObjectCollider, playerCollider, true);

            float objectWidth = grabbedObject.GetComponent<Collider2D>().bounds.size.x;
            float adjustedOffset = Mathf.Clamp(objectWidth * 0.5f, 0.5f, 1.5f);

            grabbedObject.transform.SetParent(grabPoint);
            grabbedObject.transform.localPosition = new Vector3(0f, 0f, 0f);

            grabbedObject.GetComponent<Rigidbody2D>().isKinematic = true;
            isGrabbing = true;
        }
    }

    // releasing an object being held by the player
    public void ReleaseObject(){
        if (grabbedObject != null){
            Collider2D grabbedObjectCollider = grabbedObject.GetComponent<Collider2D>();
            Collider2D playerCollider = GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(grabbedObjectCollider, playerCollider, false);

            grabbedObject.transform.SetParent(null);
            grabbedObject.GetComponent<Rigidbody2D>().isKinematic = false;
            grabbedObject = null;
            isGrabbing = false;
        }
    }

    // Throwing items
    public void ThrowObject(){
        if (isGrabbing && grabbedObject != null){
            grabbedObject.transform.SetParent(null);

            Rigidbody2D objectRb = grabbedObject.GetComponent<Rigidbody2D>();
            objectRb.isKinematic = false;

            Collider2D grabbedObjectCollider = grabbedObject.GetComponent<Collider2D>();
            Collider2D playerCollider = GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(grabbedObjectCollider, playerCollider, false);

            Vector2 throwDirection = isFacingRight ? Vector2.right : Vector2.left;
            objectRb.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);

            grabbedObject = null;
            isGrabbing = false;
        }
    }

    // Handles attacking with seperate collider
    public void Attack(){
        if (attackCollider != null){
            attackCollider.enabled = true;

            Collider2D[] results = new Collider2D[10];
            ContactFilter2D filter = new ContactFilter2D{
                layerMask = enemyLayer,
                useLayerMask = true
            };

            int hitCount = attackCollider.OverlapCollider(filter, results);

            for (int i = 0; i < hitCount; i++){
                Collider2D enemy = results[i];
                if (enemy.CompareTag("Enemy")){
                    EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                    if (enemyHealth != null){
                        enemyHealth.TakeDamage(attackDamage);

                        Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();
                        if (enemyRb != null){
                            Vector2 pushDirection = (enemy.transform.position - transform.position).normalized;
                            enemyRb.AddForce(pushDirection * attackPushDistance, ForceMode2D.Impulse);
                        }
                    }
                }
            }

            attackCollider.enabled = false;
        }
    }

    // Flip player direction
    private void Flip(){
        isFacingRight = !isFacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    // Handles when to execute the damage effect on the player
    public void SetPlayerRed(float duration){
        if (!isRed){
            isRed = true;
            StartCoroutine(ChangeColor(duration));
        }
    }

    // Handles color of damage effect on player
    private IEnumerator ChangeColor(float duration){
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(duration);
        spriteRenderer.color = originalColor;
        isRed = false;
    }
}
