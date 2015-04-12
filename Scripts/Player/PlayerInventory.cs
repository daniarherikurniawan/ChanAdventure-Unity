using UnityEngine;
using System.Collections;

public class PlayerInventory : MonoBehaviour {
	public bool isInventoryOpen;
	private Rect rectbomb;
	// Use this for initialization
	void Start () {
		isInventoryOpen = false;
	}
	
	// Update is called once per frame
	void Update () {

	}
	void OnGUI(){
		if(Input.GetKeyDown(KeyCode.I))
		{
			GUILayout.BeginArea(rectbomb);
			if(GUILayout.Button("Click To Bomb"))
			{
				print ("BombClicked");
			}
		}
	}
}
