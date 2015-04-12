using  UnityEngine;
using UnityEngine.UI;

using System.IO;
using System.Text;
using System.Collections;

public class MainMenuScript : MonoBehaviour {
	
	public Text nama;
	string namaPlayer;
	GameObject dataPlayer;

	public void startGame() {
		dataPlayer = GameObject.FindGameObjectWithTag ("DataPlayer");
		DataPlayerManager.namaPlayer = nama.text;
		Application.LoadLevel ("Stage1");
	}

	public void printNama(){

		namaPlayer = nama.text;
		DataPlayerManager.namaPlayer = namaPlayer;
		DontDestroyOnLoad(dataPlayer.gameObject);
		//print (namaPlayer);
	}

	public void closeGame(){
		Application.Quit();
	}
}
