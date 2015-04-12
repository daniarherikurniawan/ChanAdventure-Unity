using UnityEngine;
using System.Collections;

public class FinishManager : MonoBehaviour {

	public AudioSource WinSound;
	float startTime;
	Animator anim;
	GameObject player;	
	PlayerMovement playerMovement;
	GameObject dataPlayer;
	
	
	void Start()
	{

		dataPlayer = GameObject.FindGameObjectWithTag ("DataPlayer");
		player = GameObject.FindGameObjectWithTag ("Player");
		playerMovement = player.GetComponent <PlayerMovement> ();
	}
	

	
	void OnCollisionEnter(Collision collisionInfo)
	{
		if(collisionInfo.collider.name == "Player"){
			PlayerMovement.isWin = true;
			WinSound.Play();
			
			startTime = 4f;
			
			while(startTime >0){
				startTime -= Time.deltaTime ;
			}
			DataPlayerManager.coinScore =  ScoreManager.coin;
			DataPlayerManager.killScore = ScoreManager.score;
			print (DataPlayerManager.namaPlayer+"  "+DataPlayerManager.coinScore+"  "+DataPlayerManager.killScore);

			DontDestroyOnLoad(dataPlayer.gameObject);
			if(playerMovement.numberStage==1){
				Application.LoadLevel("Stage2");
			}
			if(playerMovement.numberStage==2){
				Application.LoadLevel("MainMenuScene");
			}
		}
	}
	
	void Update(){ 
		
	}
}
