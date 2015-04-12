using UnityEngine;
using System.Collections;

public class BrickToBeSpawnBehavior : MonoBehaviour {
	
	AudioSource  coinAudio;

	void Update(){ 
		ExplosionDamage(transform.position,2);
	}
	
	void ExplosionDamage(Vector3 center, float radius) {
		Collider[] hitColliders = Physics.OverlapSphere(center, radius);
		int i = 0;
		while (i < hitColliders.Length) {
			print ("BrickBehaviour "+hitColliders[i].tag);
			PutBricks.countbrick--;
			switch (hitColliders[i].tag){
			case "Hole" :
				Destroy(GameObject.Find( hitColliders[i].name));
				Destroy(this.gameObject);
				break;
			}
			i++;
		}
	}
	
}
