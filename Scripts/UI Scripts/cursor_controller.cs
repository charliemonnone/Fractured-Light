using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class cursor_controller : MonoBehaviour {

	Vector3 	mouse_pos;
	Transform 	tf;
	Camera		cam;
	float 		cam_dist;

	void Start () {
		Cursor.visible = false;
		tf = GetComponent<Transform>();
		cam = Camera.main;
		cam_dist = -cam.transform.position.z;
	}

	void Update () {
		mouse_pos = Input.mousePosition;
		mouse_pos.z = cam_dist;
		tf.position = cam.ScreenToWorldPoint(mouse_pos);
	}
}
