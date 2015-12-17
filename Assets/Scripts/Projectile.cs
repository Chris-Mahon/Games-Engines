using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour 
{
	private int direction = 0;
	public string source;
	private Vector2 start;
	// Use this for initialization
	void Start () 
	{
		start = transform.position;
	}

	public void Init(bool forward, string sauce)
	{
		source = sauce;
		direction = 4;
	}
	// Update is called once per frame
	void Update () 
	{
		transform.Translate(Vector2.right * direction * Time.deltaTime);

		if (Vector2.Distance(start, transform.position) > 12) 
		{
			Destroy(gameObject, 0f);
		}
	}
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.name == "Block") 
		{
			Destroy (gameObject, 0f);
			
		}
	}
}
