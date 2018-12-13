using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup_controller : MonoBehaviour {

	Transform 			tf;
	public float 		rot_speed;
	public float 		move_factor;
	Vector3 			loc_change;
	void Start () {
		tf = GetComponent<Transform>();
		loc_change = new Vector3(0f,0f,0f);
		move_factor = 1;
	}
	
	
	void FixedUpdate () {
		loc_change.Set(0f,0f,Mathf.Sin(move_factor));
		tf.position = loc_change ;
		move_factor += 0.01f;
	}
}
