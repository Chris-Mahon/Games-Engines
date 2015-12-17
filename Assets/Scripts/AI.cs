using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour 
{
	public float waitTime=3;
	public bool facingRight = false;
	public float jSpeed = 2f;
	private float currTime;
	private float currJumpTime;
	public float jumpingTime=2;
	private bool isJumping;
	private bool isWaiting;
	private bool isGrounded;
	public bool debugging = false;
	private int direction;
	public GameObject Bullet;
	private GameObject recentBullet;
	private GameObject player;
	private Projectile projectile;
	[HideInInspector] public Rigidbody2D rb2d;

	// Use this for initialization
	void Start () 
	{
		currTime = 0;
		direction = -1;
		isJumping = false;
		isGrounded = false;
		rb2d = GetComponent<Rigidbody2D>();
		player = GameObject.FindGameObjectWithTag ("Player");
		gameObject.name = "Enemy";
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (player.transform.position.x - transform.position.y > 0) {
			facingRight = true;
			transform.Rotate(new Vector3(0, 1, 0), -180);
		} 
		else 
		{
			facingRight = false;
			transform.Rotate(new Vector3(0, 1, 0), 180);
		}

		if (isJumping != true && isWaiting != true && isGrounded) 
		{
			isGrounded = false;
			isJumping = true;
			isWaiting = true;
			direction = 1;
		}

		if (isJumping == true) 
		{
			currJumpTime += 1*Time.deltaTime;
		}

		if (isWaiting == true) 
		{
			currTime += 1*Time.deltaTime;
		}

		if (currTime >=waitTime)
		{
			Debug.Log("Reset!");
			currTime = 0;
			isWaiting = false;
		}

		if (currJumpTime >= jumpingTime) 
		{
			//Debug.Log(transform.position + " " + direction + " ");
			direction = -1;	
			currJumpTime = 0;
			isJumping = false;

			recentBullet = Instantiate(Bullet, transform.position, transform.rotation) as GameObject;
			projectile = recentBullet.GetComponent<Projectile>();
			projectile.Init(facingRight, "Enemy");

		}
		transform.Translate(Vector2.up * direction * Time.deltaTime * jSpeed);
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.name == "Block(Clone)") 
		{
			direction = 0;
			currJumpTime = 0;
			currTime = 0;
			isJumping = false;			
			isGrounded = true;

		} 
		else if (other.gameObject.name == "Projectile(Clone)") 
		{
			if (other.gameObject.GetComponent<Projectile>().source == "Player")
			{
				Destroy(other.gameObject, 0f);
				Instantiate(GameObject.FindGameObjectWithTag("Enemy"), new Vector2(-2, 3f), Quaternion.identity);
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
