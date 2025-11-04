using UnityEngine;

public class player_fhg : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpForce = 10f;
    public Rigidbody2D rigidbody2;
    public SpriteRenderer spriteRenderer;
    public GroundChecker groundChecker;

    private Animator anim;
    private float moveInput;
    private int jumpCount = 0;
    private int maxJumps = 2;
    public float sprintMultiplier = 2f;
    private bool isSprinting = false;
    private bool wantJump = false;

    void Awake()
    {
        // возьми компоненты автоматически
        if (!rigidbody2) rigidbody2 = GetComponent<Rigidbody2D>();
        if (!spriteRenderer) spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal"); 
        if (moveInput > 0) spriteRenderer.flipX = false;
        else if (moveInput < 0) spriteRenderer.flipX = true;

        isSprinting = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        if (Input.GetKeyDown(KeyCode.Space)) wantJump = true;

      
        if (anim)
        {
            anim.SetFloat("Speed", Mathf.Abs(moveInput));
            if (groundChecker) anim.SetBool("IsGrounded", groundChecker.isGrounded);
        }
    }

    void FixedUpdate()
    {
        float currentSpeed = isSprinting ? moveSpeed * sprintMultiplier : moveSpeed;
        rigidbody2.velocity = new Vector2(moveInput * currentSpeed, rigidbody2.velocity.y);

        if (groundChecker && groundChecker.isGrounded) jumpCount = 0;

        if (wantJump && jumpCount < maxJumps)
        {
            rigidbody2.velocity = new Vector2(rigidbody2.velocity.x, 0f);
            rigidbody2.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount++;

            if (anim) anim.SetTrigger("Jump"); 
        }
        anim.SetFloat("Speed", Mathf.Abs(moveInput));

        wantJump = false;
    }
}