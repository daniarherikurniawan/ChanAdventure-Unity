using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {
	public GameObject explosionPrehab;
	public TextMesh timeofbomb;
	public int MaxDamagePlayer;
	public float startTime;
	private GameObject explosioninstance;
	private string currentTime;
	GameObject player;
	PlayerHealth playerHealth;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent <PlayerHealth> ();

	}
	
	// Update is called once per frame
	void Update () {
		startTime -= Time.deltaTime;
		currentTime = startTime.ToString ();
		timeofbomb.text = currentTime;
		if (startTime < 0) {
			explosioninstance = (GameObject)Instantiate (explosionPrehab, transform.position, Quaternion.identity);
			PutExplosion.countbomb--;
			Destroy(this.gameObject);
			ExplosionDamage(transform.position,4);
		}
	}

	void ExplosionDamage(Vector3 center, float radius) {
		Collider[] hitColliders = Physics.OverlapSphere(center, radius);
		int i = 0;
		while (i < hitColliders.Length) {

			switch (hitColliders[i].tag){
				case "Rock" :
					Destroy(GameObject.Find( hitColliders[i].name));
				break;

				case "Door" :
					Destroy(GameObject.Find( hitColliders[i].name));
				break;
				
				case "BrickToBeSpawn" :
					Destroy(GameObject.Find( hitColliders[i].name));
				break;

				case "Player" :
					float dist = (int) Vector3.Distance(center, player.transform.position);
					float damage = ((radius-dist)/radius)*MaxDamagePlayer ;
					playerHealth.TakeDamage((int)damage);
				break;

				case "Enemy" :
					Destroy(GameObject.Find( hitColliders[i].name));
				break;

			}
			i++;
		}
	}
	
	/*void OnCollisionEnter(Collision collisionInfo)
	{
		print("Detected collision between " + gameObject.name + " and " + collisionInfo.collider.name);
		print("There are " + collisionInfo.contacts.Length + " point(s) of contacts");
		print("Their relative velocity is " + collisionInfo.relativeVelocity);
	}*/
}
