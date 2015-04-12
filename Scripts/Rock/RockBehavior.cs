using UnityEngine;
using System.Collections;

public class RockBehavior : MonoBehaviour {

	AudioSource  coinAudio;
 
	void OnTriggerEnter(Collider collider)
	{
	//print("Rock "+collider.gameObject.tag);

		switch(collider.gameObject.name)
		{
		case "ActiveBomb":
			Destroy (this.gameObject);
			
			break;
			
		}	
	} 


	void Update(){ 
	}

	
}
