using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float jumpForce = 8f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && rb.linearVelocity.y < 0.01f && rb.linearVelocity.y > -0.01f)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
        rb.linearVelocity = new Vector2(moveInput * 6f, rb.linearVelocity.y);
    }
}
