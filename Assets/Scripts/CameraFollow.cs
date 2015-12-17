using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	[HideInInspector] private GameObject player;
	[HideInInspector] public bool isLoading = true;
	private float currX = 0;
	private float currY = 0;
	public bool debugging = false;
	// Use this for initialization
	void Start() 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (player == null) 
		{
			player = GameObject.FindGameObjectWithTag("Player");
			Debug.Log(player.name);
		}
		if (player.transform.position.x < -.1f) 
		{
			currX = transform.position.x;
		} 
		else 
		{
			currX = player.transform.position.x;
		}

		if (player.transform.position.y < .5f)
		{
			currY = transform.position.y;
		}
		else 
		{
			currY = player.transform.position.y;
		}

		transform.position = new Vector3(currX, currY, transform.position.z);
	}

	public void finishLoading()
	{
		isLoading = false;
	}
}


