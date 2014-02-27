using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public float playerHealth = 100.0f * End.level;
	public GUIText PlayerHealthText;

	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (playerHealth <= 0.0f) {
			Application.LoadLevel (2);
		}

		PlayerHealthText.text = "Player Health: " + Mathf.Round (playerHealth);
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Monster") {

			playerHealth -= 5;
		}
	
	}

	void OnTriggerStay(Collider other)
	{

		if(other.tag == "Monster")
			playerHealth -= Time.deltaTime * End.level;
	}
}
