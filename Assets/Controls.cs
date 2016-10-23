using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour
{
    public Transform groundCheck;       //transform linked to player that tests for ground
    public Transform wallCheck_R;       //transform linked to player that tests for wall on the right
    public Transform wallCheck_L;       //transform linked to player that tests for wall on the left
    public LayerMask groundLayer;       //added layer dropdown to set what layer it will test against

    [HideInInspector] public bool grounded = false;     //ground boolean
    [HideInInspector] public bool walledR = false;      //wall right boolean
    [HideInInspector] public bool walledL = false;      //wall left boolean
    [HideInInspector] public bool jump;     //boolean for if the player can jump or not

    public float moveSpeed = 0f;        //variable for speed set within unity for easy change
    public float jumpSpeed = 0f;        //variable for jump strength set within unity
    public float maxSpeed = 0f;     //variable for max speed set within unity

    private new Rigidbody2D rigidbody2D;        //variable to hold and call the rigidbody

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();      //when the program starts, it sets the rigidbody
    }

    void Update()
    {
        walledR = Physics2D.OverlapCircle(wallCheck_R.position, 0.05f, groundLayer);        
        walledL = Physics2D.OverlapCircle(wallCheck_L.position, 0.05f, groundLayer);        /*right and left tests for if the player is up against a wall, sets matching boolean variable accordingly*/
        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.5f, groundLayer);        //tests if the player is grounded

        if (Input.GetButtonDown("Jump") && (grounded||walledL||walledR))        //if the jump button is pressed, and if the player is grounded or on a wall
        {
            jump = true;        //the player is allowed to jump
        }
    }
	
	void FixedUpdate ()
    {
        float movex = Input.GetAxis("Horizontal");      //sets movex equal to positive or negative movement (lets it move in the + or - direction, meaning right or left accordingly)

        if (movex * rigidbody2D.velocity.x < maxSpeed)      //if the movement speed is under the max allowed speed
            rigidbody2D.AddForce(Vector2.right * movex * moveSpeed);        //adds to the speed of the rigidbody multiplying movespeed by the current speed by the direction it should apply in, and by movespeed
        if (Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed)       //if the speed of the rigidbody is over the max speed
            rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);      //set the speed of the rigidbody equal to + or - max speed, and the y velocity

        if (jump)       //if the player can jump
        {
            rigidbody2D.AddForce(new Vector2(0f, jumpSpeed));       //add force to the vector equal to the set jump force
            jump = false;       //set jump equal to false so it doesn't loop infinitely and jump forever
        }
	}
}
