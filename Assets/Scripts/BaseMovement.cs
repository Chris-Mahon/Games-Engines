using UnityEngine;
using System.Collections;

public class BaseMovement : MonoBehaviour 
{
	
	public bool facingRight = false;
	public bool debugging = false;
	public float jumpingTime=.5f;
	public int health;
	public GameObject Bullet;
	[HideInInspector] public float currTime;
	[HideInInspector] public float currJumpTime;
	[HideInInspector] public bool isGrounded;
	[HideInInspector] public bool isJumping;
	[HideInInspector] public GameObject recentBullet;
	[HideInInspector] public Projectile projectile;
	[HideInInspector] public float upDirection;
	[HideInInspector] public float sideDirection;
	[HideInInspector] public int direction;
	[HideInInspector] public bool isKnockedBack;


	public void shootProjectile(string Source)
	{
		recentBullet = Instantiate(Bullet, transform.position +(Vector3.right*.2f*direction), transform.rotation) as GameObject;
		projectile = recentBullet.GetComponent<Projectile>();
		projectile.Init(facingRight, Source);
	}
	public void Flip()
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
