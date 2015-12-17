using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	[HideInInspector] private GameObject player;
	[HideInInspector] public bool isLoading = true;
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
			GameObject[] temp = GameObject.FindGameObjectsWithTag ("Player");
			player = temp [0];
		}
		transform.position = new Vector3 (player.transform.position.x, player.transform.position.y + 1, transform.position.z);
	}

	public void finishLoading()
	{
		isLoading = false;
	}
}


