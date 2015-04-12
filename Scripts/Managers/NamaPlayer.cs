using UnityEngine;
using UnityEngine.UI;

using System.IO;
using System.Text;
using System.Collections;

public class NamaPlayer : MonoBehaviour {

	public Text nama;
	string namaPlayer;
	// Use this for initialization
	void Start () {
		namaPlayer = null;
		namaPlayer = nama.text;
	}
	
	// Update is called once per frame
	void Update () {
		if(nama!=null){
			print (namaPlayer);
		}
	}
}
