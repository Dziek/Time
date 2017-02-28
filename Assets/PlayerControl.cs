using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : Player {

	[HideInInspector] public bool facingRight = true;
    [HideInInspector] public bool jump = false;
    // [HideInInspector] public bool jetpack = false;
    public float moveForce = 365f;
    public float maxSpeed = 5f;
    public float jumpForce = 1000f;
	
	public float slideForce = 750f;
	public float maxSpeedSliding = 10f;
	public float slideJumpForce = 1500f;
	
	public float maxSpeedAir = 1.5f;
	
    // public float jetpackForce = 1000f;
    public Transform groundCheck;

	// private bool inShip;
	
    private bool isGrounded = false;
    // private Animator anim;
    private Rigidbody2D rb2d;
    // private Rigidbody rb2d;
	
	// [HideInInspector]
	// public bool seated;

    // Use this for initialization
    void Awake () 
    {
        // anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        // rb2d = GetComponent<Rigidbody>();
    }
    
    // Update is called once per frame
    void Update () 
    {
        // grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        // isGrounded = Physics2D.Linecast(transform.position + Vector3.up * -transform.localScale.y, groundCheck.position);
		
        // isGrounded = Physics2D.CircleCast(transform.position + Vector3.up * (-transform.localScale.y/2), 0.15f, -Vector2.up, 0.1f);
		
        isGrounded = Physics2D.CircleCast(transform.position + Vector3.up * (-transform.localScale.y/2), 0.15f, -Vector2.up, 0.1f, ~(1 << LayerMask.NameToLayer("Player") | (1 << LayerMask.NameToLayer("Frame"))));
		// RaycastHit2D hit = Physics2D.CircleCast(transform.position + Vector3.up * (-transform.localScale.y/2), 0.15f, -Vector2.up, 0.1f, 1 << ~(1 << LayerMask.NameToLayer("Player")) | ~(1 << LayerMask.NameToLayer("Frame")));

	    // isGrounded = Physics2D.CircleCast(transform.position + Vector3.up * (-transform.localScale.y/2), 0.15f, -Vector2.up, 0.1f, 1 << LayerMask.NameToLayer("Geometry"));
        // RaycastHit2D hit = Physics2D.CircleCast(transform.position + Vector3.up * (-transform.localScale.y/2), 0.15f, -Vector2.up, 0.1f, 1 << LayerMask.NameToLayer("Geometry"));
		
		// if (isSeated == false)
		// {
			if (Input.GetButtonDown("Jump") && isGrounded)
			// if (Input.GetKeyDown("space") && grounded)
			{
				// Debug.Log("JUMP");
				// Debug.Log(hit.collider);
				// Debug.Break();
				jump = true;
			}	
			
			// if (Input.GetButtonDown("Jump"))
			// {
				// Debug.Break();
			// }
		// }
		
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			isSliding = true;
			
			// gameObject.transform.localScale = new Vector2(1, 0.5f);
			// gameObject.transform.Translate(Vector3.up * -0.5f);
		}
		
		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			isSliding = false;
			
			// gameObject.transform.localScale = Vector2.one;
			// gameObject.transform.Translate(Vector3.up * 0.5f);
		}
		
		// if (isInBasket == false)
		// {
			// if (Input.GetButton("Jump"))
			// {
				// jetpack = true;
			// }
		// }
		
		// if (Input.GetButtonUp("Jump") && jetpack == true)
		// {
			// jetpack = false;
		// }
    }

    void FixedUpdate()
    {
		// if (isSeated == false)
		// {
			float h = Input.GetAxisRaw("Horizontal");
			
			// if (isGrounded)
			// {
				if (isSliding == true)
				{
					float dir = facingRight ? 1 : -1;
					rb2d.velocity = new Vector2(dir * maxSpeedSliding, rb2d.velocity.y);
				}else{
					rb2d.velocity = new Vector2(h * maxSpeed, rb2d.velocity.y);
				}
			// }
			// else{
				// rb2d.velocity = new Vector2(h * maxSpeedAir, rb2d.velocity.y);
			// }
			
			// // anim.SetFloat("Speed", Mathf.Abs(h));
			
			// if (isSliding == true)
			// {
				// // rb2d.AddForce(Vector2.right * h * slideForce);
				// rb2d.AddForce(Vector2.right * slideForce);
			// }else{
			
				// if (h * rb2d.velocity.x < maxSpeed)
				// {
					// rb2d.AddForce(Vector2.right * h * moveForce);
					
					// // if (isSliding)
					// // {
						// // rb2d.AddForce(Vector2.right * h * moveForce);
					// // }
					
					// // if (isInBasket == true)
					// // {
						// // if (basketScript.usingLeftLever && h > 0)
						// // {
							// // rb2d.AddForce(Vector2.right * basketScript.horizontalMovementForce);
						// // }
					// // }
					
					// // if (isInBasket == true)
					// // {
						// // if (basketScript.usingLeftLever && h > 0)
						// // {
							// // rb2d.velocity += basketScript.rb2D.velocity;
							
						// // }
					// // }
					
					// // Debug.Log("Player: " + rb2d.velocity + " Basket: " + basketScript.rb2D.velocity);
				// }
				
				// if (Mathf.Abs (rb2d.velocity.x) > maxSpeed)
					// rb2d.velocity = new Vector2(Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
			// }
			
			if (h > 0 && !facingRight)
				Flip ();
			else if (h < 0 && facingRight)
				Flip ();

			if (jump)
			{
				if (isSliding == true)
				{
						// anim.SetTrigger("Jump");
					rb2d.AddForce(new Vector2(0f, slideJumpForce));
					// rb2d.velocity = new Vector2(h * maxSpeedAir, rb2d.velocity.y);
					jump = false;
				}else{
					// anim.SetTrigger("Jump");
					rb2d.AddForce(new Vector2(0f, jumpForce));
					jump = false;
				}
				
				// Debug.Log("Jumping");
			}
			
			// if (jetpack && isInBasket == false)
			{
				// anim.SetTrigger("Jump");
				// rb2d.AddForce(new Vector2(0f, jetpackForce));
				// jump = false;
			}
		// }
    }


    void Flip()
    {
        facingRight = !facingRight;
        // Vector3 theScale = transform.localScale;
        // theScale.x *= -1;
        // transform.localScale = theScale;
    }
	
	void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position + Vector3.up * (-transform.localScale.y/2), 0.15f);
        Gizmos.DrawSphere(transform.position + Vector3.up * (-transform.localScale.y/2) + -Vector3.up * 0.1f, 0.15f);
    }
}
