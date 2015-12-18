using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	[HideInInspector] private GameObject player;
	[HideInInspector] public bool isLoading = true;
	private float currX = 0;
	private float currY = 0;
	public bool debugging = false;
	public AudioClip aClip1, aClip2;
	private AudioSource aSource;
	private int currSong;
	private bool isStartingSong = false;
	private float currSongLoad;
	private float maxSongLoad=0.2f;
	// Use this for initialization
	void Start() 
	{
		currSong = 1;
		aSource = gameObject.AddComponent<AudioSource >();

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (player == null) 
		{
			player = GameObject.FindGameObjectWithTag("Player");
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

		if (currSongLoad > maxSongLoad)
		{
			isStartingSong = false;
			currSongLoad = 0;
		}
		if (!isStartingSong)
		{
			if (currSong == 0 && Input.GetAxis ("Song1") >0) 
			{
				currSong = 1;
				aSource.Stop();
				aSource.clip = aClip1 as AudioClip;
				aSource.loop = true;
				aSource.Play ();
				isStartingSong = true;
			}
			else if (currSong == 1 && Input.GetAxis("Song1")>0)
			{
				currSong = 0;
				aSource.Stop();
				aSource.clip = aClip2 as AudioClip;
				aSource.loop = true;
				aSource.Play ();
				isStartingSong = true;
			}
		}
		else
		{
			currSongLoad += 1*Time.deltaTime;
		}
		Debug.Log (currSongLoad);
		transform.position = new Vector3(currX, currY, transform.position.z);
	}

	public void finishLoading()
	{
		isLoading = false;
	}
}


