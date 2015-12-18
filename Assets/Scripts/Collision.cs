using UnityEngine;
using System.Collections;

public class Collision : MonoBehaviour {
	public Sprite collidedSpike;
	// Use this for initialization
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Player")
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = collidedSpike;
		}
	}
}
