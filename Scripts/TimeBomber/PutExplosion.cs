using UnityEngine;
using System.Collections;

public class PutExplosion : MonoBehaviour {
	public GameObject dynamitePrehab;
	public int bomblimit;
	public static int countbomb;
	private GameObject explosioninstance;
	private GameObject dynamiteinstance;
	GameObject player;
	// Use this for initialization
	void Start () {
		countbomb = 0;
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown (KeyCode.Q) && countbomb < bomblimit && ScoreManager.itemBomb > 0 ) {
			countbomb++;
			ScoreManager.itemBomb--;
			player = GameObject.FindGameObjectWithTag ("Player");
			Vector3 position = player.transform.TransformPoint(Vector3.forward * 3);
			dynamiteinstance = (GameObject) Instantiate(dynamitePrehab, position + new Vector3(0,0,0),Quaternion.Euler(270,0,0));
		}
	}

}
