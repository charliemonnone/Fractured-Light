using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class state_controller : MonoBehaviour {
	
	public GameObject			cursor, canvas, game_controller;


	void Start () {
		cursor = Instantiate(cursor, Vector3.zero, Quaternion.identity);
		canvas = Instantiate(canvas, Vector3.zero, Quaternion.identity, Camera.main.transform);
		
	}

	public void InitGame() {
		Instantiate(game_controller, Vector3.zero, Quaternion.identity);
	}

	public void ResetGame() {
		// Have to make a reset method, will probably refactor code to include a game object registry to make this easier.
		InitGame();
	}
	
}
