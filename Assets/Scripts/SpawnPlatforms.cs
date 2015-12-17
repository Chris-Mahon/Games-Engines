using UnityEngine;
using System.Collections;

public class SpawnPlatforms : MonoBehaviour {
	public GameObject block;
	public GameObject girder;
	public GameObject player;
	public GameObject enemy;
	[HideInInspector] public bool isLoading = true;
	[HideInInspector] public GameObject playerObject;

	// Use this for initialization
	void Start () 
	{
		float i, j, l;
		
		playerObject = Instantiate(player, new Vector2(-1.1f, 0f), Quaternion.identity) as GameObject;
		Debug.Log(playerObject.name);

		Instantiate(enemy, new Vector2(-1.8f, -1.5f), Quaternion.identity);

		/*Instantiate(block, new Vector2(-2.2f, -1.8f), Quaternion.identity);
		Instantiate(block, new Vector2(-2.2f, -1.6f), Quaternion.identity);*/
		Instantiate(block, new Vector2(-0.8f, -1.8f), Quaternion.identity);
		Instantiate(block, new Vector2(-0.4f, -1.8f), Quaternion.identity);
		Instantiate(block, new Vector2(-0.4f, -1.6f), Quaternion.identity);
		Instantiate(block, new Vector2(0f, -1.8f), Quaternion.identity);
		Instantiate(block, new Vector2(0f, -1.6f), Quaternion.identity);
		Instantiate(block, new Vector2(0f, -1.4f), Quaternion.identity);

		for (i=-5f; i<10f; i+=.2f) 
		{
			Instantiate(block, new Vector2(i, -2f), Quaternion.identity);
			for (l=-2; l>-4; l-=.2f)
			{
				Instantiate(girder, new Vector2(i, l), Quaternion.identity);
			}
		}
		Debug.Log (i);

		for (j=i+0.5f; j<5f; j+=.2f) 
		{
			Instantiate(block, new Vector2(j, -1.2f), Quaternion.identity);
			for (l=-1.2f; l>-4; l-=.2f)
			{
				Instantiate(girder, new Vector2(j, l), Quaternion.identity);
			}
		}


		isLoading = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
}
