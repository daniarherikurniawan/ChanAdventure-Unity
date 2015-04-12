using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Camera cam;
	private TextMesh timecounter;
	public float startTime;
	private string currentTime;
	private void Start () {
		cam.clearFlags = CameraClearFlags.Skybox;
		cam.clearFlags = CameraClearFlags.Depth;
		cam.rect = new Rect(0.02f, 0.05f, 0.4f, 0.4f);
		int i = 100;


	}
	private void Update(){
		startTime -= Time.deltaTime;
		currentTime = startTime.ToString ();
		//print (currentTime);
//		timecounter = GameObject.FindGameObjectWithTag ("TimeCounter").GetComponent<TextMesh> ();
//		timecounter.text = currentTime;
	}
}