using UnityEngine;
using System.Collections;
using Unity.Cinemachine;
using System.Data.Common;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Transform followPoint;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded = true;
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
        rb.linearVelocity = new Vector2(moveInput * 10f, rb.linearVelocity.y);

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

        if (jumpPressed && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        
        if(jumpReleased && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }
    }

    private void LookDown()
    {
        if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
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
