using UnityEngine;
using System.Collections;

public class BombBehavior : MonoBehaviour {

	AudioSource  coinAudio;
 
	void OnTriggerEnter(Collider collider)
	{


		switch(collider.gameObject.name)
		{

		case "Player":

			Destroy (this.gameObject);
			
			break;
			
		}	
		
	} 

	void Update(){ 
		transform.Rotate(new Vector3(15,30,45) * Time.deltaTime);
	}

	
}
