using UnityEngine;

    //Player Movement

public class PlayerController2D : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float jumpForce = 15f;

    [Header("Variable Jump")]
    public float jumpCutMultiplier = 0.5f;
    public float fallGravityMultiplier = 2f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.15f;
    public LayerMask groundLayer;

    [Header("Visual")]
    public Transform visualRoot;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float moveInput;

    [HideInInspector] public bool canControl = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (canControl)
        {
            moveInput = Input.GetAxisRaw("Horizontal");

            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }

            if (Input.GetKeyUp(KeyCode.Space) && rb.linearVelocity.y > 0f)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * jumpCutMultiplier);
            }

            if (moveInput > 0.01f)
                FaceRight();
            else if (moveInput < -0.01f)
                FaceLeft();
        }
        else
        {
            moveInput = 0f;
        }
    }

    void FixedUpdate()
    {
        if (canControl)
        {
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        }

        if (rb.linearVelocity.y < 0f)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallGravityMultiplier - 1f) * Time.fixedDeltaTime;
        }
    }

    public void LockControl()
    {
        canControl = false;
        moveInput = 0f;
        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
    }

    public void UnlockControl()
    {
        canControl = true;
    }

    public void StopMovement()
    {
        moveInput = 0f;
        rb.linearVelocity = Vector2.zero;
    }

    public Rigidbody2D GetRigidbody()
    {
        return rb;
    }

    public void FaceRight()
    {
        if (visualRoot != null)
            visualRoot.localScale = new Vector3(1f, 1f, 1f);
    }

    public void FaceLeft()
    {
        if (visualRoot != null)
            visualRoot.localScale = new Vector3(-1f, 1f, 1f);
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}