using UnityEngine;
using System.Collections;

public class HoleBehaviour : MonoBehaviour {

	AudioSource  coinAudio;

	
	void OnCollisionEnter(Collision collisionInfo)
	{
		if(collisionInfo.collider.name == "Player"){
			PlayerMovement.isSinking = true;

		}

		if(collisionInfo.collider.tag == "Enemy"){
			GameObject enemy = GameObject.Find(collisionInfo.collider.name);
			EnemyHealth enemyHealth = enemy.GetComponent <EnemyHealth> ();
			enemyHealth.TakeDamage (100, transform.position);
		}
	}

	void Update(){ 

	}

	
}
