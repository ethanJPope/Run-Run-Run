using UnityEngine;
using System.Collections;
using Unity.Cinemachine;
using System.Data.Common;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private int maxJumps = 1;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Transform followPoint;
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f;
    [SerializeField] private TrailRenderer dashTrail;

    private bool canDash = true;
    private bool isDashing = false;
    private float dashTimer = 0f;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded = true;
    private int jumpCount = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        LookDown();
        UpdateAnimations();
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void HandleMovement()
    {
        if (isDashing)
        {
            return;
        }
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (moveInput > 0 && isGrounded)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveInput < 0 && isGrounded)
        {
            spriteRenderer.flipX = true;
        }

        if (PlayerManager.instance != null && PlayerManager.instance.data.ownedItems.Contains("Dash"))
        {
            EnableDash();
        }
        
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float origanalGravity = rb.gravityScale;
        rb.gravityScale = 0;
        float direction = 0;

        dashTrail.emitting = true;

        if (spriteRenderer.flipX)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }
        rb.linearVelocity = new Vector2(direction * dashSpeed, 0);
        yield return new WaitForSeconds(dashDuration);

        dashTrail.emitting = false;
        rb.gravityScale = origanalGravity;
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private void EnableDash()
    {
        canDash = true;
    }

    private void HandleJump()
    {
        bool jumpPressed = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow);
        bool jumpReleased = Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.UpArrow);

        if (jumpPressed && jumpCount < maxJumps)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount++;
        }

        if (jumpReleased && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        }

        if (PlayerManager.instance != null && PlayerManager.instance.data.ownedItems.Contains("Double Jump"))
        {
            maxJumps = 2;
        }
        else
        {
            maxJumps = 1;
        }
    }

    private void LookDown()
    {
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            followPoint.localPosition = new Vector3(0, -5, 0);
        }
        else
        {
            followPoint.localPosition = new Vector3(0, 0, 0);
        }
    }

    private void UpdateAnimations()
    {
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isRunning", Mathf.Abs(rb.linearVelocity.x) > 0.1f && isGrounded);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            jumpCount = 0;
            isGrounded = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
