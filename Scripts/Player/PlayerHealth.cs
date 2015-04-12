using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
	public int startingHealth = 100;
	public float sinkSpeed = 2.5f;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
	float startTime;
	
	GameObject dataPlayer;
    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    bool isDead;
    bool damaged;

    void Awake ()
	{
		ScoreManager.coin = DataPlayerManager.coinScore ;
		ScoreManager.score = DataPlayerManager.killScore ;
		print ("Awal"+ DataPlayerManager.namaPlayer+"  "+DataPlayerManager.coinScore+"  "+DataPlayerManager.killScore);


		dataPlayer = GameObject.FindGameObjectWithTag ("DataPlayer");
		print ("My name "+ DataPlayerManager.namaPlayer);
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
        playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth;
    }

	Vector3 movement;      
	Rigidbody playerRigidbody; 
	public float speed = 6f;  

	void Move ()
	{
		playerRigidbody = GetComponent <Rigidbody> ();

		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");
		
		// Set the movement vector based on the axis input.
		movement.Set (h, 2f, v);
		
		// Normalise the movement vector and make it proportional to the speed per second.
		movement = movement.normalized * speed * Time.deltaTime;
		
		// Move the player to it's current position plus the movement.
		playerRigidbody.MovePosition (transform.position + movement);
	}

    void Update ()
    {
        if(damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		damaged = false;

    }


    public void TakeDamage (int amount)
    {
        damaged = true;

        currentHealth -= amount;

        healthSlider.value = currentHealth;

        playerAudio.Play ();

        if(currentHealth <= 0 && !isDead)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        playerShooting.DisableEffects ();

		//PlayerDAO.PostCurrentPlayerData();
        anim.SetTrigger ("Die");

        playerAudio.clip = deathClip;

        playerAudio.Play ();

        playerMovement.enabled = false;
        playerShooting.enabled = false;

    }

    public void RestartLevel ()
    {

			DontDestroyOnLoad(dataPlayer.gameObject);
			Application.LoadLevel(playerMovement.numberStage);
    }
}
