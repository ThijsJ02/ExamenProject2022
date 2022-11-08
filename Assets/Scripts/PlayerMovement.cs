using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    [Header("Player Attributes")]
    public float jumpHeight;
    public float speed;

    [Header("Raycast Attributes & Components")]
    public Transform leftRaycastOriginPoint;
    public Transform rightRaycastOriginPoint;
    public float rayLenght = 0.5f;

    [Header("Ground Layers")]
    public LayerMask groundLayer;

    private bool jump = false;
    private float horizontal;
    private bool isGrounded = true;
    private bool isFacingRight = true;

    public AudioSource source;
    public AudioClip footstepSound;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleInput();
        Flip();
        HandleAnimations();
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void Jump()
    {
        if (jump && IsGrounded())
        {
            float jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * rb.gravityScale));
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

            if (IsGrounded())
            {
                jump = false;
                
            }
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHitLeft = Physics2D.Raycast(leftRaycastOriginPoint.position, Vector2.down, rayLenght, groundLayer);
        RaycastHit2D raycastHitRight = Physics2D.Raycast(rightRaycastOriginPoint.position, Vector2.down, rayLenght, groundLayer);
        Color rayColor;

        if(raycastHitLeft.collider != null || raycastHitRight.collider != null)
        {
            rayColor = Color.green;
            isGrounded = true;
        }
        else if(raycastHitLeft.collider == null && raycastHitRight.collider == null)
        {
            rayColor = Color.red;
            isGrounded = false;
        }
        Debug.DrawRay(leftRaycastOriginPoint.position, Vector2.down * rayLenght);
        Debug.DrawRay(rightRaycastOriginPoint.position, Vector2.down * rayLenght);
        return isGrounded;
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;

            transform.Rotate(0f, 180f, 0f);       
        }
    }

    private void HandleInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        animator.SetInteger("Run", (int)Mathf.Abs(horizontal));


        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            animator.SetBool("Jump", true);
            jump = true;
        }
    }

    private void HandleAnimations()
    {
        if (IsGrounded())
        {
            animator.SetBool("Jump", false);
        }
        else
        {
            if (rb.velocity.y > 0.1f && !IsGrounded())
            {
                animator.SetBool("Jump", true);
            }
        }

        if(rb.velocity.y < -2.5f && !IsGrounded())
        {
            animator.SetBool("Jump", true);
        }
    }

    public void PlayFootstepSound()
    {
        source.PlayOneShot(footstepSound);
    }
}
