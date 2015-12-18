using UnityEngine;
using System.Collections;

public class AI : BaseMovement
{
	public float waitTime=3;
	public float jSpeed = 2f;
	public float jumpOffset = 0;
	private bool isWaiting;
	private GameObject player;
	[HideInInspector] public Rigidbody2D rb2d;

	// Use this for initialization
	void Start () 
	{
		currTime = 0+jumpOffset;
		upDirection = 0;
		health = 1;
		isJumping = false;
		isWaiting = true;
		isGrounded = true;
		player = GameObject.FindGameObjectWithTag ("Player");
		gameObject.name = "Enemy";
	}
	
	// Update is called once per frame
	void Update () 
	{
		sideDirection = player.transform.position.x - transform.position.x;
		Flip ();

		if (isJumping != true && isWaiting != true && isGrounded) 
		{
			isGrounded = false;
			isJumping = true;
			isWaiting = true;
			upDirection = 1;
		}

		if (isJumping == true) {
			currJumpTime += 1 * Time.deltaTime;
		}

		if (isWaiting == true) 
		{
			currTime += 1*Time.deltaTime;
		}

		if (currTime >=waitTime)
		{
			currTime = 0;
			isWaiting = false;
		}

		if (currJumpTime >= jumpingTime) 
		{
			upDirection = -1;	
			currJumpTime = 0;
			isJumping = false;

			if(Vector2.Distance(player.transform.position, transform.position)<5)
			{
				shootProjectile("Enemy");
			}

		}
		transform.Translate(Vector2.up * upDirection * Time.deltaTime * jSpeed);
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.name == "Block") 
		{
			upDirection = 0;
			currJumpTime = 0;
			currTime = 0;			
			isGrounded = true;
		} 
		else if (other.gameObject.name == "Projectile(Clone)") 
		{
			if (other.gameObject.GetComponent<Projectile>().source == "Player")
			{
				Destroy(other.gameObject, 0f);
				Destroy(gameObject, 0f);
			}
		}

	}

	void OnGUI()
	{
		if (debugging) 
		{
			GUI.color = Color.black;
			GUI.Label (new Rect (10, 10, 200, 20), "isGrounded: " + isGrounded);
			GUI.Label (new Rect (10, 20, 200, 30), "isWaiting: " + isWaiting);
			GUI.Label (new Rect (10, 30, 200, 40), "isJumping: " + isJumping);
			GUI.Label (new Rect (10, 40, 200, 50), "direction: " + direction);
			GUI.Label (new Rect (10, 50, 200, 60), "currTime: " + currTime);
			GUI.Label (new Rect (10, 60, 200, 70), "currJumpTime: " + currJumpTime);
		}
	}
}
