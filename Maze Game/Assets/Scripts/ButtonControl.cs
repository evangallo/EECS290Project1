using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;

/**
 * The class that controls the buttons on the main menu/game over menu.
 */
public class ButtonControl : MonoBehaviour {

	//We use these three variables to set the difference between buttons in Unity.
	public bool isStartButton;

	public bool isQuitButton;

	public bool isRestartButton;

	void Start(){
	
	}

	/**
	 * Changes the color of the start text when moused over.
	 */
	void OnMouseEnter(){
		renderer.material.color = Color.red;
	}

	/**
	 * Changes the color of the text back to white once the mouse has left.
	 */
	void OnMouseExit(){

		renderer.material.color = Color.white;
	}

	/**
	 * Takes an action depending on what button was clicked.
	 * If the start button, loads the game. If the quit button, quits the game.
	 */
	void OnMouseUp(){

		if (isStartButton) {

			//Loads game, designated in build settings as level 1
			Application.LoadLevel(1);

		} else if (isQuitButton) {

			//Quits game
			Application.Quit();

		} else if (isRestartButton) {
			//Restarts from main menu.
			Application.LoadLevel(0);
		}
	}
}
