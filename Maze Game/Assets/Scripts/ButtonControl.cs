using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;


public class ButtonControl : MonoBehaviour {

	public bool isStartButton;

	public bool isOptionsButton;

	public bool isQuitButton;

	void Start(){
	
	}

	void OnMouseEnter(){

		//changes the color of the start text when highlighted
		renderer.material.color = Color.red;
	}

	void OnMouseExit(){

		//changes the color back to white when not highlighted
		renderer.material.color = Color.white;
	}

	void OnMouseUp(){

		if (isStartButton) {

			//Loads game, designated in build settings as level 1
			Application.LoadLevel(1);

		} else if (isOptionsButton) {

			//Loads options menu, designated in build settings as level 2
			Application.LoadLevel(2);

		} else if (isQuitButton) {

			//Quits game
			Application.Quit();

		}
	}
}
