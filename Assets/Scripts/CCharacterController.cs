using UnityEngine;
using System.Collections;

public class CCharacterController : BaseMovement 
{
	public float jSpeed = 2f;
	public float sSpeed = 2f;
	public float reloadTime = .5f;
	public float invincTime = .2f;
	public float knockbackTime = .5f;
	public float movementFactor = 4;
	[HideInInspector] public bool isFinished;
	private float currReloadTime;
	private float currKnocked;
	private float currInvincTime;
	private bool isReloading;
	private bool isInvincible;
	private int collisionCount = 0;

	// Use this for initialization
	void Start () 
	{
		ResetValues();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			Application.Quit ();
		} 
		if (transform.position.y < -2) 
		{
			health = 0;
		}

		upDirection = Input.GetAxis ("Jump");
		sideDirection = Input.GetAxis ("Horizontal");

		if (!isKnockedBack)
		{
			Flip();
		}
		
		if (currKnocked > knockbackTime) {
			isKnockedBack = false;
			currKnocked = 0;
		}

		if (isGrounded && upDirection > 0) 
		{
			isGrounded = false;
			isJumping = true;
		}

		if (currJumpTime >= jumpingTime) 
		{
			isJumping = false;
		} 


		else if (upDirection == 0 && !isGrounded && currJumpTime > .3) 
		{
			upDirection = -1;
			isJumping = false;
		} 

		if (collisionCount > 0) 
		{
			isGrounded = true;
		} 
		else 
		{
			isGrounded = false;
		}

		if (isKnockedBack) 
		{
			sideDirection = -1;
			upDirection = 0.5f;
			currKnocked += 1*Time.deltaTime;

		}
		else if (isJumping)
		{
			currJumpTime += 1*Time.deltaTime;
			sideDirection /= 2;
			upDirection = 1;

		} 
		else if (!isGrounded)
		{
			upDirection = -1;
			sideDirection /= 2;
		}

		if (isReloading) 
		{
			currReloadTime += 1*Time.deltaTime;
		}

		if (currInvincTime > invincTime) 
		{
			currInvincTime = 0;
			isInvincible = false;
		}
		if (currReloadTime > reloadTime) 
		{
			isReloading = false;
			currReloadTime = 0;
		}

		if (isInvincible) 
		{
			currInvincTime += 1 * Time.deltaTime;
			gameObject.transform.GetComponent<SpriteRenderer> ().color = new Color(1, 1, 1, .5f);
		} 
		else 
		{
			gameObject.transform.GetComponent<SpriteRenderer> ().color = new Color(1, 1, 1, 1);
		}

		if (Input.GetAxis("Fire1")>0 && !isReloading) 
		{
			shootProjectile("Player");
			isReloading  = true;
		}

	}

	void FixedUpdate()
	{
		transform.Translate ((Vector2.up * upDirection * Time.deltaTime * jSpeed)
		                     + (Vector2.right * sideDirection * Time.deltaTime * sSpeed), Space.Self);


	}
	
	void OnCollisionEnter2D(Collision2D other)
	{
		Debug.Log(other.gameObject.name);
		if (other.gameObject.name != "Projectile(Clone)" && other.gameObject.name != "Enemy" && other.gameObject.name != "HealthUp") 
		{
			collisionCount++;
		}

		if (other.gameObject.name == "Block") 
		{
			upDirection = 0;
			currJumpTime = 0;
		} 
		else if (other.gameObject.name == "Obstacle") 
		{
			if (!isInvincible)
			{
				upDirection = 0;
				currJumpTime = 0;
				health--;
				isInvincible = true;
			}
		} 
		else if (other.gameObject.name == "Enemy") 
		{
			if (!isInvincible)
			{
				isKnockedBack = true;
				health--;
				isInvincible = true;
			}
		}
		if (other.gameObject.name == "Projectile(Clone)") 
		{
			if (other.gameObject.GetComponent<Projectile>().source == "Enemy")
			{
				Destroy(other.gameObject, 0f);
			}
			if (!isInvincible && other.gameObject.GetComponent<Projectile>().source == "Enemy")
			{
				isKnockedBack = true;
				health--;
				isInvincible = true;
			}
		}
		if (other.gameObject.name == "EndGoal") 
		{
			isFinished = true;
			health+=2;
		}
		if (other.gameObject.name == "HealthUp")
		{
			Destroy(other.gameObject, 0f);
			health+=2;
		}
	}

	void OnCollisionExit2D(Collision2D other)
	{
		if (other.gameObject.name != "Projectile(Clone)" && other.gameObject.name != "Enemy") 
		{
			collisionCount--;
		}
	}

	void OnGUI()
	{
		GUI.color = Color.black;
		if (debugging) 
		{
			GUI.Label (new Rect (10, 10, 200, 20), "isGrounded: " + isGrounded);
			GUI.Label (new Rect (10, 20, 200, 30), "isJumping: " + isJumping);
			GUI.Label (new Rect (10, 30, 200, 40), "isReloading: " + isReloading);
			GUI.Label (new Rect (10, 40, 200, 50), "upDirection: " + upDirection);
			GUI.Label (new Rect (10, 50, 200, 60), "sideDirection: " + sideDirection);
			GUI.Label (new Rect (10, 60, 200, 70), "currJumpTime: " + currJumpTime);
			GUI.Label (new Rect (10, 70, 200, 80), "facingRight: " + facingRight);
			GUI.Label (new Rect (10, 80, 200, 90), "CollisionCount: " + collisionCount);
			GUI.Label (new Rect (10, 90, 200, 100), "isKnockedBack: " + isKnockedBack);
			GUI.Label (new Rect (10, 100, 200, 110), "currKnocked:" + currKnocked);

		}
		GUI.Label (new Rect (500, 10, 700, 20), "Health: " + health);
	}
	public void ResetValues()
	{
		upDirection = -1;
		collisionCount = 0;
		health = 6;
		isJumping = false;
		isGrounded = false;
		isFinished = false;
		Flip ();
	}
}
