using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class player_fhg : MonoBehaviour
{
    // Start is called before the first frame update

    public float moveSpeed = 10;
    public float jumpForce = 10;
    public Rigidbody2D rigidbody2;
    public SpriteRenderer spriteRenderer;
    public GroundChecker groundChecker;
    public bool isJump = false;
    private float moveInput;
    int jumpCount = 0;
    int maxJumps = 2;
    public float sprintMultiplier = 2;
    bool isSprinting = false;





    void Start()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {

        moveInput = Input.GetAxis("Horizontal");
        if (moveInput > 0) spriteRenderer.flipX = false;
        else if (moveInput < 0) spriteRenderer.flipX = true;

        isSprinting = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        if (Input.GetKeyDown(KeyCode.Space))
            isJump = true;



    }

    private void FixedUpdate()
    {
        float currentSpeed = moveSpeed;

        if (isSprinting)
            currentSpeed *= sprintMultiplier;
        rigidbody2.velocity = new Vector2(moveInput * currentSpeed, rigidbody2.velocity.y);


        if (groundChecker.isGrounded)
        {
            jumpCount = 0;
        }


        if (isJump && jumpCount < maxJumps)
        {

            rigidbody2.velocity = new Vector2(rigidbody2.velocity.x, 0);
            rigidbody2.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            jumpCount++;
        }

        isJump = false;

    }
}



