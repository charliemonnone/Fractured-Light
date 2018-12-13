using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class player_controller : MonoBehaviour {

	public Rigidbody 			rb;
	public GameObject 			shot, cursor;
	public Transform 			shot_tf, light_tf;
	public int 					health, clip_size;
	public float 				speed, pause, fire_rate;
	Vector3 					point_to, mouse_pos, cursor_pos;
	Transform 					tf;
	Camera						cam;
	AudioSource					sfx;
	int							shot_select;
	bool 						alive;
	ArrayList					clip;
	game_controller				game;


// For art, try making smaller sprite, requiring less down scaling


	void Start () {
		rb = GetComponent<Rigidbody>();
		tf = GetComponent<Transform>();
		sfx = GetComponent<AudioSource>();
		cursor = GameObject.FindGameObjectWithTag("cursor");
		game = GameObject.FindGameObjectWithTag("game_controller").GetComponent<game_controller>();
		cam = Camera.main;
		clip = new ArrayList();
		health = 3;
		alive = true;
		shot_select = 0;
		StartCoroutine("Monitor");
	}

	void Update() {
		rb.rotation = Quaternion.LookRotation(Vector3.forward,cursor.transform.position - tf.position);
	}
	
	void FixedUpdate () {
		Move();
		Fire();
		
	}

	void Move() {
		Vector3 limit = cam.WorldToViewportPoint(tf.position);
		limit.x = Mathf.Clamp(limit.x, 0.02f,0.98f);
		limit.y = Mathf.Clamp(limit.y, 0.05f,0.95f);
		tf.position = cam.ViewportToWorldPoint(limit);
		Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
		rb.velocity = move * speed / Time.deltaTime;
		
	}

	void Fire() {
		if(Input.GetButton("Fire1") && Time.time > pause) {
			GameObject temp_shot;
			pause = Time.time + fire_rate;
			if(clip.Count <= clip_size) {
				temp_shot = Instantiate(shot, shot_tf.position, rb.rotation);
				temp_shot.GetComponent<shot_controller>().enable_fire(true);
				clip.Add(temp_shot);
				
			}
			else {
				shot_select = (shot_select < clip.Count-1) ? shot_select+= 1 : shot_select = 0;
				temp_shot = (GameObject)clip[shot_select];
				temp_shot.GetComponent<shot_controller>().enable_fire(true);
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		
		if(other.gameObject.tag == "health_pickup") {
			sfx.Play();
			Destroy(other.gameObject);
			health = Mathf.Clamp(health += 1, 0, 3);
		}
		if(other.gameObject.tag == "enemy") {
			health = Mathf.Clamp(health -= 1, 0, 3);
		}

	}

	IEnumerator Monitor() {
		while(alive) {
			if(health <= 0) {
				Destroy(gameObject);
				alive = false;
				game.running = false;
			}
			yield return new WaitForFixedUpdate();
		}
	}
}

