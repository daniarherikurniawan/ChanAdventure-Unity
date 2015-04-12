using UnityEngine;
using System.Collections;

public class PutBricks : MonoBehaviour {
	public GameObject BricksPrefab;
	public int bricklimit;
	public static int countbrick;
	private GameObject brickinstance;
	AudioSource SoundBrick;
	public AudioClip putBrickSound; 

	GameObject player;
	// Use this for initialization
	void Start () {
		countbrick= 0;
		SoundBrick = GetComponent <AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown (KeyCode.E) && countbrick < bricklimit && ScoreManager.itemBrick > 0 ) {
			countbrick++;
			ScoreManager.itemBrick--;

			SoundBrick.clip = putBrickSound;
			SoundBrick.Play();


			player = GameObject.FindGameObjectWithTag ("Player");
			Vector3 position = player.transform.TransformPoint(Vector3.forward * 3);
			brickinstance = (GameObject) Instantiate(BricksPrefab, position + new Vector3(0,0,0),Quaternion.Euler(0,0,0));
		}
	}

}
