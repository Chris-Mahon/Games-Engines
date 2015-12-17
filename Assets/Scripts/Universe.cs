using UnityEngine;
using System.Collections;

public class Universe : MonoBehaviour 
{
	private GameObject player;
	private Vector2 startPos;
	private bool isLoading = true;

	// Use this for initialization
	void Start () 
	{
		int i = 0;
		player = gameObject.GetComponent<SpawnPlatforms>().playerObject;
		startPos = player.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		isLoading = gameObject.GetComponent<SpawnPlatforms> ().isLoading;
		if (isLoading != true) 
		{
			player = gameObject.GetComponent<SpawnPlatforms>().playerObject;
			if (player.GetComponent<CCharacterController>().health < 1) 
			{
				Debug.Log ("Dead");
				player.transform.position = startPos;
				player.GetComponent<CCharacterController>().health = 6;
			}
			GameObject[] camera = GameObject.FindGameObjectsWithTag ("MainCamera");
			camera [0].AddComponent<CameraFollow> ();
			camera [0].GetComponent<CameraFollow> ().finishLoading();
			Debug.Log ("Camera, Lights, ACTION!");
		} 
	}
}

