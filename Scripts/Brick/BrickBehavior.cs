using UnityEngine;
using System.Collections;

public class BrickBehavior : MonoBehaviour {
	
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
		transform.Rotate(new Vector3(0f,15,0f) * Time.deltaTime);
	}
	
	
}
