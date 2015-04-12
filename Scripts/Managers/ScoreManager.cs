using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
	public static int score;        // The player's score.
	public static int coin; 
	public static int itemBrick; 
	public static int itemBomb; 

	public Text textScore;                      // Reference to the Text component.
	public Text textCoin;
	public Text textBrick;
	public Text textBomb;
	void Awake ()
	{
		// Set up the reference.
		//text = GetComponent <Text> ();
		
		// Reset the score.
		score = 0;
		coin = 0;
		itemBomb = 0;
		itemBrick = 0;
	}
	
	
	void Update ()
	{
		// Set the displayed text to be the word "Score" followed by the score value.
		textScore.text = "Score: " + score;
		textCoin.text = "Coin : " + coin;
		textBrick.text = "Brick : " + itemBrick;
		textBomb.text = "Bomb : " + itemBomb;
	}
}