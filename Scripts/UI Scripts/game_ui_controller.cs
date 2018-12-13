using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class game_ui_controller : MonoBehaviour {

	game_controller				game;
	player_controller			player;
	Vector3 					bl_corner_pos, ur_corner_pos;
	GameObject					score, health_bar;
	RectTransform				health_scale;
	Text						score_text;
	Camera						cam;
	bool						init;
	float						start_health;

	void Start () {
		score = GameObject.FindGameObjectWithTag("score text");
		score_text = score.GetComponent<Text>();
		health_bar = GameObject.FindGameObjectWithTag("health bar");
		health_scale = GameObject.FindGameObjectWithTag("health fill").GetComponent<RectTransform>();
		game = GameObject.FindGameObjectWithTag("game_controller").GetComponent<game_controller>();
		cam = Camera.main;
		ur_corner_pos = new Vector3(0.1f,0.95f,-cam.transform.position.z);
		bl_corner_pos = new Vector3(0.05f,0.1f,-cam.transform.position.z);
		health_bar.transform.position = cam.ViewportToWorldPoint(bl_corner_pos);
		score.transform.position = cam.ViewportToWorldPoint(ur_corner_pos);
		init = true;
	}

	void Update() {
		if(game.running && init) {
			player = GameObject.FindGameObjectWithTag("player").GetComponent<player_controller>();
			start_health = player.health;
			init = false; 
		}
		score_text.text = game.score.ToString();
		float y_scale = player.health / start_health;
		health_scale.localScale = new Vector3(health_scale.localScale.x, y_scale, health_scale.localScale.z);
	}
}
