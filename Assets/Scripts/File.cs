using UnityEngine;
using System.Collections;

public class File : MonoBehaviour 
{
	public float waitTime;
	public bool facingRight = true;
	private float currTime;
	private bool isJumping;
	private bool isWaiting;
	public GameObject Bullet;
	private GameObject recentBullet;
	//private Projectile projectile;
	[HideInInspector] public Rigidbody2D rb2d;

	// Use this for initialization
	void Start () 
	{
		currTime = 0;
		isJumping = false;
		rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Debug.Log (isJumping + " " + isWaiting);
		if (isJumping != true && isWaiting != true) 
		{
			transform.Translate(Vector3.up * 10 * Time.deltaTime, Space.Self);
			recentBullet = Instantiate(Bullet, transform.position, transform.rotation) as GameObject;
			//projectile = recentBullet.GetComponent<Projectile>();

			if (facingRight == true)
			{
				//projectile.direction = 1;
			}
			else
			{
				//projectile.direction = -1;
			}

			isJumping = true;
			isWaiting = true;
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
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.name == "Block(Clone)") 
		 {
			isJumping = false;
		}
	}

}
