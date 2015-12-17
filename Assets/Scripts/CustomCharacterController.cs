using UnityEngine;
using System.Collections;

public class CustomCharacterController : MonoBehaviour {
	// Use this for initialization
	float moveX, moveY, maxJumpTime, currJumpTime;
	bool isGrounded;
	public float orThrust = 1;
	private float thrust;
	public float orUpthrust = 3;
	private float upthrust;
	public float topSpeed = 10f;
	public bool facingRight=true;
	private Rigidbody2D rb2d;

	void Start () 
	{
		moveY = 0;
		thrust = orThrust;
		upthrust = orUpthrust;
		maxJumpTime = .5f;
		isGrounded = false;
		rb2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		float x, y;
		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			Application.Quit ();
		} 
		else if (transform.position.y < -4) 
		{
			transform.position = new Vector2(-1, -1.1f);
		}
		else 
		{
			moveX = Input.GetAxis ("Horizontal");
			if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
			{
				isGrounded = false;
				moveY = 1;
				thrust = orThrust/2;
			}
			if (isGrounded == false)
				currJumpTime += 1* Time.deltaTime;
			if (currJumpTime > maxJumpTime)
				moveY = -0.5f;
			//force += transform.right * thrust * moveX;
			//Flip(moveX);
			rb2d.AddForce(transform.right * thrust * moveX);
			rb2d.AddForce(transform.up * upthrust * moveY);
			if (rb2d.velocity.magnitude > topSpeed)
			{
				x = rb2d.velocity.x;
				y = rb2d.velocity.y;
				if (rb2d.velocity.x > 5)
				{
					x = 5f;
				}
				else if (rb2d.velocity.y > 6)
				{
					y = 6f;
				}
				rb2d.velocity = new Vector2(x, y);
			}
		} 
	}



	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.name == "Block(Clone)") 
		{
			isGrounded = true;
			currJumpTime = 0;
			moveY = 0;
			thrust = orThrust;
		}
		if (other.gameObject.name == "Projectile(Clone)") 
		{

		}
	}


	void OnCollisionExit2D(Collision2D other)
	{	
	}
}

//transform.position += new Vector3(moveX*speed*Time.deltaTime, moveY*jSpeed*Time.deltaTime, 0);
//Vector3 movement = new Vector3(moveX*speed, moveY*jSpeed, 0);
//transform.Translate(movement*Time.deltaTime);