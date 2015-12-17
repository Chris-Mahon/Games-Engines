using UnityEngine;
using System.Collections;

public class CCharacterController : MonoBehaviour 
{
	public bool facingRight = true;
	public bool debugging = false;
	public float jSpeed = 2f;
	public float sSpeed = 2f;
	public float upDirection;
	public int health = 6;
	public GameObject Bullet;
	public float jumpingTime= .5f;
	public float reloadTime = .5f;
	public float knockbackTime = .5f;
	public float movementFactor = 4;
	private float currJumpTime;
	private float currReloadTime;
	private float currKnocked;
	private bool isReloading;
	private bool isJumping;
	private bool isKnockedBack;
	private bool isGrounded;
	private float sideDirection;
	private GameObject recentBullet;
	private Projectile projectile;
	private int direction = 1;
	private int collisionCount = 0;
	private float distanceAmount = 0;
	private float upAmount = 0;

	// Use this for initialization
	void Start () 
	{
		upDirection = -1;
		isJumping = false;
		isGrounded = false;
		Flip ();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			Application.Quit ();
		} 
		if (transform.position.y < -4) 
		{
			transform.position = new Vector2 (-1f, -1f);
		}


		upDirection = Input.GetAxis ("Jump");
		sideDirection = Input.GetAxis ("Horizontal");
		
		Flip();

		
		if (currKnocked > knockbackTime) 
		{
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
		if (currReloadTime > reloadTime) 
		{
			isReloading = false;
			currReloadTime = 0;
		}

		transform.Translate ((Vector2.up * upDirection * Time.deltaTime * jSpeed)
				+ (Vector2.right * sideDirection * Time.deltaTime * sSpeed), Space.Self);

		if (Input.GetAxis("Fire1")>0 && !isReloading) 
		{
			recentBullet = Instantiate(Bullet, transform.position +(Vector3.right*.2f*direction), transform.rotation) as GameObject;
			projectile = recentBullet.GetComponent<Projectile>();
			projectile.Init(facingRight, "Player");
			isReloading  = true;
		}
	
	}
	
	void OnCollisionEnter2D(Collision2D other)
	{
		Debug.Log(other.gameObject.name);
		if (other.gameObject.name != "Projectile(Clone)" && other.gameObject.name != "Enemy") 
		{
			collisionCount++;
		}

		if (other.gameObject.name == "Block(Clone)") 
		{
			upDirection = 0;
			currJumpTime = 0;
		} 
		else if (other.gameObject.name == "Enemy") 
		{
			isKnockedBack = true;
			health--;
		}
		if (other.gameObject.name == "Projectile(Clone)") 
		{
			if (other.gameObject.GetComponent<Projectile>().source == "Enemy")
			{
				Destroy(other.gameObject, 0f);
				health--;
			}
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
		if (debugging) 
		{
			GUI.color = Color.black;
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
			GUI.Label (new Rect (500, 10, 700, 20), "Health: " + health);

		}
	}

	void Flip()
	{
		if (sideDirection < 0) 
		{
			direction = -1;
		} 
		else if (sideDirection >0) 
		{
			direction = 1;
		}
		if (sideDirection < 0 && facingRight == true) 
		{
			facingRight = !facingRight;
			transform.Rotate(new Vector3(0, 1, 0), -180);
		} 
		else if (sideDirection >0 && facingRight == false)
		{
			facingRight = true;
			transform.Rotate(new Vector3(0, 1, 0),  180);
		}
		if (sideDirection <0 && !facingRight && !isKnockedBack)
		{
			sideDirection *= -1;
		}

		
	}
}
