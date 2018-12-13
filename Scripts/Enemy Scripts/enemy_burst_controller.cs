using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_burst_controller : MonoBehaviour {
	
	ParticleSystem 			ps;
	float 					end_timer;
	AudioSource				sfx;

	void Start () {
		sfx = GetComponent<AudioSource>();
		SetParamsAndPlay();
		ps = GetComponent<ParticleSystem>();
		end_timer = 1f;
		ps.Play(true);
		StartCoroutine("End");
		
	}
	
	IEnumerator End() {
		yield return new WaitForSeconds(end_timer);
		Destroy(gameObject);
	}

	void SetParamsAndPlay() {
		sfx.volume = Random.Range(0.3f, 0.7f);
		sfx.pitch = Random.Range(0.5f,1.5f);
		sfx.Play();
	}
}
