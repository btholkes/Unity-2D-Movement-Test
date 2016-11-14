using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour
{
    public Transform groundCheck;       //transform linked to player that tests for ground
    public Transform wallCheck;       //transform linked to player that tests for wall on the right
    public LayerMask groundLayer;       //added layer dropdown to set what layer it will test against

    bool grounded = false;     //ground boolean
    bool touchingWall = false;      //wall right boolean
    bool doubleJump = false;

    public float moveSpeed = 0f;        //variable for speed set within unity for easy change
    public float jumpForce = 0f;        //variable for jump strength set within unity
    public float jumpPushForce = 0f;

    private new Rigidbody2D rigidbody2D;        //variable to hold and call the rigidbody

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();      //when the program starts, it sets the rigidbody
    }
	
	void FixedUpdate ()
    {
        touchingWall = Physics2D.OverlapCircle(wallCheck.position, 1f, groundLayer);

        grounded = Physics2D.OverlapCircle(groundCheck.position, .5f, groundLayer);        //tests if the player is grounded

        if (grounded)
            doubleJump = false;

        if (touchingWall)
        {
            grounded = false;
            doubleJump = false;
        }

        float move = Input.GetAxis("Horizontal");

        rigidbody2D.velocity = new Vector2(move * moveSpeed, rigidbody2D.velocity.y);
	}

    void Update()
    {
        if ((grounded || !doubleJump) && Input.GetButtonDown("Jump"))
        {
            rigidbody2D.velocity = new Vector2(0, jumpForce);

            if (!doubleJump && !grounded)
                doubleJump = true;
        }

        if (touchingWall && Input.GetButtonDown("Jump"))
            wallJump();
    }

    void wallJump()
    {
        rigidbody2D.AddForce(new Vector2(-jumpPushForce, jumpForce));
    }
}
