using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class player_fhg : MonoBehaviour
{
    // Start is called before the first frame update

    public float moveSpeed = 5000;
    public float jumpForce = 300;
    public Rigidbody2D rigidbody2;
    public SpriteRenderer spriteRenderer;
    public  GroundChecker groundChecker;
    public bool isJump =false;
    private float moveInput;    

    



    void Start()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        
        moveInput = Input.GetAxis("Horizontal");
       
      if(Input.GetKeyDown(KeyCode.Space))
        {
        isJump = true;
        }
       

    }

    private void FixedUpdate()
    {
        rigidbody2.velocity = new Vector2(moveInput * moveSpeed * Time.fixedDeltaTime, rigidbody2.velocity.y);   
        if(isJump && groundChecker.isGrounded)  
        {
            rigidbody2.AddForce(Vector2.up * jumpForce);
            isJump = false;

        }
}
}
