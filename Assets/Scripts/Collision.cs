using UnityEngine;
using System.Collections;

public class Collision : MonoBehaviour {
	public Sprite collidedSpike;
	// Use this for initialization
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.name == "Enemy" || other.gameObject.name == "GigaGuy")
		{
			gameObject.GetComponent<SpriteRenderer> ().sprite = collidedSpike;
		}
	}
}
