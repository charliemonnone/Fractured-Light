using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_controller : MonoBehaviour {

	Transform 				tf;
	float					sin_counter; 
	Vector3 				pos_change, reset_position;
	public float			shake_val;
	void Start () {
		tf = GetComponent<Transform>();
		reset_position = tf.position;
		pos_change = Vector3.zero;
		sin_counter = 1f;
		shake_val = 0f;
	}
	
	
	void FixedUpdate () {
		pos_change.Set(Mathf.Cos(sin_counter), Mathf.Sin(sin_counter), tf.position.z);
		tf.position = pos_change;
		sin_counter += 0.001f;
		Shake();
		if(shake_val > 0f) {
			shake_val -= 0.07f;
		}
		
		
	}

	public void Shake() {
		tf.position = new Vector3(Mathf.PerlinNoise(Mathf.Pow(shake_val, 3),Mathf.Pow(shake_val, 5)), 
		Mathf.PerlinNoise(Mathf.Pow(shake_val, 2), Mathf.Pow(shake_val, 4)), tf.position.z);
	}
}
