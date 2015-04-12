using UnityEngine;
using UnityEngine.UI;

using System.IO;
using System.Text;
using System.Collections;

public class HighScoreManager : MonoBehaviour {
    public Text highScoreText;

	void refreshHighScore()
	{
		highScoreText.text = PlayerDAO.GetHighScoresAsString();
	}

	void Start () {
		refreshHighScore ();
	}

	void onEnable () {
		refreshHighScore ();
	}
}