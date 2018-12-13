using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shot_burst_controller : MonoBehaviour {
	
	ParticleSystem 			ps;
	float 					end_timer;

	void Start () {
		ps = GetComponent<ParticleSystem>();
		end_timer = 1f;
		ps.Play(true);
		StartCoroutine("End");
		
	}
	
	IEnumerator End() {
		yield return new WaitForSeconds(end_timer);
		Destroy(gameObject);
	}

}
