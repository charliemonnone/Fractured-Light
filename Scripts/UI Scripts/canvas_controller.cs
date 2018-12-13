using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class canvas_controller : MonoBehaviour {

	GameObject 			menu_ui, game_ui, restart_button; 
	AudioSource 		sfx;		

	void Start () {
		menu_ui = GameObject.FindGameObjectWithTag("menu_ui");
		game_ui = GameObject.FindGameObjectWithTag("game_ui");
		restart_button = GameObject.FindGameObjectWithTag("restart button");
		game_ui.SetActive(false);
		sfx = GetComponent<AudioSource>();
	}
	
	public void HideMenu() {
		menu_ui.SetActive(false);
		game_ui.SetActive(true);
		restart_button.SetActive(false);
	}

	public void PlayButtonSound() {
		sfx.Play();
	}

	public void ShowRestart() {
		restart_button.SetActive(true);

	}

}
