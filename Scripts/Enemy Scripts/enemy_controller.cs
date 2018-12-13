using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class enemy_controller : MonoBehaviour {
	GameObject 					player;
	Transform 					tf;
	Rigidbody 					rb;
	camera_controller			cam;
	game_controller				game;
	public GameObject 			burst;
	public float 				rot_speed;
	public float 				move_speed;
	public Vector3 				target;

	void Start () {
		cam = Camera.main.GetComponent<camera_controller>();
		game = GameObject.FindGameObjectWithTag("game_controller").GetComponent<game_controller>();
		player = GameObject.FindGameObjectWithTag("player");
		tf = GetComponent<Transform>();
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate() {
		if(game.running) {
			target = player.GetComponent<Rigidbody>().position;
			tf.Rotate(Vector3.forward * rot_speed * Time.deltaTime, Space.World);
			MoveToPlayer(target);
		}
		
	}

	// void RotateToPlayer(Vector3 target) 
	// {
	// 	Quaternion quat = Quaternion.LookRotation(Vector3.up, target);
	// 	rb.rotation = Quaternion.RotateTowards(rb.rotation, quat, rot_speed / Time.deltaTime);
	// }

	void MoveToPlayer(Vector3 target) {
		Vector3 diff = target - rb.position;
		diff.z = 0f;
		rb.velocity = diff * move_speed * Time.deltaTime;
	}

	void OnTriggerEnter(Collider other) {
		// if(other.gameObject.activeInHierarchy) {
			if(other.gameObject.tag == "player" || other.gameObject.tag == "shot") {
			Instantiate(burst,tf.position,tf.rotation);
			cam.shake_val += 0.5f;
			game.score ++;
			Destroy(gameObject);
			
		// }
		}
		
	}

}
