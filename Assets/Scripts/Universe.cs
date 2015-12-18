using UnityEngine;
using System.Collections;

public class Universe : MonoBehaviour 
{
	public GameObject[] segments;
	public GameObject PlayerObject;
	public int levelLength = 5;
	private GameObject[] currentLevel = new GameObject[10];
	private GameObject player;
	private Vector2 startPos;
	private Vector2 checkpoint;
	private int level = 1;

	// Use this for initialization
	void Start () 
	{
		level = 1;
		player = Instantiate (PlayerObject, new Vector2(0.5f, 0.5f), Quaternion.identity) as GameObject;
		startPos = player.transform.position;
		
		levelCreation();

	}
	
	// Update is called once per frame
	void Update () 
	{
		//isLoading = gameObject.GetComponent<SpawnPlatforms> ().isLoading;
		//player = gameObject.GetComponent<SpawnPlatforms>().playerObject;

		if (player.GetComponent<CCharacterController>().health < 1) 
		{
			player.transform.position = startPos;
			player.GetComponent<CCharacterController>().health = 6;
		}
		if (player.GetComponent<CCharacterController>().isFinished)
		{
			level++;
			levelDeletion();
			levelCreation();
		}
	}

	void levelCreation()
	{
		int i = 0;
		float offset = 0;
		currentLevel[0] = Instantiate(segments[0], new Vector2(0, 0), Quaternion.identity) as GameObject;
		int j = 0;
		for (i=0; i<levelLength; i++) 
		{
			j = Random.Range (0, segments.Length-2);
			offset +=3.3f;
			currentLevel [i+2] = Instantiate (segments [j+2], new Vector2 (offset, 0), Quaternion.identity) as GameObject;
		}
		offset += 3.3f;
		currentLevel[1] = Instantiate(segments[1], new Vector2(offset, 0), Quaternion.identity) as GameObject;
		player.GetComponent<CCharacterController>().isFinished = false;
		player.transform.position = startPos;
		checkpoint = startPos;
		player.GetComponent<CCharacterController>().ResetValues();

	}

	void levelDeletion()
	{
		for (int i=0; i<currentLevel.Length; i++) 
		{
			Destroy(currentLevel[i], 0f);
		}
	}

	void OnGUI()
	{
		GUI.color = Color.black;
		GUI.Label (new Rect (500, 20, 700, 30), "level: " + level);
	}
}

