using UnityEngine;
using System.Collections;

/**
 * A class that controls the player's health.
 */
public class PlayerHealth : MonoBehaviour {

	// The amount of health assigned to the player.
	public float playerHealth = 100.0f;

	// A textual representation of the player's health, displayed on screen.
	public GUIText PlayerHealthText;

	
	/**
	 * The player's health increases with each level.
	 */
	void Start () {
		playerHealth = 100.0f * End.GetLevel();
	}
	
	/**
	 * Determines if the player has died. If so, loads the game over screen.
	 * Also updates the player's health text.
	 */
	void Update () {

		if (playerHealth <= 0.0f) {
			Application.LoadLevel (2);
		}

		PlayerHealthText.text = "Player Health: " + Mathf.Round (playerHealth);
	}

	/**
	 * If the player touches a monster, decreases the player's health.
	 * @param other The object colliding with the player.
	 */
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Monster") {

			playerHealth -= 5;
		}
	
	}

	/**
	 * Ensures that health is subtracted as a function of time, not instantaneously.
	 * @param other The object colliding with the player.
	 */
	void OnTriggerStay(Collider other)
	{

		if(other.tag == "Monster")
			playerHealth -= Time.deltaTime * End.GetLevel();
	}
}
