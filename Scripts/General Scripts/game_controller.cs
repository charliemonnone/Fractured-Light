using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class game_controller : MonoBehaviour {
	
	public GameObject			enemy, health, player, cursor;
	public float 				start_wait, spawn_wait;
	public int					spawn_size, score;
	player_controller			p_script;
	canvas_controller			canvas;
	Camera						cam;
	public bool					running;
	Vector3						enemy_spawn_pos;
	int							ramp_control;

	public void Start () {
		cam = Camera.main;
		canvas = GameObject.FindGameObjectWithTag("canvas").GetComponent<canvas_controller>();
		DirectorInit();
		PlayerInit();
		CoroutineInits();
	}

	void Update() {
		if(running) {
			if(Time.frameCount % 5 == 0) {
				score ++;
			}
		} else {
			canvas.ShowRestart();
		}
		
		
	}
	
	void PlayerInit() {
		player = Instantiate(player, Vector3.zero, Quaternion.identity);
		p_script = player.GetComponent<player_controller>();
	}

	void DirectorInit() {
		ramp_control = 0;
		running = true;
		score = 0;
	}

	void CoroutineInits() {
		StartCoroutine("SpawnEnemy");
		StartCoroutine("WaveTimer");
		StartCoroutine("HealthTimer");
	}

	void Ramp() {
		Debug.Log(spawn_size);
		switch(ramp_control) {
			case 0:
				spawn_size++;
				break;
			case 1:
				spawn_size--;
				break;
		}
	}

	// Should keep one instantiate object and activate/deactivate instead of allocating new obj
	// Should try similar thing for shots and enemies aswell
	IEnumerator HealthTimer() {
		Vector3 health_pos = Vector3.forward; 
		while(running) {
			health_pos.Set(Random.Range(0f,cam.pixelWidth), Random.Range(0f,cam.pixelHeight), 0f);
			if(p_script.health <= 2) {
				if(Random.Range(0f,1f) <= 0.3f) {
					Instantiate(health, cam.ScreenToViewportPoint(health_pos), Quaternion.identity);
					yield return new WaitForSeconds(10f);
				}
			}

			yield return new WaitForEndOfFrame();
		}
	}

	IEnumerator SpawnEnemy() {	
		yield return new WaitForSeconds(start_wait);
		while(running) {
			for(var i = 0; i < spawn_size; i ++) {
				enemy_spawn_pos = RandomSpawnPos();
				Instantiate(enemy, enemy_spawn_pos, Quaternion.identity);
			}
			yield return new WaitForSeconds(spawn_wait);
		}
	}

	IEnumerator WaveTimer() {
		yield return new WaitForSeconds(5.0f);
		while(running) {
			if(spawn_size == 6) {
				ramp_control = 1;
			} 
			if(spawn_size == 3){
				ramp_control = 0;
			}
			Ramp();
			yield return new WaitForSeconds(7.0f + Random.Range(-2f,0f));
		}
	}


	Vector3 RandomSpawnPos() {
		float select = Random.Range(0.0f, 1.0f);
		System.Math.Round(select, 2);
		int side = 0;
		float x = cam.pixelWidth;
		float y = cam.pixelHeight;
		float x_off = Random.Range(-x / 3, x / 3);
		float y_off = Random.Range(-y / 3, y / 3);
		Vector3 side_pos = new Vector3();

		if(select > 0.25f && select < 0.5f) {
			side = 1;
		} else if(select > 0.5f && select < 0.75f) {
			side = 2;
		} else if(select > 0.75f) {
			side = 3;
		}

		switch(side) 
		{
			case 0:
				side_pos.Set(x / 2 + x_off, y , 20f);
				break;
			case 1:
				side_pos.Set(x , y / 2 + y_off, 20f);
				break;
			case 2:
				side_pos.Set(x / 2 + x_off, 0f , 20f);
				break;
			case 3:
				side_pos.Set(0f , y / 2 + y_off, 20f);
				break;
		}

		return cam.ScreenToWorldPoint(side_pos);

	}

}
