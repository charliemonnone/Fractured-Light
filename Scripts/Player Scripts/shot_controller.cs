using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class shot_controller : MonoBehaviour {
	Rigidbody 					rb;
	Transform 					tf;
	public SphereCollider		scl;
	player_controller			player;
	game_controller				game;
	public bool					fire_switch;
	public float 				speed;
	public GameObject 			burst, sprite, self_light;

	void Start () {
		rb = GetComponent<Rigidbody>();
		tf = GetComponent<Transform>();
		scl = GetComponent<SphereCollider>();
		player = GameObject.FindGameObjectWithTag("player").GetComponent<player_controller>();
		game = GameObject.FindGameObjectWithTag("game_controller").GetComponent<game_controller>();
	}

	void Update() {
		// if(!fire_switch) {
		// 	tf.position = player.shot_tf.position;
		// 	rb.rotation = player.rb.rotation;
		// }

	}

	void FixedUpdate () {
		if(game.running) {
			if(fire_switch) {
				rb.velocity = tf.up  * speed / Time.deltaTime;
			}

			if(!fire_switch) {
				tf.position = player.shot_tf.position;
				rb.rotation = player.rb.rotation;
			}
		}
		
		
	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "shot" || other.gameObject.tag == "player") {
			Physics.IgnoreCollision(GetComponent<Collider>(), other);
		} else if(other.gameObject.tag == "enemy"){
			enable_fire(false);
		}
	}

	void OnTriggerExit(Collider other) {
		if(other.gameObject.tag == "bounds") {
			Instantiate(burst,tf.position,tf.rotation);
			enable_fire(false);
		}
	}


	public void enable_fire(bool f_switch) {
		scl.enabled = f_switch;
		sprite.SetActive(f_switch);
		self_light.SetActive(f_switch);
		fire_switch = f_switch;
		

	}

	public void Reset(Vector3 new_position, Quaternion new_rotation) {
		tf.position = new_position;
		rb.rotation = new_rotation;
	}
	

}
