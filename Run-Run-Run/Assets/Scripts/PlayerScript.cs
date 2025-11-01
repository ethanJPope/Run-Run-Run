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

        float moveInput = Input.GetAxis("Horizontal");
        var jumpInputReleased = Input.GetKeyUp(KeyCode.Space);
        rb.linearVelocity = new Vector2(moveInput * 10f, rb.linearVelocity.y);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
        if(jumpInputReleased && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            followPoint.localPosition = new Vector3(0f, -5f, 0f);
        }
        else
        {
            followPoint.localPosition = new Vector3(0f, 0f, 0f);
        }
        if (!isGrounded)
        {
            animator.SetBool("isGrounded", false);
        }
        else
        {
            animator.SetBool("isGrounded", true);
        }
        if (moveInput > 0 && isGrounded)
        {
            spriteRenderer.flipX = false;
            animator.SetBool("isRunning", true);
        }
        else if (moveInput < 0 && isGrounded)
        {
            spriteRenderer.flipX = true;
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
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
