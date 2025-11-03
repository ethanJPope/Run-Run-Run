using UnityEngine;
using System.Collections;
using Unity.Cinemachine;
using System.Data.Common;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private int maxJumps = 1;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Transform followPoint;
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
    }

    private void HandleMovement()
    {
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

        if (PlayerManager.instance != null && PlayerManager.instance.data.ownedItems.Contains("DoubleJump"))
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
